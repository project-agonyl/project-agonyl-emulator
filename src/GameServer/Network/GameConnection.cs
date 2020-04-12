﻿#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using Agonyl.Game.Data;
using Agonyl.Shared.Network;
using Agonyl.Shared.Util;

namespace Agonyl.Game.Network
{
    public class GameConnection : Connection
    {
        /// <summary>
        /// Holds account details.
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Holds selected character details.
        /// </summary>
        public Character Character { get; set; }

        /// <summary>
        /// Handles game server packets.
        /// </summary>
        /// <param name="packet"></param>
        protected override void HandlePacket(Packet packet)
        {
            GamePacketHandler.Instance.Handle(this, packet);
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
            if (this.Account != null && GameServer.Instance.Redis.IsLoggedIn(this.Account.Username))
            {
                Log.Info(this.Account.Username + " account has left the game server");
                GameServer.Instance.Redis.RemoveLoggedInAccount(this.Account.Username);
            }
        }
    }
}
