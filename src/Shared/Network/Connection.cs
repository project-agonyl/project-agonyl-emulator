#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

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
		/// Creates new connection.
		/// </summary>
		public Connection()
		{
			_buffer = new byte[1024 * 500];
			_backBuffer = new byte[ushort.MaxValue];
			_crypto = new Crypt();

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
			if (_socket != null)
				throw new InvalidOperationException("Socket is already set.");

			_socket = socket;
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

			try { _socket.Shutdown(SocketShutdown.Both); }
			catch { }
			try { _socket.Close(); }
			catch { }

			this.OnClosed();
		}

		/// <summary>
		/// Starts packet receiving.
		/// </summary>
		public void BeginReceive()
		{
			_socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, this.OnReceive, null);
		}

		/// <summary>
		/// Called when new data is available from socket.
		/// </summary>
		/// <param name="result"></param>
		private void OnReceive(IAsyncResult result)
		{
			try
			{
				var length = _socket.EndReceive(result);
				var read = 0;

				// Client disconnected
				if (length == 0)
				{
					this.State = ConnectionState.Closed;
					this.OnClosed();
					Log.Info("Connection was closed from '{0}'.", this.Address);
					return;
				}

				while (read < length)
				{
					var packetLength = BitConverter.ToUInt16(_buffer, read);
					if (packetLength > length)
					{
						Log.Debug(BitConverter.ToString(_buffer, read, length - read));
						throw new Exception("Packet length greater than buffer length (" + packetLength + " > " + length + ").");
					}

					// Read packet from buffer
					var packetBuffer = new byte[packetLength];
					Buffer.BlockCopy(_buffer, read, packetBuffer, 0, packetLength);
					read += packetLength;

					if (this.ShouldDecrypt)
						_crypto.Decrypt(ref packetBuffer);

					// Get packet
					Packet packet;
					if (this.ShouldDecrypt)
						packet = new Packet(packetBuffer);
					else
						packet = new Packet(packetBuffer, 9);

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
				Log.Info("Lost connection from '{0}'.", this.Address);

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

			lock (_cleanUpLock)
			{
				if (!_cleanedUp)
					this.CleanUp();
				else
					Log.Warning("Trying to clean already cleaned connection.");

				_cleanedUp = true;
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
			if (_socket == null || this.State == ConnectionState.Closed)
				return;

			// Get size from table
			var size = Op.GetSize(packet.Op);
			if (size == -1)
				throw new ArgumentException("Size for op '" + packet.Op.ToString("X4") + "' unknown.");

			// Calculate length
			var fixHeaderSize = (sizeof(short) + sizeof(int) + sizeof(int) + packet.Length);
			var dynHeaderSize = (sizeof(short) + sizeof(int) + sizeof(int) + sizeof(short) + packet.Length);
			var length = (size == 0 ? dynHeaderSize : size);

			// Check table length
			if (size != 0)
			{
				if (length < sizeof(short) + sizeof(int) + sizeof(int) + packet.Length)
					throw new Exception("Packet is bigger than specified in the packet size table.");

				if (size != sizeof(short) + sizeof(int) + sizeof(int) + packet.Length)
					Log.Warning("Packet size doesn't match packet table size. (op: {3} ({0:X4}), size: {1}, expected: {2})", packet.Op, fixHeaderSize, size, Op.GetName(packet.Op));
			}

			// Create packet
			var buffer = new byte[length];
			Buffer.BlockCopy(BitConverter.GetBytes((short)packet.Op), 0, buffer, 0, sizeof(short));
			Buffer.BlockCopy(BitConverter.GetBytes(-1), 0, buffer, sizeof(short), sizeof(int)); // checksum?

			var offset = (sizeof(short) + sizeof(int) + sizeof(int));
			if (size == 0)
			{
				Buffer.BlockCopy(BitConverter.GetBytes((short)length), 0, buffer, offset, sizeof(short));
				offset += sizeof(short);
			}

			packet.Build(ref buffer, offset);

			//Send
			_socket.Send(buffer);
		}
	}

	public enum ConnectionState
	{
		Closed,
		Open,
	}
}
