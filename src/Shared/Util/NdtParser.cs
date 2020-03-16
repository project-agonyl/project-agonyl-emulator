#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.IO;
using System.Linq;
using Agonyl.Shared.Data.Game;

namespace Agonyl.Shared.Util
{
    public class NdtParser : BinaryFileParser
    {
        public NdtParser(string filePath)
            : base(filePath)
        {
        }

        public void ParseFile(ref Map map)
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            for (var i = 0; i < fileBytes.Length; i += 8)
            {
                var npcId = Functions.BytesToUInt16(fileBytes.Skip(i).Take(2).ToArray());
                var npc = new NPC()
                {
                    Id = npcId,
                    Location = Functions.BytesToUInt16(fileBytes.Skip(i + 2).Take(2).ToArray()),
                    Orientation = fileBytes[i + 6],
                    SpawnStep = fileBytes[i + 7],
                };
                if (npcId < 1000)
                {
                    map.Monsters.Add(npc);
                }
                else
                {
                    map.Shops.Add(npc);
                }
            }
        }
    }
}
