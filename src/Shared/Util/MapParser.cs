#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.IO;
using System.Linq;
using Agonyl.Shared.Data.Game;

namespace Agonyl.Shared.Util
{
    public class MapParser : BinaryFileParser
    {
        public MapParser(string filePath)
            : base(filePath)
        {
        }

        public void ParseFile(ref Map map)
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            map.Name = System.Text.Encoding.Default.GetString(fileBytes.Skip(2).Take(20).ToArray());
            map.WarpCount = fileBytes[22];
            for (var i = 0; i < map.WarpCount; i++)
            {
                var warp = new Warp()
                {
                    MapNo = Functions.BytesToUInt16(fileBytes.Skip(23 + (6 * i)).Take(2).ToArray()),
                    Coordinates = Functions.BytesToUInt16(fileBytes.Skip(25 + (6 * i)).Take(4).ToArray()),
                };
                map.WarpList.Add(warp);
            }

            ushort x = 0, y = 0;
            var fileReadStart = BitConverter.GetBytes(map.Id).Length + map.Name.Length + sizeof(byte) + (map.WarpCount * 6);
            for (var i = fileReadStart; i < fileBytes.Length; i += 4)
            {
                var currentCell = new byte[4];
                Array.Copy(fileBytes, i, currentCell, 0, 4);
                var currentCellBinary = Convert.ToString(Functions.BytesToUInt32(currentCell), 2);
                map.NavigationMesh[x, y] = (byte)(currentCellBinary[currentCellBinary.Length - 1] == '1' ? 1 : 0);
                if (x <= 255)
                {
                    x++;
                }

                if (x > 255)
                {
                    x = 0;
                    y++;
                }

                if (y == 256)
                {
                    break;
                }
            }
        }
    }
}
