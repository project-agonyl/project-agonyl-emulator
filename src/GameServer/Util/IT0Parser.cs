#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Agonyl.Shared.Data.Game;
using Agonyl.Shared.Util;

namespace Agonyl.Game.Util
{
    public class IT0Parser : BinaryFileParser
    {
        public IT0Parser(string FilePath) : base(FilePath)
        {
        }

        public void ParseFile(ref Dictionary<uint, Item> Data)
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            for (var i = 0; i < fileBytes.Length; i += 242)
            {
                var Item = new Item();
                Item.ItemCode = Convert.ToUInt32((Functions.BytesToInt16(fileBytes.Skip(i).Take(2).ToArray()) << 10) + Functions.BytesToInt16(fileBytes.Skip(i + 2).Take(2).ToArray()));
                Item.SlotIndex = fileBytes[i + 4];
                Item.ItemType = fileBytes[i + 6];
                Item.ItemName = System.Text.Encoding.Default.GetString(fileBytes.Skip(i + 7).Take(30).ToArray());
                Data.Add(Item.ItemCode, Item);
            }
        }
    }
}
