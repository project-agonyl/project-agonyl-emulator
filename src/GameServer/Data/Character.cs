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
            this.Inventory = new Inventory(this);
        }

        public int Handle { get; private set; }

        public uint CurrentTickCount { get; set; }

        public uint PreviousTickCount { get; set; }

        public uint ClientTick { get; set; }

        public uint CurrentServerTick { get; set; }

        public uint PreviousServerTick { get; set; }

        public uint TickErrorCount { get; set; }

        public PlayerState CurrentState { get; private set; }

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

        public ushort HitAttack { get; set; }

        public ushort MagicAttack { get; set; }

        public ushort Defense { get; set; }

        public ushort FireAttack { get; set; }

        public ushort FireDefence { get; set; }

        public ushort IceAttack { get; set; }

        public ushort IceDefense { get; set; }

        public ushort LightAttack { get; set; }

        public ushort LightDefense { get; set; }

        public ushort MaxHp { get; set; }

        public ushort MaxMp { get; set; }

        public ushort HitAddition { get; set; }

        public ushort MagicAddition { get; set; }

        public ushort SkillDuration { get; set; }

        public ushort HPAbsorption { get; set; }

        public ushort MPAbsorption { get; set; }

        public ushort ReduceHPMPUsage { get; set; }

        public ushort CritHitEvasion { get; set; }

        public ushort WzAcquisition { get; set; }

        public ushort CritHitRate { get; set; }

        public ushort Accuracy { get; set; }

        public ushort Evasion { get; set; }

        public ushort MagicEvasion { get; set; }

        public Inventory Inventory;

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
            stat.CalculatedStat = new CHARACTER_CALCULATED_STAT()
            {
                HitAttack = this.HitAttack,
                MagicAttack = this.MagicAttack,
                Defense = this.Defense,
                FireAttack = this.FireAttack,
                FireDefence = this.FireDefence,
                IceAttack = this.IceAttack,
                IceDefense = this.IceDefense,
                LightAttack = this.LightAttack,
                LightDefense = this.LightDefense,
                MaxHp = this.MaxHp,
                MaxMp = this.MaxMp,
                HitAddition = this.HitAddition,
                MagAddition = this.MagicAddition,
            };
        }

        public void GetMsgItemWear(out ITEM_WEAR[] wearList)
        {
            wearList = this.Inventory.GetWearMsgStructure();
        }

        public void GetMsgItemInventory(out ITEM_INVENTORY[] inventoryList)
        {
            inventoryList = this.Inventory.GetInventoryMsgStructure();
        }

        public void GetMsgActivePet(out PET_INFO petInfo)
        {
            petInfo = this.ActivePet;
        }

        public void GetMsgPetInventory(out PET_INFO[] petInfo)
        {
            petInfo = this.PetInventory;
        }

        public void SetPlayerState(PlayerState state)
        {
            this.CurrentState = state;
        }

        public void UpdateCalculatedStats()
        {
            this.MaxHp = this.HP; // TODO: Calculate based on level and stats
            this.MaxMp = this.MP; // TODO: Calculate based on level and stats

            this.Defense = (ushort)(this.Dex / 2);
            this.HitAttack = (ushort)(this.Str / 2);
            this.MagicAttack = (ushort)(this.Int / 2);
            foreach (var wearItem in this.Inventory.GetWearItems())
            {
                if (wearItem.BaseId != 0)
                {
                    var currentItemData = GameServer.Instance.GameData.Items[wearItem.BaseId];

                    switch ((SlotType)wearItem.Slot)
                    {
                        case SlotType.Shield:
                            if (this.Type == CharacterType.Paladin)
                            {
                                this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].Attribute;
                                this.IceDefense += wearItem.BlueOption;
                                this.FireDefence += wearItem.RedOption;
                                this.LightDefense += wearItem.GreyOption;
                                if (wearItem.HasAdditionalAttribute)
                                {
                                    this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].AdditionalAttribute;
                                }
                            }

                            break;

                        case SlotType.PaladinSword:
                            this.HitAttack += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].Attribute;
                            this.IceAttack += wearItem.BlueOption;
                            this.FireAttack += wearItem.RedOption;
                            this.LightAttack += wearItem.GreyOption;
                            if (wearItem.HasAdditionalAttribute)
                            {
                                this.HitAddition += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].AdditionalAttribute;
                            }

                            break;

                        case SlotType.WarriorSword:
                            // Warrior sword and staff have same slot but attack has to be different
                            if (this.Type == CharacterType.Mage)
                            {
                                this.MagicAttack += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].Attribute;
                                if (wearItem.HasAdditionalAttribute)
                                {
                                    this.MagicAddition += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].AdditionalAttribute;
                                }
                            }
                            else
                            {
                                this.HitAttack += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].Attribute;
                                if (wearItem.HasAdditionalAttribute)
                                {
                                    this.HitAddition += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].AdditionalAttribute;
                                }
                            }

                            this.IceAttack += wearItem.BlueOption;
                            this.FireAttack += wearItem.RedOption;
                            this.LightAttack += wearItem.GreyOption;
                            break;

                        case SlotType.Necklace:
                            this.HitAttack += currentItemData.IT1ItemProperty.Attribute;
                            this.IceAttack += currentItemData.IT1ItemProperty.BlueOption;
                            this.FireAttack += currentItemData.IT1ItemProperty.RedOption;
                            this.LightAttack += currentItemData.IT1ItemProperty.GreyOption;
                            break;

                        case SlotType.Ring:
                            this.Defense += currentItemData.IT1ItemProperty.Attribute;
                            this.IceDefense += currentItemData.IT1ItemProperty.BlueOption;
                            this.FireDefence += currentItemData.IT1ItemProperty.RedOption;
                            this.LightDefense += currentItemData.IT1ItemProperty.GreyOption;
                            break;

                        case SlotType.Armor:
                            this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].Attribute;
                            this.IceDefense += wearItem.BlueOption;
                            this.FireDefence += wearItem.RedOption;
                            this.LightDefense += wearItem.GreyOption;
                            if (wearItem.HasAdditionalAttribute)
                            {
                                this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].AdditionalAttribute;
                            }

                            break;

                        case SlotType.Helmet:
                            this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].Attribute;
                            this.MPAbsorption += wearItem.BlueOption;
                            this.HPAbsorption += wearItem.RedOption;
                            this.ReduceHPMPUsage += wearItem.GreyOption;
                            if (wearItem.HasAdditionalAttribute)
                            {
                                this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].AdditionalAttribute;
                            }

                            break;

                        case SlotType.Gloves:
                            this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].Attribute;
                            this.SkillDuration += (ushort)(wearItem.BlueOption * 5);
                            this.HitAttack += wearItem.RedOption;
                            this.MagicAttack += wearItem.GreyOption;
                            if (wearItem.HasAdditionalAttribute)
                            {
                                this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].AdditionalAttribute;
                            }

                            break;

                        case SlotType.Boots:
                            this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].Attribute;
                            this.CritHitEvasion += wearItem.BlueOption;
                            this.WzAcquisition += (ushort)(wearItem.RedOption * 2);
                            this.CritHitRate += wearItem.GreyOption;
                            if (wearItem.HasAdditionalAttribute)
                            {
                                this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].AdditionalAttribute;
                            }

                            break;

                        case SlotType.Pants:
                            this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].Attribute;
                            this.Accuracy += wearItem.BlueOption;
                            this.Evasion += wearItem.RedOption;
                            this.MagicEvasion += wearItem.GreyOption;
                            if (wearItem.HasAdditionalAttribute)
                            {
                                this.Defense += currentItemData.IT0ItemProperty.ItemLevel[wearItem.Level - 1].AdditionalAttribute;
                            }

                            break;
                    }
                }
            }
        }
    }
}
