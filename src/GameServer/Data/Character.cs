#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using Agonyl.Game.Network;
using Agonyl.Shared.Database.Model;
using Agonyl.Shared.Network;

namespace Agonyl.Game.Data
{
    public class Character
    {
        public Account Account { get; set; }

        public string Name { get; set; }

        public Charac0 Info { get; set; }

        public GameConnection GameConnection { get; set; }

        public void GetMsgCharacterInfo(out CHARACTER_INFO info)
        {
            info = default(CHARACTER_INFO);
            info.Name = this.Info.c_id;
            info.Type = Convert.ToByte(this.Info.c_sheaderb);
            info.Level = Convert.ToUInt16(this.Info.c_sheaderc);
            info.Exp = Convert.ToUInt32(this.Info.GetExp());
            info.MapIndex = Convert.ToUInt32(this.Info.c_headerb.Split(';')[0]);
            info.CellIndex = Convert.ToUInt32(this.Info.c_headerb.Split(';')[1]);
            info.SkillList = default(SKILL_INFO);
            info.SkillList.Skill0 = Convert.ToUInt32(this.Info.GetSkill().Split(';')[0]);
            info.SkillList.Skill1 = Convert.ToUInt32(this.Info.GetSkill().Split(';')[1]);
            info.SkillList.Skill2 = Convert.ToUInt32(this.Info.GetSkill().Split(';')[2]);
            info.PKCount = Convert.ToUInt32(this.Info.GetPk());
            info.RTime = Convert.ToUInt32(this.Info.GetRTime());
            info.SInfo = default(SOCIAL_INFO);
            info.SInfo.Nation = 0; // For now all are temoz
            info.SInfo.Rank = 0;
            info.SInfo.KnightIndex = 0;
            info.Money = Convert.ToUInt32(this.Info.c_headerc);
            info.StoredHp = Convert.ToUInt32(this.Info.c_headera.Split(';')[8]); // Check if correct
            info.StoredMp = Convert.ToUInt32(this.Info.c_headera.Split(';')[9]); // Check if correct
            info.Lore = Convert.ToUInt32(this.Info.GetLore());
        }

        public void GetMsgCharacterStat(out CHARACTER_STAT stat)
        {
            stat = default(CHARACTER_STAT);
            stat.Str = Convert.ToUInt16(this.Info.c_headera.Split(';')[0]);
            stat.Magic = Convert.ToUInt16(this.Info.c_headera.Split(';')[1]);
            stat.Dex = Convert.ToUInt16(this.Info.c_headera.Split(';')[2]);
            stat.Vit = Convert.ToUInt16(this.Info.c_headera.Split(';')[3]);
            stat.Mana = Convert.ToUInt16(this.Info.c_headera.Split(';')[4]);
            stat.Point = Convert.ToUInt16(this.Info.c_headera.Split(';')[5]);
            stat.HP = Convert.ToUInt16(this.Info.c_headera.Split(';')[6]);
            stat.MP = Convert.ToUInt16(this.Info.c_headera.Split(';')[7]);
            stat.HPCapacity = Convert.ToUInt32(this.Info.c_headera.Split(';')[8]); // Calculate based on vitality points
            stat.MPCapacity = Convert.ToUInt32(this.Info.c_headera.Split(';')[9]); // Calculate based on mana points
            stat.CalculatedStat = default(CHARACTER_CALCULATED_STAT);
            stat.CalculatedStat.MaxHp = Convert.ToUInt16(this.Info.c_headera.Split(';')[6]); // Calculate based on level
            stat.CalculatedStat.MaxMp = Convert.ToUInt16(this.Info.c_headera.Split(';')[7]); // Calculate based on level
        }

        public void GetMsgItemWear(out ITEM_WEAR[] wearList)
        {
            wearList = new ITEM_WEAR[10];
            var currentWearIndex = 0;
            var itemArray = this.Info.GetWear().Split(';');
            for (var j = 0; j < itemArray.Length; j += 3)
            {
                if (currentWearIndex == 10 || j + 2 >= itemArray.Length)
                {
                    break;
                }

                if (!decimal.TryParse(itemArray[j], out _))
                {
                    continue;
                }

                if (!GameServer.Instance.GameData.Items.ContainsKey(Convert.ToUInt32(itemArray[j]) & 0x3FFF))
                {
                    continue;
                }

                wearList[currentWearIndex] = default(ITEM_WEAR);
                wearList[currentWearIndex].Item = default(ITEM);
                wearList[currentWearIndex].Item.ItemId = default(ITEM_ID);
                wearList[currentWearIndex].Item.ItemId.ItemCode = Convert.ToUInt32(itemArray[j]);
                wearList[currentWearIndex].Item.ItemOption = 0;
                if (decimal.TryParse(itemArray[j + 1], out _))
                {
                    wearList[currentWearIndex].Item.ItemOption = Convert.ToUInt32(itemArray[j + 1]);
                }

                wearList[currentWearIndex].Item.ItemId.ItemPtr = 0;
                wearList[currentWearIndex].Item.ItemKey = 0;
                if (decimal.TryParse(itemArray[j + 2], out _))
                {
                    wearList[currentWearIndex].Item.ItemKey = Convert.ToUInt32(itemArray[j + 2]);
                    wearList[currentWearIndex].Item.ItemId.ItemPtr = Convert.ToUInt32(itemArray[j + 2]);
                }

                wearList[currentWearIndex].WearIndex = GameServer.Instance.GameData.Items[Convert.ToUInt32(itemArray[j]) & 0x3FFF].SlotIndex;
                currentWearIndex++;
            }
        }

        public void GetMsgItemInventory(out ITEM_INVENTORY[] inventoryList)
        {
            inventoryList = new ITEM_INVENTORY[30];
            var currentInventoryIndex = 0;
            var itemArray = this.Info.GetInventory().Split(';');
            for (var j = 0; j < itemArray.Length; j += 4)
            {
                if (currentInventoryIndex == 30 || j + 3 >= itemArray.Length)
                {
                    break;
                }

                if (!decimal.TryParse(itemArray[j], out _))
                {
                    continue;
                }

                if (!GameServer.Instance.GameData.Items.ContainsKey(Convert.ToUInt32(itemArray[j]) & 0x3FFF))
                {
                    continue;
                }

                inventoryList[currentInventoryIndex] = default(ITEM_INVENTORY);
                inventoryList[currentInventoryIndex].Item = default(ITEM);
                inventoryList[currentInventoryIndex].Item.ItemId = default(ITEM_ID);
                inventoryList[currentInventoryIndex].Item.ItemId.ItemCode = Convert.ToUInt32(itemArray[j]);
                inventoryList[currentInventoryIndex].Item.ItemOption = 0;
                if (decimal.TryParse(itemArray[j + 1], out _))
                {
                    inventoryList[currentInventoryIndex].Item.ItemOption = Convert.ToUInt32(itemArray[j + 1]);
                }

                inventoryList[currentInventoryIndex].Item.ItemId.ItemPtr = 0;
                inventoryList[currentInventoryIndex].Item.ItemKey = 0;
                if (decimal.TryParse(itemArray[j + 2], out _))
                {
                    inventoryList[currentInventoryIndex].Item.ItemKey = Convert.ToUInt32(itemArray[j + 2]);
                    inventoryList[currentInventoryIndex].Item.ItemId.ItemPtr = Convert.ToUInt32(itemArray[j + 2]);
                }

                inventoryList[currentInventoryIndex].InventoryIndex = Convert.ToUInt32(itemArray[j + 3]);
                currentInventoryIndex++;
            }
        }

        public ushort GetMapNumberFromDbInfo()
        {
            return Convert.ToUInt16(this.Info.c_headerb.Split(';')[0]);
        }

        public void GetMsgActivePet(out PET_INFO petInfo)
        {
            petInfo = default(PET_INFO);
            petInfo.PetId = default(PET_ID);
            var petArray = this.Info.GetActivePet().Split(';');
            petInfo.PetId.PetPtr = Convert.ToUInt32(petArray[3]); // Fix later to unique ID maintained by Game Server
            petInfo.PetId.PetCode = Convert.ToUInt32(petArray[0]);
            petInfo.Option1 = Convert.ToUInt32(petArray[1]);
            petInfo.Option2 = Convert.ToUInt32(petArray[2]);
            petInfo.SerialKey = Convert.ToUInt32(petArray[3]);
        }

        public void GetMsgPetInventory(out PET_INFO[] petInfo)
        {
            petInfo = new PET_INFO[5];
            var petArray = this.Info.GetPetInventory().Split(';');
            var currentPetIndex = 0;
            for (var i = 0; i < petArray.Length; i += 4)
            {
                if (currentPetIndex == 5 || i + 3 >= petArray.Length)
                {
                    break;
                }

                petInfo[currentPetIndex] = default(PET_INFO);
                petInfo[currentPetIndex].PetId = default(PET_ID);
                petInfo[currentPetIndex].PetId.PetPtr = Convert.ToUInt32(petArray[i + 3]); // Fix later to unique ID maintained by Game Server
                petInfo[currentPetIndex].PetId.PetCode = Convert.ToUInt32(petArray[i]);
                petInfo[currentPetIndex].Option1 = Convert.ToUInt32(petArray[i + 1]);
                petInfo[currentPetIndex].Option2 = Convert.ToUInt32(petArray[i + 2]);
                petInfo[currentPetIndex].SerialKey = Convert.ToUInt32(petArray[i + 3]);
                currentPetIndex++;
            }
        }
    }
}
