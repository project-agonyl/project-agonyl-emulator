#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using System.IO;
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
                item.ItemType = fileBytes[i];
                item.SlotIndex = (byte)(item.ItemType == 4 ? 8 : 9);
                item.ItemCode = Convert.ToUInt32((Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i)) << 10) + Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + 2)));
                item.ItemName = System.Text.Encoding.Default.GetString(Functions.SkipAndTakeLinqShim(ref fileBytes, 30, i + 4)).Trim();
                item.NPCPrice = Functions.BytesToUInt32(Functions.SkipAndTakeLinqShim(ref fileBytes, 4, i + 36));
                item.IT1ItemProperty.RequiredLevel = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + 42));
                item.IT1ItemProperty.Attribute = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + 44));
                item.IT1ItemProperty.BlueOption = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + 46));
                item.IT1ItemProperty.RedOption = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + 48));
                item.IT1ItemProperty.GreyOption = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + 50));
                data.Add(item.ItemCode, item);
            }
        }
    }
}
