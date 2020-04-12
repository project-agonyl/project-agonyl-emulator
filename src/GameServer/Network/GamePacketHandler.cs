#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using Agonyl.Game.Data;
using Agonyl.Shared.Data;
using Agonyl.Shared.Network;
using Agonyl.Shared.Util;

namespace Agonyl.Game.Network
{
    public class GamePacketHandler : PacketHandler<GameConnection>
    {
        public static readonly GamePacketHandler Instance = new GamePacketHandler();

        /// <summary>
        /// Sent on clicking server name.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="packet"></param>
        [PacketHandler(Op.C2S_CHARACTER_LIST)]
        public void C2S_CHARACTER_LIST(GameConnection conn, Packet packet)
        {
            packet.SetReadPointer(14);
            var username = packet.GetString(20);
            packet.SetReadPointer(35);
            var password = packet.GetString(20);
            if (GameServer.Instance.ASDDatabase.AccountExists(username, password))
            {
                conn.Account = new Account(username);
                conn.Username = username;
                Send.S2C_CHARACTER_LIST(conn, conn.Account);
            }
        }

        /// <summary>
        /// Sent on creating new character.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="packet"></param>
        [PacketHandler(Op.C2S_CHARACTER_CREATE_REQUEST)]
        public void C2S_CREATE_CHARACTER(GameConnection conn, Packet packet)
        {
            var decodedPacket = new MSG_C2S_CHARACTER_CREATE_REQUEST();
            decodedPacket.Deserialize(ref packet);
            if (GameServer.Instance.ASDDatabase.CharacterExists(decodedPacket.CharacterName))
            {
                Send.S2C_ERROR(conn, Constants.S2C_ERROR_CODE_DUPLICATE_CHARACTER, "Character name already in use");
            }
            else if (GameServer.Instance.ASDDatabase.CharacterCount(conn.Account.Username) >= 5)
            {
                Send.S2C_ERROR(conn, Constants.S2C_ERROR_CODE_CHARACTER_SLOTS_FULL, "Your character slots are full");
            }
            else
            {
                // Create and send acknowledgement
                var stats = GameServer.Instance.Conf.StarterStatsWarrior;
                var location = GameServer.Instance.Conf.StarterLocationTemoz;
                var body = GameServer.Instance.Conf.StarterBodyWarrior;
                var level = GameServer.Instance.Conf.StarterLevel;

                // Insert wear into body
                switch (decodedPacket.CharacterType)
                {
                    case Constants.CHARACTER_TYPE_WARRIOR:
                        body = Functions.InsertWearIntoMbody(GameServer.Instance.Conf.StarterBodyWarrior, GameServer.Instance.Conf.StarterGearWarrior);
                        break;

                    case Constants.CHARACTER_TYPE_HK:
                        body = Functions.InsertWearIntoMbody(GameServer.Instance.Conf.StarterBodyHK, GameServer.Instance.Conf.StarterGearHK);
                        stats = GameServer.Instance.Conf.StarterStatsHK;
                        break;

                    case Constants.CHARACTER_TYPE_MAGE:
                        body = Functions.InsertWearIntoMbody(GameServer.Instance.Conf.StarterBodyMage, GameServer.Instance.Conf.StarterGearMage);
                        stats = GameServer.Instance.Conf.StarterStatsMage;
                        break;

                    case Constants.CHARACTER_TYPE_ARCHER:
                        body = Functions.InsertWearIntoMbody(GameServer.Instance.Conf.StarterBodyArcher, GameServer.Instance.Conf.StarterGearArcher);
                        stats = GameServer.Instance.Conf.StarterStatsArcher;
                        break;
                }

                if (decodedPacket.CharacterTown == Constants.TOWN_QUANATO)
                {
                    location = GameServer.Instance.Conf.StarterLocationQuanato;
                    body = body.Replace("SINFO=0", "SINFO=1");
                }

                if (GameServer.Instance.ASDDatabase.CreateCharacter(conn.Account.Username, decodedPacket.CharacterName, decodedPacket.CharacterType, stats, body, location, level))
                {
                    Send.S2C_CHARACTER_CREATE_ACK(conn, decodedPacket.CharacterName, decodedPacket.CharacterType);
                }
                else
                {
                    Send.S2C_ERROR(conn, Constants.S2C_ERROR_CODE_CHARACTER_INVALID, "Could not create character at this time");
                }
            }
        }

        /// <summary>
        /// Sent on deleting a character.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="packet"></param>
        [PacketHandler(Op.C2S_CHARACTER_DELETE_REQUEST)]
        public void C2S_DELETE_CHARACTER(GameConnection conn, Packet packet)
        {
            packet.SetReadPointer(12);
            var name = packet.GetString(13);
            if (GameServer.Instance.ASDDatabase.CharacterExists(conn.Account.Username, name))
            {
                if (GameServer.Instance.ASDDatabase.DeleteCharacter(name))
                {
                    Send.S2C_CHARACTER_DELETE_ACK(conn, name);
                }
                else
                {
                    Send.S2C_ERROR(conn, Constants.S2C_ERROR_CODE_CHARACTER_INVALID, "Could not delete character at this time");
                }
            }
            else
            {
                Send.S2C_ERROR(conn, Constants.S2C_ERROR_CODE_CHARACTER_NOT_FOUND, "Character not found in the account");
            }
        }

        [PacketHandler(Op.C2S_SELECT_CHARACTER)]
        public void C2S_SELECT_CHARACTER(GameConnection conn, Packet packet)
        {
            packet.SetReadPointer(12);
            var name = packet.GetString(13);
            if (GameServer.Instance.ASDDatabase.CharacterExists(conn.Account.Username, name))
            {
                conn.Character = new Character();
                conn.Character.Info = GameServer.Instance.ASDDatabase.GetCharacter(name);
                conn.Character.Name = conn.Character.Info.c_id;
                Send.S2C_CHARACTER_SELECT_ACK(conn);
            }
            else
            {
                Send.S2C_ERROR(conn, Constants.S2C_ERROR_CODE_CHARACTER_NOT_FOUND, "Character not found in the account");
            }
        }

        [PacketHandler(Op.C2S_WORLD_LOGIN)]
        public void C2S_WORLD_LOGIN(GameConnection conn, Packet packet)
        {
            if (conn.Character != null)
            {
                Send.S2C_WORLD_LOGIN(conn);
            }
            else
            {
                Send.S2C_ERROR(conn, Constants.S2C_ERROR_CODE_CHARACTER_NOT_FOUND, "Character not found in the account");
            }
        }

        /// <summary>
        /// Sent on client exits.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="packet"></param>
        [PacketHandler(Op.C2S_CLIENT_EXIT)]
        public void C2S_CLIENT_EXIT(GameConnection conn, Packet packet)
        {
            if (conn.Account != null && GameServer.Instance.Redis.IsLoggedIn(conn.Account.Username))
            {
                Log.Info(conn.Account.Username + " account has left the game server");
                GameServer.Instance.Redis.RemoveLoggedInAccount(conn.Account.Username);
            }
        }
    }
}
