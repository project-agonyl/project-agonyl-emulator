#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using Agonyl.Game.Network;
using Agonyl.Shared.Const;
using Agonyl.Shared.Data;
using Agonyl.Shared.Network;

namespace Agonyl.Game.Data
{
    public class Character
    {
        public Map Map;

        public Character()
        {
            this.Handle = GameServer.Instance.World.CreateCharacterHandle();
        }

        public int Handle { get; private set; }

        public PlayerState CurrentState { get; set; }

        public string IPAddress { get; set; }

        public Account Account { get; set; }

        public string Name { get; set; }

        public byte Level { get; set; }

        public Position CurrentPostion { get; set; }

        public Position DestinationPosition { get; set; }

        public uint Exp { get; set; }

        public uint Woonz { get; set; }

        public uint Lore { get; set; }

        public ushort MapId { get; set; }

        public uint PKCount { get; set; }

        public uint RTime { get; set; }

        public CharacterType Type { get; set; }

        public byte MovementSpeed { get; set; }

        public uint StoredHP { get; set; }

        public uint StoredMP { get; set; }

        public uint Rank { get; set; }

        public uint KnightIndex { get; set; }

        public Town Nation { get; set; }

        public uint SkillLevel1 { get; set; }

        public uint SkillLevel2 { get; set; }

        public uint SkillLevel3 { get; set; }

        public ushort Str { get; set; }

        public ushort Int { get; set; }

        public ushort Dex { get; set; }

        public ushort Vital { get; set; }

        public ushort Mana { get; set; }

        public ushort RemainingPoints { get; set; }

        public ushort HP { get; set; }

        public ushort MP { get; set; }

        public uint HPCapacity { get; set; }

        public uint MPCapacity { get; set; }

        public CHARACTER_CALCULATED_STAT CalculatedStat;

        public ITEM_WEAR[] Wear;

        public ITEM_INVENTORY[] Inventory;

        public PET_INFO ActivePet;

        public PET_INFO[] PetInventory;

        public GameConnection GameConnection { get; set; }

        public void GetMsgCharacterInfo(out CHARACTER_INFO info)
        {
            info = default(CHARACTER_INFO);
            info.Name = this.Name;
            info.Type = (byte)this.Type;
            info.Level = this.Level;
            info.Exp = this.Exp;
            info.MapIndex = this.MapId;
            info.CellIndex = this.CurrentPostion.GetNumberRepresentation();
            info.SkillList = default(SKILL_INFO);
            info.SkillList.Skill0 = this.SkillLevel1;
            info.SkillList.Skill1 = this.SkillLevel2;
            info.SkillList.Skill2 = this.SkillLevel3;
            info.PKCount = this.PKCount;
            info.RTime = this.RTime;
            info.SInfo = default(SOCIAL_INFO);
            info.SInfo.Nation = (uint)this.Nation;
            info.SInfo.Rank = 0;
            info.SInfo.KnightIndex = 0;
            info.Money = this.Woonz;
            info.StoredHp = this.StoredHP;
            info.StoredMp = this.StoredMP;
            info.Lore = this.Lore;
        }

        public void GetMsgCharacterStat(out CHARACTER_STAT stat)
        {
            stat = default(CHARACTER_STAT);
            stat.Str = this.Str;
            stat.Magic = this.Int;
            stat.Dex = this.Dex;
            stat.Vit = this.Vital;
            stat.Mana = this.Mana;
            stat.Point = this.RemainingPoints;
            stat.HP = this.HP;
            stat.MP = this.MP;
            stat.HPCapacity = this.HPCapacity;
            stat.MPCapacity = this.MPCapacity;
            stat.CalculatedStat = this.CalculatedStat;
        }

        public void GetMsgItemWear(out ITEM_WEAR[] wearList)
        {
            wearList = this.Wear;
        }

        public void GetMsgItemInventory(out ITEM_INVENTORY[] inventoryList)
        {
            inventoryList = this.Inventory;
        }

        public void GetMsgActivePet(out PET_INFO petInfo)
        {
            petInfo = this.ActivePet;
        }

        public void GetMsgPetInventory(out PET_INFO[] petInfo)
        {
            petInfo = this.PetInventory;
        }
    }
}
