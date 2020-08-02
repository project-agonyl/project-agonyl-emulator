#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.IO;
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
                Id = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2)),
                StartNpcId = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 4)),
                SubmitNpcId = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 8)),
                LowLevel = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 32)),
                HighLevel = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 36)),
                Experience = Functions.BytesToUInt32(Functions.SkipAndTakeLinqShim(ref fileBytes, 4, 80)),
                Woonz = Functions.BytesToUInt32(Functions.SkipAndTakeLinqShim(ref fileBytes, 4, 84)),
                Lore = Functions.BytesToUInt32(Functions.SkipAndTakeLinqShim(ref fileBytes, 4, 88)),
                NextQuestId = 0xffff,
            };
            if (fileBytes.Length == 798)
            {
                quest.NextQuestId = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 786));
            }

            for (var i = 0; i < 3; i++)
            {
                quest.QuestItemRewards[i] = new QuestItemReward()
                {
                    Id = Functions.BytesToUInt32(Functions.SkipAndTakeLinqShim(ref fileBytes, 4, 44 + (i * 4))),
                    Count = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 68 + (i * 4))),
                };
            }

            for (var i = 0; i < 6; i++)
            {
                quest.QuestRequirements[i] = new QuestRequirement()
                {
                    MapId = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 100 + (i * 96))),
                    MonsterId = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 112 + (i * 96))),
                    MonsterCount = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 116 + (i * 96))),
                    QuestItem = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 124 + (i * 96))),
                    QuestItemCount = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 172 + (i * 96))),
                };
            }

            return quest;
        }
    }
}
