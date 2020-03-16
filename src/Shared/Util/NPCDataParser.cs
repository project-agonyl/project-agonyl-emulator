#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.IO;
using System.Linq;
using Agonyl.Shared.Data.Game;

namespace Agonyl.Shared.Util
{
    public class NPCDataParser : BinaryFileParser
    {
        public NPCDataParser(string filePath)
            : base(filePath)
        {
        }

        public void ParseData(ref NPCData npcData)
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            npcData.Name = System.Text.Encoding.Default.GetString(fileBytes.Take(20).ToArray());
        }
    }
}
