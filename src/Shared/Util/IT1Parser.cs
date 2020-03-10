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
    public class IT1Parser : BinaryFileParser
    {
        public IT1Parser(string FilePath) : base(FilePath)
        {
        }

        public void ParseFile(ref Dictionary<uint, Item> Data)
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            for (var i = 0; i < fileBytes.Length; i += 52)
            {
                var Item = new Item();
                Item.SlotIndex = fileBytes[i]; // Investigate if this is right
                Item.ItemType = fileBytes[i];
                Item.ItemCode = Convert.ToUInt32((Functions.BytesToUInt16(fileBytes.Skip(i).Take(2).ToArray()) << 10) + Functions.BytesToUInt16(fileBytes.Skip(i + 2).Take(2).ToArray()));
                Item.ItemName = System.Text.Encoding.Default.GetString(fileBytes.Skip(i + 4).Take(30).ToArray());
                Item.NPCPrice = Functions.BytesToUInt32(fileBytes.Skip(i + 36).Take(4).ToArray());
                Item.IT1ItemProperty.RequiredLevel = Functions.BytesToUInt16(fileBytes.Skip(i + 42).Take(2).ToArray());
                Item.IT1ItemProperty.Attribute = Functions.BytesToUInt16(fileBytes.Skip(i + 44).Take(2).ToArray());
                Item.IT1ItemProperty.BlueOption = Functions.BytesToUInt16(fileBytes.Skip(i + 46).Take(2).ToArray());
                Item.IT1ItemProperty.RedOption = Functions.BytesToUInt16(fileBytes.Skip(i + 48).Take(2).ToArray());
                Item.IT1ItemProperty.GreyOption = Functions.BytesToUInt16(fileBytes.Skip(i + 50).Take(2).ToArray());
                Data.Add(Item.ItemCode, Item);
            }
        }
    }
}
