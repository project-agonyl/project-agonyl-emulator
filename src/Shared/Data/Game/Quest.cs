#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data.Game
{
    public class Quest
    {
        public ushort Id { get; set; }

        public ushort StartNpcId { get; set; }

        public ushort SubmitNpcId { get; set; }

        public ushort LowLevel { get; set; }

        public ushort HighLevel { get; set; }

        public uint Experience { get; set; }

        public uint Woonz { get; set; }

        public uint Lore { get; set; }

        public QuestRequirement[] QuestRequirements = new QuestRequirement[6];

        public QuestItemReward[] QuestItemRewards = new QuestItemReward[3];

        public ushort NextQuestId { get; set; }
    }
}
