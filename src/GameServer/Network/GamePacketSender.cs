#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.Linq;
using Agonyl.Shared.Const;
using Agonyl.Shared.Data;
using Agonyl.Shared.Network;

namespace Agonyl.Game.Network
{
    public static class Send
    {
        public static void S2C_CHAR_LIST(GameConnection conn, Data.Account account)
        {
            var characters = GameServer.Instance.ASDDatabase.GetCharacterList(account.Username).ToList();
            var characterListPacket = new MSG_S2C_CHARACTER_LIST();
            for (var i = 0; i < 5; i++)
            {
                if (i < characters.Count)
                {
                    characterListPacket.CharInfo[i] = new ACL_CHARACTER_INFO();
                    characterListPacket.CharInfo[i].CharacterName = characters[i].c_id;
                    characterListPacket.CharInfo[i].Used = 1;
                    characterListPacket.CharInfo[i].Town = 0;
                    characterListPacket.CharInfo[i].Class = Convert.ToByte(characters[i].c_sheaderb);
                    characterListPacket.CharInfo[i].Level = Convert.ToUInt32(characters[i].c_sheaderc);
                    characterListPacket.CharInfo[i].WearList = new ACL_ITEM_WEAR[10];
                    var itemArray = characters[i].GetWear().Replace("_1WEAR=", string.Empty).Split(';');
                    var wearIndex = 0;
                    for (var j = 0; j < itemArray.Length; j += 3)
                    {
                        if (wearIndex == 10)
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

                        uint option = 0;
                        if (decimal.TryParse(itemArray[j + 1], out _))
                        {
                            option = Convert.ToUInt32(itemArray[j + 1]);
                        }

                        characterListPacket.CharInfo[i].WearList[wearIndex] = new ACL_ITEM_WEAR();
                        characterListPacket.CharInfo[i].WearList[wearIndex].ItemPtr = 0;
                        characterListPacket.CharInfo[i].WearList[wearIndex].ItemCode = Convert.ToUInt32(itemArray[j]);
                        characterListPacket.CharInfo[i].WearList[wearIndex].ItemOption = option;
                        characterListPacket.CharInfo[i].WearList[wearIndex].WearIndex = GameServer.Instance.GameData.Items[Convert.ToUInt32(itemArray[j]) & 0x3FFF].SlotIndex;
                        wearIndex++;
                    }
                }
                else
                {
                    characterListPacket.CharInfo[i] = new ACL_CHARACTER_INFO();
                    characterListPacket.CharInfo[i].Class = 255;
                }
            }

            conn.Send(characterListPacket.Serialize());
        }

        public static void S2C_ERROR(GameConnection conn, ushort errorCode, string error)
        {
            var errorMsg = new MSG_S2C_ERROR();
            errorMsg.ErrorCode = errorCode;
            errorMsg.ErrorMsg = error;
            conn.Send(errorMsg.Serialize());
        }

        public static void S2C_ANS_CREATE_PLAYER(GameConnection conn, string name, byte type)
        {
            var msg = new MSG_S2C_CHARACTER_CREATE_ACK();
            msg.CharacterName = name;
            msg.CharacterType = type;
            var starterGear = GameServer.Instance.Conf.StarterGearWarrior;
            switch ((CharacterType)type)
            {
                case CharacterType.Paladin:
                    starterGear = GameServer.Instance.Conf.StarterGearHK;
                    break;

                case CharacterType.Mage:
                    starterGear = GameServer.Instance.Conf.StarterGearMage;
                    break;

                case CharacterType.Archer:
                    starterGear = GameServer.Instance.Conf.StarterGearArcher;
                    break;
            }

            for (var i = 0; i < starterGear.Count; i++)
            {
                if (i == 10)
                {
                    break;
                }

                var itemArray = starterGear[i].Split(';');
                if (!decimal.TryParse(itemArray[0], out _))
                {
                    continue;
                }

                if (!GameServer.Instance.GameData.Items.ContainsKey(Convert.ToUInt32(itemArray[0]) & 0x3FFF))
                {
                    continue;
                }

                uint option = 0;
                if (itemArray.Length >= 2 && decimal.TryParse(itemArray[1], out _))
                {
                    option = Convert.ToUInt32(itemArray[1]);
                }

                msg.WearList[i].ItemPtr = 0;
                msg.WearList[i].ItemCode = Convert.ToUInt32(itemArray[0]);
                msg.WearList[i].ItemOption = option;
                msg.WearList[i].WearIndex = GameServer.Instance.GameData.Items[Convert.ToUInt32(itemArray[0]) & 0x3FFF].SlotIndex;
            }

            conn.Send(msg.Serialize());
        }

        public static void S2C_ANS_DELETE_PLAYER(GameConnection conn, string name)
        {
            var msg = new MSG_S2C_CHARACTER_DELETE_ACK();
            msg.CharacterName = name;
            conn.Send(msg.Serialize());
        }

        public static void S2C_CHAR_LOGIN_OK(GameConnection conn)
        {
            var msg = new MSG_S2C_CHARACTER_SELECT_ACK();
            msg.CharacterName = conn.Character.Name;
            msg.Map = conn.Character.MapId;
            conn.Send(msg.Serialize());
        }

        public static void S2C_WORLD_LOGIN(GameConnection conn)
        {
            var msg = new MSG_S2C_WORLD_LOGIN();
            conn.Character.GetMsgCharacterInfo(out msg.CharacterInfo);
            conn.Character.GetMsgCharacterStat(out msg.CharacterStat);
            conn.Character.GetMsgItemWear(out msg.WearList);
            conn.Character.GetMsgItemInventory(out msg.InventoryList);
            conn.Character.GetMsgActivePet(out msg.PetActive);
            conn.Character.GetMsgPetInventory(out msg.PetInventory);
            conn.Send(msg.Serialize());
            conn.Send(new MSG_S2C_UNKNOWN_37().Serialize());
            conn.Send(new MSG_S2C_UNKNOWN_25().Serialize());
            conn.Send(new MSG_S2C_CHAT_INITIALIZE().Serialize());
        }

        public static void S2C_CHAR_LOGOUT(GameConnection conn)
        {
            var msg = new MSG_S2C_HEADER(Op.S2C_CHAR_LOGOUT);
            msg.Size = 12;
            conn.Send(msg.Serialize());
        }
    }
}
