#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using Agonyl.Game.Data;
using Agonyl.Shared.Const;
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
        [PacketHandler(Op.C2S_PREPARE_USER)]
        public void C2S_PREPARE_USER(GameConnection conn, Packet packet)
        {
            packet.SetReadPointer(14);
            var username = packet.GetString(20);
            packet.SetReadPointer(35);
            var password = packet.GetString(20);
            if (GameServer.Instance.ASDDatabase.AccountExists(username, password))
            {
                conn.Account = new Account(username);
                conn.Username = username;
                Send.S2C_CHAR_LIST(conn, conn.Account);
            }
        }

        /// <summary>
        /// Sent on creating new character.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="packet"></param>
        [PacketHandler(Op.C2S_ASK_CREATE_PLAYER)]
        public void C2S_ASK_CREATE_PLAYER(GameConnection conn, Packet packet)
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
                switch ((CharacterType)decodedPacket.CharacterType)
                {
                    case CharacterType.Warrior:
                        body = Functions.InsertWearIntoMbody(GameServer.Instance.Conf.StarterBodyWarrior, GameServer.Instance.Conf.StarterGearWarrior);
                        break;

                    case CharacterType.Paladin:
                        body = Functions.InsertWearIntoMbody(GameServer.Instance.Conf.StarterBodyHK, GameServer.Instance.Conf.StarterGearHK);
                        stats = GameServer.Instance.Conf.StarterStatsHK;
                        break;

                    case CharacterType.Mage:
                        body = Functions.InsertWearIntoMbody(GameServer.Instance.Conf.StarterBodyMage, GameServer.Instance.Conf.StarterGearMage);
                        stats = GameServer.Instance.Conf.StarterStatsMage;
                        break;

                    case CharacterType.Archer:
                        body = Functions.InsertWearIntoMbody(GameServer.Instance.Conf.StarterBodyArcher, GameServer.Instance.Conf.StarterGearArcher);
                        stats = GameServer.Instance.Conf.StarterStatsArcher;
                        break;
                }

                if ((Town)decodedPacket.CharacterTown == Town.Quanato)
                {
                    location = GameServer.Instance.Conf.StarterLocationQuanato;
                    body = body.Replace("SINFO=0", "SINFO=1");
                }

                if (GameServer.Instance.ASDDatabase.CreateCharacter(conn.Account.Username, decodedPacket.CharacterName, decodedPacket.CharacterType, stats, body, location, level))
                {
                    Send.S2C_ANS_CREATE_PLAYER(conn, decodedPacket.CharacterName, decodedPacket.CharacterType);
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
        [PacketHandler(Op.C2S_ASK_DELETE_PLAYER)]
        public void C2S_ASK_DELETE_PLAYER(GameConnection conn, Packet packet)
        {
            packet.SetReadPointer(12);
            var name = packet.GetString(13);
            if (GameServer.Instance.ASDDatabase.CharacterExists(conn.Account.Username, name))
            {
                if (GameServer.Instance.ASDDatabase.DeleteCharacter(name))
                {
                    Send.S2C_ANS_DELETE_PLAYER(conn, name);
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

        [PacketHandler(Op.C2S_CHAR_LOGIN)]
        public void C2S_CHAR_LOGIN(GameConnection conn, Packet packet)
        {
            packet.SetReadPointer(12);
            var name = packet.GetString(13);
            if (GameServer.Instance.ASDDatabase.CharacterExists(conn.Account.Username, name))
            {
                var character = GameServer.Instance.ASDDatabase.GetCharacter(name);
                var map = GameServer.Instance.World.GetMap(character.MapId);
                if (map == null)
                {
                    Log.Warning("C2S_CHAR_LOGIN: User '{0}' logged on with invalid map '{1}'.", conn.Account.Username, character.MapId);
                    conn.Close();
                    return;
                }

                conn.Character = character;
                conn.Character.GameConnection = conn;
                map.AddCharacter(character);
                Send.S2C_CHAR_LOGIN_OK(conn);
            }
            else
            {
                Send.S2C_ERROR(conn, Constants.S2C_ERROR_CODE_CHARACTER_NOT_FOUND, "Character not found in the account");
            }
        }

        [PacketHandler(Op.C2S_WORLD_LOGIN)]
        public void C2S_WORLD_LOGIN(GameConnection conn, Packet packet)
        {
            if (conn.Character == null)
            {
                Send.S2C_ERROR(conn, Constants.S2C_ERROR_CODE_CHARACTER_NOT_FOUND, "Character not found in the account");
                return;
            }

            Send.S2C_WORLD_LOGIN(conn);
            conn.Character.SetPlayerState(PlayerState.STANDING);
        }

        /// <summary>
        /// Sent on client exits.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="packet"></param>
        [PacketHandler(Op.C2S_CHAR_LOGOUT)]
        public void C2S_CHAR_LOGOUT(GameConnection conn, Packet packet)
        {
            Log.Info("Account '{0}' is logging out", conn.Account.Username);
            Send.S2C_CHAR_LOGOUT(conn);
        }

        [PacketHandler(Op.C2S_PING)]
        public void C2S_PING(GameConnection conn, Packet packet)
        {
            Log.Info("Received ping from " + conn.Character.Name);
            var currentServerTick = (uint)(int.MaxValue & (Environment.TickCount - conn.Character.Handle));
            var cTick = new MSG_CHK_TIMETICK();
            var buffer = packet.GetBuffer();
            cTick.Deserialize(ref buffer);

            if (conn.Character.ClientTick == 0)
            {
                conn.Character.PreviousServerTick = currentServerTick;
                conn.Character.ClientTick = cTick.ClientTick;
                conn.Character.PreviousTickCount = cTick.TickCount;
                return;
            }

            if (cTick.TickCount - conn.Character.PreviousTickCount != 1)
            {
                Log.Info(string.Format("[Index] {0} {1} {2} Pre:{3} now:{4} SendTime:{5} {6}", conn.Character.Account.Username, conn.Character.Name, conn.Character.IPAddress, conn.Character.CurrentTickCount, cTick.TickCount, cTick.ClientTick - conn.Character.ClientTick, currentServerTick - conn.Character.CurrentServerTick));
                conn.Character.CurrentTickCount = cTick.TickCount++;
                conn.Character.CurrentTickCount = cTick.TickCount;
                return;
            }

            if (cTick.ServerTick != conn.Character.CurrentServerTick)
            {
                Log.Info(string.Format("[Hack] {0} {1} {2} Changed SvrTick:{3}->{4} {5}", conn.Character.Account.Username, conn.Character.Name, conn.Character.IPAddress, conn.Character.CurrentServerTick, cTick.ServerTick, currentServerTick - conn.Character.CurrentServerTick));

                // TODO: Handle speed hackers
                return;
            }

            var serverTickDiff = Math.Abs(currentServerTick - conn.Character.PreviousServerTick);
            var clientTickDiff = Math.Abs(cTick.ClientTick - conn.Character.ClientTick);
            if (Math.Abs(serverTickDiff - clientTickDiff) > 500)
            {
                if (++conn.Character.TickErrorCount > 1)
                {
                    Log.Info(string.Format("[Hack] {0} {1} {2} {3} {4} {5}", conn.Character.Account.Username, conn.Character.Name, conn.Character.IPAddress, serverTickDiff, clientTickDiff, currentServerTick - conn.Character.CurrentServerTick));

                    // TODO: Handle speed hackers
                    return;
                }

                conn.Character.PreviousTickCount = cTick.TickCount;
                conn.Character.ClientTick = cTick.ClientTick;
                conn.Character.PreviousServerTick = currentServerTick;
                return;
            }

            conn.Character.PreviousTickCount = cTick.TickCount;
            conn.Character.ClientTick = cTick.ClientTick;
            conn.Character.PreviousServerTick = currentServerTick;
            conn.Character.TickErrorCount = 0;
        }
    }
}
