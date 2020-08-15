#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using Agonyl.Game.Data;
using Agonyl.Shared.Const;
using Agonyl.Shared.Data;
using Agonyl.Shared.Network;
using MySql.Data.MySqlClient;

namespace Agonyl.Game.Database
{
    public class ASD : Shared.Database.ASD
    {
        public Character GetCharacter(string name)
        {
            using (var conn = this.GetConnection())
            {
                using (var mc = new MySqlCommand("SELECT * FROM `charac0` WHERE `c_id` = @name", conn))
                {
                    mc.Parameters.AddWithValue("@name", name);

                    using (var reader = mc.ExecuteReader())
                    {
                        reader.Read();
                        var info = new Shared.Database.Model.Charac0
                        {
                            c_id = reader["c_id"].ToString(),
                            c_sheadera = reader["c_sheadera"].ToString(),
                            c_sheaderb = reader["c_sheaderb"].ToString(),
                            c_sheaderc = reader["c_sheaderc"].ToString(),
                            c_headera = reader["c_headera"].ToString(),
                            c_headerb = reader["c_headerb"].ToString(),
                            c_headerc = reader["c_headerc"].ToString(),
                            m_body = reader["m_body"].ToString(),
                        };
                        var character = new Character()
                        {
                            Name = info.c_id,
                            Type = (CharacterType)Convert.ToByte(info.c_sheaderb),
                            Level = Convert.ToByte(info.c_sheaderc),
                            Exp = Convert.ToUInt32(info.GetExp()),
                            MapId = Convert.ToUInt16(info.c_headerb.Split(';')[0]),
                            CurrentPostion = Position.GetPositionFromNumber(Convert.ToUInt16(info.c_headerb.Split(';')[1])),
                            SkillLevel1 = Convert.ToUInt32(info.GetSkill().Split(';')[0]),
                            SkillLevel2 = Convert.ToUInt32(info.GetSkill().Split(';')[1]),
                            SkillLevel3 = Convert.ToUInt32(info.GetSkill().Split(';')[2]),
                            PKCount = Convert.ToUInt32(info.GetPk()),
                            RTime = Convert.ToUInt32(info.GetRTime()),
                            Nation = Town.Temoz, // TODO: Read from DB
                            Woonz = Convert.ToUInt32(info.c_headerc),
                            StoredHP = Convert.ToUInt32(info.c_headera.Split(';')[8]), // TODO: Check if correct
                            StoredMP = Convert.ToUInt32(info.c_headera.Split(';')[9]), // TODO: Check if correct
                            Lore = Convert.ToUInt32(info.GetLore()),
                            Str = Convert.ToUInt16(info.c_headera.Split(';')[0]),
                            Int = Convert.ToUInt16(info.c_headera.Split(';')[1]),
                            Dex = Convert.ToUInt16(info.c_headera.Split(';')[2]),
                            Vital = Convert.ToUInt16(info.c_headera.Split(';')[3]),
                            Mana = Convert.ToUInt16(info.c_headera.Split(';')[4]),
                            RemainingPoints = Convert.ToUInt16(info.c_headera.Split(';')[5]),
                            HP = Convert.ToUInt16(info.c_headera.Split(';')[6]),
                            MP = Convert.ToUInt16(info.c_headera.Split(';')[7]),
                            HPCapacity = Convert.ToUInt32(info.c_headera.Split(';')[8]), // TODO: Calculate based on vitality points
                            MPCapacity = Convert.ToUInt32(info.c_headera.Split(';')[9]), // TODO: Calculate based on mana points
                        };
                        character.SetPlayerState(PlayerState.STANDBY);
                        byte currentWearIndex = 0;
                        var itemArray = info.GetWear().Split(';');
                        for (var j = 0; j < itemArray.Length; j += 3)
                        {
                            if (currentWearIndex == 10 || j + 2 >= itemArray.Length)
                            {
                                break;
                            }

                            if (!decimal.TryParse(itemArray[j], out _) ||
                                !decimal.TryParse(itemArray[j + 1], out _) ||
                                !decimal.TryParse(itemArray[j + 2], out _) ||
                                !GameServer.Instance.GameData.Items.ContainsKey(Convert.ToUInt32(itemArray[j]) & 0x3FFF))
                            {
                                continue;
                            }
                            else
                            {
                                var wearItem = new WearItem(
                                    Convert.ToUInt32(itemArray[j]),
                                    Convert.ToUInt32(itemArray[j + 1]),
                                    Convert.ToUInt32(itemArray[j + 2]),
                                    (int)Convert.ToUInt32(itemArray[j + 2]),
                                    GameServer.Instance.GameData.Items[Convert.ToUInt32(itemArray[j]) & 0x3FFF].SlotIndex);

                                character.Inventory.AddWearItem(currentWearIndex, wearItem);
                            }

                            currentWearIndex++;
                        }

                        byte currentInventoryIndex = 0;
                        itemArray = info.GetInventory().Split(';');
                        for (var j = 0; j < itemArray.Length; j += 4)
                        {
                            if (currentInventoryIndex == 30 || j + 3 >= itemArray.Length)
                            {
                                break;
                            }

                            if (!decimal.TryParse(itemArray[j], out _) ||
                                !decimal.TryParse(itemArray[j + 1], out _) ||
                                !decimal.TryParse(itemArray[j + 2], out _) ||
                                !decimal.TryParse(itemArray[j + 3], out _) ||
                                !GameServer.Instance.GameData.Items.ContainsKey(Convert.ToUInt32(itemArray[j]) & 0x3FFF))
                            {
                                continue;
                            }
                            else
                            {
                                var inventoryItem = new InventoryItem(
                                    Convert.ToUInt32(itemArray[j]),
                                    Convert.ToUInt32(itemArray[j + 1]),
                                    Convert.ToUInt32(itemArray[j + 2]),
                                    (int)Convert.ToUInt32(itemArray[j + 2]),
                                    Convert.ToByte(itemArray[j + 3]));
                                character.Inventory.AddInventoryItem(Convert.ToByte(itemArray[j + 3]), inventoryItem);
                            }

                            currentInventoryIndex++;
                        }

                        character.ActivePet = default(PET_INFO);
                        character.ActivePet.PetId = default(PET_ID);
                        var petArray = info.GetActivePet().Split(';');
                        if (petArray[0] != string.Empty)
                        {
                            character.ActivePet.PetId.PetPtr = Convert.ToUInt32(petArray[3]); // Fix later by getting unique ID maintained by Game Server
                            character.ActivePet.PetId.PetCode = Convert.ToUInt32(petArray[0]);
                            character.ActivePet.Option1 = Convert.ToUInt32(petArray[1]);
                            character.ActivePet.Option2 = Convert.ToUInt32(petArray[2]);
                            character.ActivePet.SerialKey = Convert.ToUInt32(petArray[3]);
                        }
                        else
                        {
                            character.ActivePet = new PET_INFO();
                        }
                        character.PetInventory = new PET_INFO[5];
                        petArray = info.GetPetInventory().Split(';');
                        var currentPetIndex = 0;
                        for (var i = 0; i < petArray.Length; i += 4)
                        {
                            if (currentPetIndex == 5 || i + 3 >= petArray.Length)
                            {
                                break;
                            }

                            character.PetInventory[currentPetIndex] = default(PET_INFO);
                            character.PetInventory[currentPetIndex].PetId = default(PET_ID);
                            character.PetInventory[currentPetIndex].PetId.PetPtr = Convert.ToUInt32(petArray[i + 3]); // Fix later to unique ID maintained by Game Server
                            character.PetInventory[currentPetIndex].PetId.PetCode = Convert.ToUInt32(petArray[i]);
                            character.PetInventory[currentPetIndex].Option1 = Convert.ToUInt32(petArray[i + 1]);
                            character.PetInventory[currentPetIndex].Option2 = Convert.ToUInt32(petArray[i + 2]);
                            character.PetInventory[currentPetIndex].SerialKey = Convert.ToUInt32(petArray[i + 3]);
                            currentPetIndex++;
                        }

                        character.UpdateCalculatedStats();
                        return character;
                    }
                }
            }
        }
    }
}
