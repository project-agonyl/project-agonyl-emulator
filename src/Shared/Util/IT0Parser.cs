#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using System.IO;
using Agonyl.Shared.Data.Game;

namespace Agonyl.Shared.Util
{
    public class IT0Parser : BinaryFileParser
    {
        public IT0Parser(string filePath)
            : base(filePath)
        {
        }

        public void ParseFile(ref Dictionary<uint, Item> data, ref IT0exParser iT0exParser)
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            var iT0exFileBytes = File.ReadAllBytes(iT0exParser.FilePath);
            for (var i = 0; i < fileBytes.Length; i += 242)
            {
                // Sometimes the file is bigger than usual hence this check is needed
                if (i + 241 > fileBytes.Length)
                {
                    continue;
                }

                var currentIndex = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + 2));
                var item = new Item();
                item.ItemCode = Convert.ToUInt32((Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i)) << 10) + currentIndex);
                if (data.ContainsKey(item.ItemCode))
                {
                    continue;
                }

                item.SlotIndex = fileBytes[i + 4];
                item.ItemType = fileBytes[i + 6];
                item.ItemName = System.Text.Encoding.Default.GetString(Functions.SkipAndTakeLinqShim(ref fileBytes, 30, i + 7)).Trim();
                item.NPCPrice = Functions.BytesToUInt32(Functions.SkipAndTakeLinqShim(ref fileBytes, 4, i + 40));
                for (var j = 0; j < 10; j++)
                {
                    var levelData = new IT0ItemLevel();
                    levelData.Level = Convert.ToByte(j + 1);
                    levelData.AdditionalAttribute = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + (18 * j) + 62));
                    levelData.Strength = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + (18 * j) + 64));
                    levelData.Dexterity = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + (18 * j) + 66));
                    levelData.Intelligence = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + (18 * j) + 68));
                    levelData.Attribute = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + (18 * j) + 70));
                    levelData.AttributeRange = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + (18 * j) + 72));
                    levelData.BlueOption = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + (18 * j) + 74));
                    levelData.RedOption = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + (18 * j) + 76));
                    levelData.GreyOption = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + (18 * j) + 78));
                    item.IT0ItemProperty.ItemLevel.Add(levelData);
                }

                for (var k = 0; k < iT0exFileBytes.Length; k += 92)
                {
                    if (currentIndex == Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref iT0exFileBytes, 2, k)))
                    {
                        for (var j = 0; j < 5; j++)
                        {
                            var levelData = new IT0ItemLevel();
                            levelData.Level = Convert.ToByte(j + 11);
                            levelData.AdditionalAttribute = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref iT0exFileBytes, 2, k + (18 * j) + 2));
                            levelData.Strength = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref iT0exFileBytes, 2, k + (18 * j) + 4));
                            levelData.Dexterity = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref iT0exFileBytes, 2, k + (18 * j) + 6));
                            levelData.Intelligence = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref iT0exFileBytes, 2, k + (18 * j) + 8));
                            levelData.Attribute = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref iT0exFileBytes, 2, k + (18 * j) + 10));
                            levelData.AttributeRange = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref iT0exFileBytes, 2, k + (18 * j) + 12));
                            levelData.BlueOption = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref iT0exFileBytes, 2, k + (18 * j) + 14));
                            levelData.RedOption = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref iT0exFileBytes, 2, k + (18 * j) + 16));
                            levelData.GreyOption = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref iT0exFileBytes, 2, k + (18 * j) + 18));
                            item.IT0ItemProperty.ItemLevel.Add(levelData);
                        }

                        break;
                    }
                }

                data.Add(item.ItemCode, item);
            }
        }
    }
}
