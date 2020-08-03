﻿#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.Net;
using System.Net.Sockets;
using Agonyl.Shared.Util;
using Agonyl.Shared.Util.Security;

namespace Agonyl.Shared.Network
{
    public abstract class Connection
    {
        private byte[] _buffer, _backBuffer;
        private Crypt _crypto;

        protected Socket _socket;

        private object _cleanUpLock = new object();
        private bool _cleanedUp;

        /// <summary>
        /// State of the connection.
        /// </summary>
        public ConnectionState State { get; protected set; }

        /// <summary>
        /// True if logged in.
        /// </summary>
        public bool LoggedIn { get; set; }

        /// <summary>
        /// Whether to decrypt packet.
        /// </summary>
        public bool ShouldDecrypt { get; set; }

        /// <summary>
        /// Remote address.
        /// </summary>
        public string Address { get; protected set; }

        /// <summary>
        /// Raised when connection is closed.
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// Connection's index on the connection manager's list.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Username of the current connection.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Creates new connection.
        /// </summary>
        public Connection()
        {
            this._buffer = new byte[1024 * 500];
            this._backBuffer = new byte[ushort.MaxValue];
            this._crypto = new Crypt();

            this.State = ConnectionState.Open;
            this.Address = "?:?";
            this.ShouldDecrypt = true;
        }

        /// <summary>
        /// Sets connection's socket once.
        /// </summary>
        /// <param name="socket"></param>
        /// <exception cref="InvalidOperationException">Thrown if socket was already set.</exception>
        public void SetSocket(Socket socket)
        {
            if (this._socket != null)
            {
                throw new InvalidOperationException("Socket is already set.");
            }

            this._socket = socket;
            this.Address = ((IPEndPoint)socket.RemoteEndPoint).ToString();
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void Close()
        {
            if (this.State == ConnectionState.Closed)
            {
                Log.Warning("Attempted closing of an already closed connection.");
                return;
            }

            this.State = ConnectionState.Closed;

            try { this._socket.Shutdown(SocketShutdown.Both); }
            catch { }
            try { this._socket.Close(); }
            catch { }

            this.OnClosed();
        }

        /// <summary>
        /// Starts packet receiving.
        /// </summary>
        public void BeginReceive()
        {
            this._socket.BeginReceive(this._buffer, 0, this._buffer.Length, SocketFlags.None, this.OnReceive, null);
        }

        /// <summary>
        /// Called when new data is available from socket.
        /// </summary>
        /// <param name="result"></param>
        private void OnReceive(IAsyncResult result)
        {
            try
            {
                var length = this._socket.EndReceive(result);
                var read = 0;

                // Client disconnected
                if (length == 0)
                {
                    this.State = ConnectionState.Closed;
                    this.OnClosed();
                    return;
                }

                while (read < length)
                {
                    var packetLength = BitConverter.ToUInt16(this._buffer, read);
                    if (packetLength > length)
                    {
                        Log.Debug(BitConverter.ToString(this._buffer, read, length - read));
                        throw new Exception("Packet length greater than buffer length (" + packetLength + " > " + length + ").");
                    }

                    // Read packet from buffer
                    var packetBuffer = new byte[packetLength];
                    Buffer.BlockCopy(this._buffer, read, packetBuffer, 0, packetLength);
                    read += packetLength;

                    if (this.ShouldDecrypt && !(this._buffer[10] == 0x11 && this._buffer[11] == 0x38))
                    {
                        this._crypto.Decrypt(ref packetBuffer);
                    }

                    // Get packet
                    Packet packet;
                    if (this.ShouldDecrypt)
                    {
                        packet = new Packet(packetBuffer);
                    }
                    else
                    {
                        packet = new Packet(packetBuffer, 9);
                    }

                    // Check size from table?
                    var size = Op.GetSize(packet.Op);
                    if (size != 0 && packet.Length < size)
                    {
                        Log.Warning("Invalid packet size for '{0:X4}' ({1} < {2}), from '{3}'. Ignoring packet.", packet.Op, packet.Length, size, this.Address);
                    }
                    else
                    {
                        // Handle
                        try
                        {
                            this.HandlePacket(packet);
                        }
                        catch (Exception ex)
                        {
                            Log.Exception(ex, "Error while handling packet '{0:X4}', {1}.", packet.Op, Op.GetName(packet.Op));
                            Log.Debug(BitConverter.ToString(packetBuffer));
                        }
                    }
                }

                this.BeginReceive();
            }
            catch (SocketException)
            {
                this.State = ConnectionState.Closed;
                this.OnClosed();
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Error while receiving packet.");
            }
        }

        /// <summary>
        /// To be called when connection is closed, calls event
        /// and CleanUp.
        /// </summary>
        private void OnClosed()
        {
            this.Closed?.Invoke(this, null);

            lock (this._cleanUpLock)
            {
                if (!this._cleanedUp)
                {
                    this.CleanUp();
                }
                else
                {
                    Log.Warning("Trying to clean already cleaned connection.");
                }

                this._cleanedUp = true;
            }
        }

        /// <summary>
        /// Called when the connection is closed.
        /// </summary>
        protected virtual void CleanUp()
        {
            Log.Debug("CLEAN UP");
        }

        /// <summary>
        /// Called for every packet that is read from the network stream.
        /// </summary>
        /// <param name="packet"></param>
        protected virtual void HandlePacket(Packet packet)
        {
            Log.Warning("No packet handling.");
        }

        /// <summary>
        /// Sends packet to client.
        /// </summary>
        /// <param name="packet"></param>
        public virtual void Send(Packet packet)
        {
            if (this._socket == null || this.State == ConnectionState.Closed)
            {
                return;
            }

            // Create packet
            var buffer = new byte[packet.Length];
            packet.Build(ref buffer, 0);

            // Encrypt packet
            this._crypto.Encrypt(ref buffer);

            //Send
            this._socket.Send(buffer);
        }

        /// <summary>
        /// Sends packet to client.
        /// </summary>
        /// <param name="buffer"></param>
        public virtual void Send(byte[] buffer)
        {
            if (this._socket == null || this.State == ConnectionState.Closed)
            {
                return;
            }

            // Encrypt packet
            this._crypto.Encrypt(ref buffer);

            //Send
            this._socket.Send(buffer);
        }
    }

    public enum ConnectionState
    {
        Closed,
        Open,
    }
}
