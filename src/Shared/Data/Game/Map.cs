#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.Collections.Generic;

namespace Agonyl.Shared.Data.Game
{
    public class Map
    {
        public ushort Id { get; set; }

        public string Name { get; set; }

        public byte[,] NavigationMesh;

        public byte WarpCount { get; set; }

        public List<Warp> WarpList;

        public List<NPC> Shops;

        public List<NPC> Monsters;

        public Map()
        {
            this.Shops = new List<NPC>();
            this.Monsters = new List<NPC>();
            this.WarpList = new List<Warp>();
            this.NavigationMesh = new byte[256, 256];
        }

        public Map(ushort id, string name = "")
        {
            this.Id = id;
            this.Name = name;
            this.Shops = new List<NPC>();
            this.Monsters = new List<NPC>();
            this.WarpList = new List<Warp>();
            this.NavigationMesh = new byte[256, 256];
        }
    }
}
