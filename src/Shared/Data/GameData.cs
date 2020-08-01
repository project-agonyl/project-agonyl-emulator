#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.Collections.Generic;
using Agonyl.Shared.Data.Game;

namespace Agonyl.Shared.Data
{
    /// <summary>
    /// Stores all game related data loaded from server files.
    /// </summary>
    public class GameData
    {
        public Dictionary<uint, Item> Items;
        public Dictionary<ushort, Map> Maps;
        public Dictionary<ushort, NPCData> NPCData;
        public Dictionary<ushort, Quest> Quests;

        public GameData()
        {
            this.Items = new Dictionary<uint, Item>();
            this.Maps = new Dictionary<ushort, Map>();
            this.NPCData = new Dictionary<ushort, NPCData>();
            this.Quests = new Dictionary<ushort, Quest>();
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
