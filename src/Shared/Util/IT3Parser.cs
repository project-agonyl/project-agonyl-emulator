#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Agonyl.Shared.Data.Game;

namespace Agonyl.Shared.Util
{
    public class IT3Parser : BinaryFileParser
    {
        public IT3Parser(string FilePath) : base(FilePath)
        {
        }

        public void ParseFile(ref Dictionary<uint, Item> Data)
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            for (var i = 0; i < fileBytes.Length; i += 48)
            {
                var Item = new Item();
                Item.ItemType = fileBytes[i];
                // Sometimes the file is bigger than usual hence this check is needed
                if (i + 47 > fileBytes.Length)
                {
                    continue;
                }
                Item.ItemCode = Convert.ToUInt32((Functions.BytesToUInt16(fileBytes.Skip(i).Take(2).ToArray()) << 10) + Functions.BytesToUInt16(fileBytes.Skip(i + 2).Take(2).ToArray()));
                if (Data.ContainsKey(Item.ItemCode))
                {
                    continue;
                }
                Item.ItemName = System.Text.Encoding.Default.GetString(fileBytes.Skip(i + 4).Take(30).ToArray());
                Item.NPCPrice = Functions.BytesToUInt32(fileBytes.Skip(i + 36).Take(4).ToArray());
                Data.Add(Item.ItemCode, Item);
            }
        }
    }
}
