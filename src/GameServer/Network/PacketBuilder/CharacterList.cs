#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using Agonyl.Shared.Database.Model;
using Agonyl.Shared.Network;

namespace Agonyl.Game.Network.PacketBuilder
{
    public class CharacterList : Shared.Network.PacketBuilder
    {
        private List<Charac0> _characters;

        public CharacterList(List<Charac0> characters, int PacketSize = 952, byte Control = 0x03, byte Command = 0xFF, int Protocol = 4357, int uid = 0) : base(PacketSize, Control, Command, Protocol, uid)
        {
            _characters = characters;
        }

        public new void Build(ref Packet packet)
        {
            this._buildHeader(ref packet);
            if (_characters.Count != 0)
            {
                foreach (var character in _characters)
                {
                    packet.PutString(character.c_id); // Name
                    packet.PutEmptyBin(20 - character.c_id.Length);
                    packet.PutByte(0x00);
                    packet.PutByte(0x01);
                    packet.PutByte(Convert.ToByte(character.c_sheaderb)); // Type
                    packet.PutByte(0x00); // Town
                    packet.PutReverseHexOfInt(Convert.ToInt32(character.c_sheaderc)); // Level
                    packet.PutEmptyBin(160);
                }
            }
            for (var i = 0; i < 5 - _characters.Count; i++)
            {
                packet.PutEmptyBin(20);
                packet.PutByte(0x00);
                packet.PutByte(0x01);
                packet.PutByte(0xFF);
                packet.PutEmptyBin(165);
            }
            packet.PutEmptyBin(952 - packet.Length);
        }
    }
}
