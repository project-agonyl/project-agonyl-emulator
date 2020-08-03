#region copyright

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
            // TODO: Save stuff to database
            if (this.Character != null)
            {
                this.Character.Map.RemoveCharacter(this.Character);
            }

            if (this.Account != null && GameServer.Instance.Redis.IsLoggedIn(this.Account.Username))
            {
                GameServer.Instance.Redis.RemoveLoggedInAccount(this.Account.Username);
            }
        }
    }
}
