#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

using System;
using Agonyl.Shared.Network;

namespace Agonyl.Game.Network
{
	public static class Send
	{
		public static void G2C_CHARACTER_LIST(GameConnection conn, Data.Account account)
		{
			var packet = new Packet(Op.G2C_CHARACTER_LIST);
			var header = new byte[] { 0xB8, 0x03, 0x00, 0x00, 0x00, 0x00, 0x0b, 0x00, 0x03, 0xFF, 0x05, 0x11 };
			packet.PutBytes(header);
			var characters = GameServer.Instance.Database.GetCharacterList(account.Id);
			while (characters.Read())
			{
				packet.PutString(characters["name"].ToString());
				packet.PutEmptyBin(20 - characters["name"].ToString().Length);
				packet.PutByte(0x00);
				packet.PutByte(0x01);
				packet.PutInt(Convert.ToInt32(characters["type"].ToString()));
				packet.PutInt(Convert.ToInt32(characters["town"].ToString()));
				packet.PutInt(Convert.ToInt32(characters["level"].ToString()));
				packet.PutEmptyBin(188 - packet.Length);
			}
			packet.PutEmptyBin(952 - packet.Length);
			conn.Send(packet);
		}
	}
}
