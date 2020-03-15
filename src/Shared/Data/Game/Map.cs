#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System.Collections.Generic;

namespace Agonyl.Shared.Data.Game
{
    public class Map
    {
        public ushort Id { get; set; }

        public string Name { get; set; }

        public Dictionary<ushort, NPC> Npcs;

        public Map()
        {
            this.Npcs = new Dictionary<ushort, NPC>();
        }

        public Map(ushort id, string name = "")
        {
            this.Id = id;
            this.Name = name;
            this.Npcs = new Dictionary<ushort, NPC>();
        }
    }
}
