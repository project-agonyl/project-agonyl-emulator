#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using System.IO;
using Agonyl.Shared.Data.Game;

namespace Agonyl.Shared.Util
{
    public class IT2Parser : BinaryFileParser
    {
        public IT2Parser(string filePath)
            : base(filePath)
        {
        }

        public void ParseFile(ref Dictionary<uint, Item> data)
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            for (var i = 0; i < fileBytes.Length; i += 48)
            {
                var item = new Item();
                item.ItemType = fileBytes[i];

                // Sometimes the file is bigger than usual hence this check is needed
                if (i + 47 > fileBytes.Length)
                {
                    continue;
                }

                item.ItemCode = Convert.ToUInt32((Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i)) << 10) + Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + 2)));
                if (data.ContainsKey(item.ItemCode))
                {
                    continue;
                }

                item.ItemName = System.Text.Encoding.Default.GetString(Functions.SkipAndTakeLinqShim(ref fileBytes, 30, i + 4)).Trim();
                item.NPCPrice = Functions.BytesToUInt32(Functions.SkipAndTakeLinqShim(ref fileBytes, 4, i + 36));
                item.IT2ItemProperty.RequiredLevel = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + 42));
                item.IT2ItemProperty.SkillLevel = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + 46));
                data.Add(item.ItemCode, item);
            }
        }
    }
}
