#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.IO;
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
                var npcId = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i));
                var npc = new NPC()
                {
                    Id = npcId,
                    Location = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, i + 2)),
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
