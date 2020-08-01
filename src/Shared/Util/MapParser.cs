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
            map.Id = Functions.BytesToUInt16(fileBytes.Take(2).ToArray());
            map.Name = System.Text.Encoding.Default.GetString(fileBytes.Skip(2).Take(20).ToArray());
            map.WarpCount = fileBytes[22];
            for (int i = 0; i < map.WarpCount; i++)
            {
                var warp = new Warp();
                warp.MapNo = Functions.BytesToUInt16(fileBytes.Skip(23 + (6 * i)).Take(2).ToArray());
                warp.Coordinates = Functions.BytesToUInt16(fileBytes.Skip(25 + (6 * i)).Take(4).ToArray());
                map.WarpList.Add(warp);
            }
            ushort x = 0, y = 0;
            int fileReadStart = BitConverter.GetBytes(map.Id).Length + map.Name.Length + sizeof(byte) + (map.WarpCount * 6);
            // int fileReadStart = fileBytes.Length - 0x40000;
            for (int i = fileReadStart; i < fileBytes.Length; i += 4)
            {
                byte[] temp = new byte[4];
                Array.Copy(fileBytes, i, temp, 0, 4);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(temp);
                char[] binaryData = string.Join("", temp.Select(a => Convert.ToString(a, 2).PadLeft(8, '0'))).ToArray();
                map.NavigationMesh[x, y] = (byte)(binaryData[binaryData.Length - 1] == '1' ? 1 : 0);
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
                    break;
            }
        }
    }
}
