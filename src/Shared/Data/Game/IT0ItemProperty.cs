#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.Collections.Generic;

namespace Agonyl.Shared.Data.Game
{
    public class IT0ItemProperty
    {
        public byte ItemClass { get; set; }

        public byte ItemCategory { get; set; }

        public byte AttackActionCount { get; set; }

        public byte AttackRange { get; set; }

        public List<IT0ItemLevel> ItemLevel { get; set; }

        public IT0ItemProperty()
        {
            this.ItemLevel = new List<IT0ItemLevel>();
        }
    }
}
