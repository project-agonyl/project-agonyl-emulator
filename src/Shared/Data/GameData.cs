#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System.Collections.Generic;
using Agonyl.Shared.Data.Game;

namespace Agonyl.Shared.Data
{
    /// <summary>
    /// Stores all game related data loaded from server files
    /// </summary>
    public class GameData
    {
        public Dictionary<uint, Item> Items;
        public Dictionary<uint, Map> Maps;

        public GameData()
        {
            this.Items = new Dictionary<uint, Item>();
            this.Maps = new Dictionary<uint, Map>();
        }

        public Item GetItemByCode(uint code)
        {
            if (!this.Items.ContainsKey(code))
            {
                return new Item();
            }

            return this.Items[code];
        }
    }
}
