#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using Agonyl.Game.Data;
using Agonyl.Shared.Network;
using Agonyl.Shared.Util;

namespace Agonyl.Game.Network
{
	public class GamePacketHandler : PacketHandler<GameConnection>
    {
        public static readonly GamePacketHandler Instance = new GamePacketHandler();

        /// <summary>
        /// Sent on clicking server name
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="packet"></param>
        [PacketHandler(Op.C2G_CHARACTER_LIST)]
        public void C2G_CHARACTER_LIST(GameConnection conn, Packet packet)
        {
            packet.SetReadPointer(14);
            var username = packet.GetString(20);
            packet.SetReadPointer(35);
            var password = packet.GetString(20);
            if (GameServer.Instance.ASDDatabase.AccountExists(username, password))
            {
				Log.Info("Received character list request from the account " + username);
				conn.Account = new Account(username);
				conn.Username = username;
                Send.G2C_CHARACTER_LIST(conn, conn.Account);
            }
        }

        /// <summary>
        /// Sent client exits
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="packet"></param>
        [PacketHandler(Op.C2G_CLIENT_EXIT)]
        public void C2G_CLIENT_EXIT(GameConnection conn, Packet packet)
        {
			if (conn.Account != null && GameServer.Instance.Redis.IsLoggedIn(conn.Account.Username))
			{
				Log.Info(conn.Account.Username + " account has left the game server");
				GameServer.Instance.Redis.RemoveLoggedInAccount(conn.Account.Username);
			}
        }
    }
}
