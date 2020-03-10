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
    public class IT0Parser : BinaryFileParser
    {
        public IT0Parser(string FilePath) : base(FilePath)
        {
        }

        public void ParseFile(ref Dictionary<uint, Item> Data, ref IT0exParser IT0exParser)
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            var IT0exFileBytes = File.ReadAllBytes(IT0exParser.FilePath);
            for (var i = 0; i < fileBytes.Length; i += 242)
            {
                // Sometimes the file is bigger than usual hence this check is needed
                if (i + 241 > fileBytes.Length)
                {
                    continue;
                }
                var currentIndex = Functions.BytesToUInt16(fileBytes.Skip(i + 2).Take(2).ToArray());
                var Item = new Item();
                Item.ItemCode = Convert.ToUInt32((Functions.BytesToUInt16(fileBytes.Skip(i).Take(2).ToArray()) << 10) + currentIndex);
                if (Data.ContainsKey(Item.ItemCode))
                {
                    continue;
                }
                Item.SlotIndex = fileBytes[i + 4];
                Item.ItemType = fileBytes[i + 6];
                Item.ItemName = System.Text.Encoding.Default.GetString(fileBytes.Skip(i + 7).Take(30).ToArray());
                Item.NPCPrice = Functions.BytesToUInt32(fileBytes.Skip(i + 40).Take(4).ToArray());
                for (var j = 0; j < 10; j++)
                {
                    var levelData = new IT0ItemLevel();
                    levelData.Level = Convert.ToByte(j + 1);
                    levelData.AdditionalAttribute = Functions.BytesToUInt16(fileBytes.Skip(i + (18 * j) + 62).Take(2).ToArray());
                    levelData.Strength = Functions.BytesToUInt16(fileBytes.Skip(i + (18 * j) + 64).Take(2).ToArray());
                    levelData.Dexterity = Functions.BytesToUInt16(fileBytes.Skip(i + (18 * j) + 66).Take(2).ToArray());
                    levelData.Intelligence = Functions.BytesToUInt16(fileBytes.Skip(i + (18 * j) + 68).Take(2).ToArray());
                    levelData.Attribute = Functions.BytesToUInt16(fileBytes.Skip(i + (18 * j) + 70).Take(2).ToArray());
                    levelData.AttributeRange = Functions.BytesToUInt16(fileBytes.Skip(i + (18 * j) + 72).Take(2).ToArray());
                    levelData.BlueOption = Functions.BytesToUInt16(fileBytes.Skip(i + (18 * j) + 74).Take(2).ToArray());
                    levelData.RedOption = Functions.BytesToUInt16(fileBytes.Skip(i + (18 * j) + 76).Take(2).ToArray());
                    levelData.GreyOption = Functions.BytesToUInt16(fileBytes.Skip(i + (18 * j) + 78).Take(2).ToArray());
                    Item.IT0ItemProperty.ItemLevel.Add(levelData);
                }
                for (var k = 0; k < IT0exFileBytes.Length; k += 92)
                {
                    if (currentIndex == Functions.BytesToUInt16(IT0exFileBytes.Skip(k).Take(2).ToArray()))
                    {
                        for (var j = 0; j < 5; j++)
                        {
                            var levelData = new IT0ItemLevel();
                            levelData.Level = Convert.ToByte(j + 11);
                            levelData.AdditionalAttribute = Functions.BytesToUInt16(IT0exFileBytes.Skip(k + (18 * j) + 2).Take(2).ToArray());
                            levelData.Strength = Functions.BytesToUInt16(IT0exFileBytes.Skip(k + (18 * j) + 4).Take(2).ToArray());
                            levelData.Dexterity = Functions.BytesToUInt16(IT0exFileBytes.Skip(k + (18 * j) + 6).Take(2).ToArray());
                            levelData.Intelligence = Functions.BytesToUInt16(IT0exFileBytes.Skip(k + (18 * j) + 8).Take(2).ToArray());
                            levelData.Attribute = Functions.BytesToUInt16(IT0exFileBytes.Skip(k + (18 * j) + 10).Take(2).ToArray());
                            levelData.AttributeRange = Functions.BytesToUInt16(IT0exFileBytes.Skip(k + (18 * j) + 12).Take(2).ToArray());
                            levelData.BlueOption = Functions.BytesToUInt16(IT0exFileBytes.Skip(k + (18 * j) + 14).Take(2).ToArray());
                            levelData.RedOption = Functions.BytesToUInt16(IT0exFileBytes.Skip(k + (18 * j) + 16).Take(2).ToArray());
                            levelData.GreyOption = Functions.BytesToUInt16(IT0exFileBytes.Skip(k + (18 * j) + 18).Take(2).ToArray());
                            Item.IT0ItemProperty.ItemLevel.Add(levelData);
                        }
                        break;
                    }
                }
                Data.Add(Item.ItemCode, Item);
            }
        }
    }
}
