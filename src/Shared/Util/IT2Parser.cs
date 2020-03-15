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

                item.ItemCode = Convert.ToUInt32((Functions.BytesToUInt16(fileBytes.Skip(i).Take(2).ToArray()) << 10) + Functions.BytesToUInt16(fileBytes.Skip(i + 2).Take(2).ToArray()));
                if (data.ContainsKey(item.ItemCode))
                {
                    continue;
                }

                item.ItemName = System.Text.Encoding.Default.GetString(fileBytes.Skip(i + 4).Take(30).ToArray());
                item.NPCPrice = Functions.ReverseHexBytesToUInt32(fileBytes.Skip(i + 36).Take(4).ToArray());
                item.IT2ItemProperty.RequiredLevel = Functions.BytesToUInt16(fileBytes.Skip(i + 42).Take(2).ToArray());
                item.IT2ItemProperty.SkillLevel = Functions.BytesToUInt16(fileBytes.Skip(i + 46).Take(2).ToArray());
                data.Add(item.ItemCode, item);
            }
        }
    }
}
