#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data.Game
{
    public class NPC
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public ushort Location { get; set; }
        public byte Orientation { get; set; }
        public byte SpawnStep { get; set; }

        public NPC()
        {
        }

        public NPC(ushort id, string name = "")
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
