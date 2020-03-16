#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data.Game
{
    public class NPC
    {
        public ushort Id { get; set; }

        public ushort Location { get; set; }

        public byte Orientation { get; set; }

        public byte SpawnStep { get; set; }
    }
}
