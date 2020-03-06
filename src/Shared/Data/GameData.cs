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
        public Dictionary<int, Item> Items;

        public GameData()
        {
            Items = new Dictionary<int, Item>();
        }
    }
}
