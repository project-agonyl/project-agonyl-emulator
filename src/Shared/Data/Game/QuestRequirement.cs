#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data.Game
{
    public class QuestRequirement
    {
        public ushort MapId { get; set; }

        public ushort MapName { get; set; }

        public ushort MonsterId { get; set; }

        public ushort MonsterName { get; set; }

        public ushort MonsterCount { get; set; }

        public ushort QuestItem { get; set; }

        public ushort QuestItemCount { get; set; }
    }
}
