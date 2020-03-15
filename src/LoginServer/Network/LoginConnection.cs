#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using Agonyl.Shared.Network;
using Agonyl.Shared.Util;

namespace Agonyl.Login.Network
{
    public class LoginConnection : Connection
    {
        public LoginConnection()
        {
            this.ShouldDecrypt = false;
        }

        public override void Send(Packet packet)
        {
            if (this._socket == null || this.State == ConnectionState.Closed)
            {
                return;
            }

            var buffer = new byte[packet.Length];
            packet.Build(ref buffer, 0);
            this._socket.Send(buffer);
        }

        /// <summary>
        /// Handles login server packets.
        /// </summary>
        /// <param name="packet"></param>
        protected override void HandlePacket(Packet packet)
        {
            LoginPacketHandler.Instance.Handle(this, packet);
        }

        /// <summary>
        /// Cleans up connection, incl. account and characters.
        /// </summary>
        protected override void CleanUp()
        {
            return;
        }

        protected override void OnAfterClose()
        {
            if (this.Username != null && LoginServer.Instance.Redis.IsLoggedIn(this.Username))
            {
                Log.Info(this.Username + " account has left the login server");
                LoginServer.Instance.Redis.RemoveLoggedInAccount(this.Username);
            }
        }
    }
}
