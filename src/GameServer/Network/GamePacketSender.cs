#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System.Linq;
using Agonyl.Game.Network.PacketBuilder;
using Agonyl.Shared.Network;

namespace Agonyl.Game.Network
{
    public static class Send
    {
        public static void G2C_CHARACTER_LIST(GameConnection conn, Data.Account account)
        {
            var packet = new Packet(Op.G2C_CHARACTER_LIST);
            var packetBuilder = new CharacterListPacketBuilder(GameServer.Instance.ASDDatabase.GetCharacterList(account.Username).ToList());
            packetBuilder.Build(ref packet);
            conn.Send(packet);
        }
    }
}
