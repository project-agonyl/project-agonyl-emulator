#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.IO;
using System.Linq;
using Agonyl.Shared.Data.Game;

namespace Agonyl.Shared.Util
{
    public class QuestParser : BinaryFileParser
    {
        public QuestParser(string filePath)
            : base(filePath)
        {
        }

        public Quest ParseFile()
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            var quest = new Quest()
            {
                Id = BitConverter.ToUInt16(fileBytes.Take(2).ToArray(), 0),
                StartNpcId = BitConverter.ToUInt16(fileBytes.Skip(4).Take(2).ToArray(), 0),
                SubmitNpcId = BitConverter.ToUInt16(fileBytes.Skip(8).Take(2).ToArray(), 0),
                LowLevel = BitConverter.ToUInt16(fileBytes.Skip(32).Take(2).ToArray(), 0),
                HighLevel = BitConverter.ToUInt16(fileBytes.Skip(36).Take(2).ToArray(), 0),
                Experience = BitConverter.ToUInt32(fileBytes.Skip(80).Take(4).ToArray(), 0),
                Woonz = BitConverter.ToUInt32(fileBytes.Skip(84).Take(4).ToArray(), 0),
                Lore = BitConverter.ToUInt32(fileBytes.Skip(88).Take(4).ToArray(), 0),
                NextQuestId = 0xffff,
            };
            if (fileBytes.Length == 798)
            {
                quest.NextQuestId = BitConverter.ToUInt16(fileBytes.Skip(786).Take(2).ToArray(), 0);
            }

            for (var i = 0; i < 3; i++)
            {
                quest.QuestItemRewards[i] = new QuestItemReward()
                {
                    Id = BitConverter.ToUInt32(fileBytes.Skip(44 + (i * 4)).Take(4).ToArray(), 0),
                    Count = BitConverter.ToUInt16(fileBytes.Skip(68 + (i * 4)).Take(2).ToArray(), 0),
                };
            }

            for (var i = 0; i < 6; i++)
            {
                quest.QuestRequirements[i] = new QuestRequirement()
                {
                    MapId = BitConverter.ToUInt16(fileBytes.Skip(100 + (i * 96)).Take(2).ToArray(), 0),
                    MonsterId = BitConverter.ToUInt16(fileBytes.Skip(112 + (i * 96)).Take(2).ToArray(), 0),
                    MonsterCount = BitConverter.ToUInt16(fileBytes.Skip(116 + (i * 96)).Take(2).ToArray(), 0),
                    QuestItem = BitConverter.ToUInt16(fileBytes.Skip(124 + (i * 96)).Take(2).ToArray(), 0),
                    QuestItemCount = BitConverter.ToUInt16(fileBytes.Skip(172 + (i * 96)).Take(2).ToArray(), 0),
                };
            }

            return quest;
        }
    }
}
