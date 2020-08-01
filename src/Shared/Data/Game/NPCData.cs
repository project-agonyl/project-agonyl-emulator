#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data.Game
{
    public class NPCData
    {
        public string Name { get; set; }

        public ushort Id { get; set; }

        public ushort RespawnRate { get; set; }

        public byte AttackTypeInfo { get; set; }

        public byte TargetSelectionInfo { get; set; }

        public byte Defense { get; set; }

        public byte AdditionalDefense { get; set; }

        public NPCAttack[] Attacks = new NPCAttack[3];

        public ushort AttackSpeedLow { get; set; }

        public ushort AttackSpeedHigh { get; set; }

        public ushort MovementSpeed { get; set; }

        public byte Level { get; set; }

        public ushort PlayerExp { get; set; }

        public byte Appearance { get; set; }

        public uint HP { get; set; }

        public ushort BlueAttackDefense { get; set; }

        public ushort RedAttackDefense { get; set; }

        public ushort GreyAttackDefense { get; set; }

        public ushort MercenaryExp { get; set; }
    }
}
