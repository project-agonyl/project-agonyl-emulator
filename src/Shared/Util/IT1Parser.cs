#region copyright

// Copyright (c) 2020 Project Agonyl

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
        public IT1Parser(string filePath)
            : base(filePath)
        {
        }

        public void ParseFile(ref Dictionary<uint, Item> data)
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            for (var i = 0; i < fileBytes.Length; i += 52)
            {
                var item = new Item();
                item.SlotIndex = fileBytes[i]; // Investigate if this is right
                item.ItemType = fileBytes[i];
                item.ItemCode = Convert.ToUInt32((Functions.BytesToUInt16(fileBytes.Skip(i).Take(2).ToArray()) << 10) + Functions.BytesToUInt16(fileBytes.Skip(i + 2).Take(2).ToArray()));
                item.ItemName = System.Text.Encoding.Default.GetString(fileBytes.Skip(i + 4).Take(30).ToArray());
                item.NPCPrice = Functions.BytesToUInt32(fileBytes.Skip(i + 36).Take(4).ToArray());
                item.IT1ItemProperty.RequiredLevel = Functions.BytesToUInt16(fileBytes.Skip(i + 42).Take(2).ToArray());
                item.IT1ItemProperty.Attribute = Functions.BytesToUInt16(fileBytes.Skip(i + 44).Take(2).ToArray());
                item.IT1ItemProperty.BlueOption = Functions.BytesToUInt16(fileBytes.Skip(i + 46).Take(2).ToArray());
                item.IT1ItemProperty.RedOption = Functions.BytesToUInt16(fileBytes.Skip(i + 48).Take(2).ToArray());
                item.IT1ItemProperty.GreyOption = Functions.BytesToUInt16(fileBytes.Skip(i + 50).Take(2).ToArray());
                data.Add(item.ItemCode, item);
            }
        }
    }
}
