#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.Collections.Generic;
using System.Reflection;

namespace Agonyl.Shared.Network
{
    public static class Op
    {
        /*-----------------------------------------------------------------------------

        0x0000 ~ 0x0fff : reserved for system (error, system management, alert)
        0x1100 ~ 0x11ff : login, logout, appear, disappear
        0x1200 ~ 0x12ff : move related (move, warp)
        0x1300 ~ 0x13ff : npc related (regen, move, attack)
        0x1400 ~ 0x14ff : attack related (attack, skill, die, alive)
        0x1500 ~ 0x15ff : magic related
        0x1600 ~ 0x16ff : ability related (level up, get skill
        0x1700 ~ 0x17ff : item related
        0x1800 ~ 0x1fff : reserved for basic function

        0x2100 ~ 0x21ff : quest
        0x2200 ~ 0x22ff : party
        0x2300 ~ 0x23ff : guild
        0x2400 ~ 0x24ff : nation
        0x2500 ~ 0x2fff : reserved for event
        0x5000 ~ 0x5fff : mercenary related

        0xa000 ~ 0xffff : main server <-> zone/account server

        -----------------------------------------------------------------------------*/

        public const ushort C2S_LOGIN = 0xE0; // Size 56
        public const ushort C2S_SERVER_DETAILS = 0xE1; // Size 11
        public const ushort S2C_LOGIN_MESSAGE = 0xE2; // Size 92
        public const ushort S2C_SERVER_LIST = 0xE3;  // Size 111
        public const ushort S2C_LOGIN_OK = 0xE4;  // Size 10
        public const ushort S2C_SERVER_DETAILS = 0xE5;  // Size 34

        public const ushort S2C_ERROR = 0x0fff;
        public const ushort C2S_UNKNOWN_PROTOCOL = 0x000f;

        public const ushort S2C_PC_APPEAR = 0x1100;
        public const ushort S2C_PC_DISAPPEAR = 0x1101;
        public const ushort C2S_PREPARE_USER = 0x1105;
        public const ushort S2C_CHAR_LIST = 0x1105;
        public const ushort C2S_CHAR_LOGIN = 0x1106;
        public const ushort S2C_CHAR_LOGIN_OK = 0x1106;
        public const ushort C2S_WORLD_LOGIN = 0x1107;
        public const ushort S2C_WORLD_LOGIN = 0x1107;
        public const ushort C2S_CHAR_LOGOUT = 0x1108;

        public const ushort C2S_ASK_MOVE = 0x1200;
        public const ushort S2C_ANS_MOVE = 0x1200;
        public const ushort S2C_SEE_MOVE = 0x1201;
        public const ushort C2S_PC_MOVE = 0x1202;
        public const ushort S2C_FIX_MOVE = 0x1203;
        public const ushort S2C_SEE_STOP = 0x1204;

        public const ushort S2C_NPC_INITIALIZE_PROTOCOL = 0x1300;

        public const ushort S2C_UNKNOWN_25_PROTOCOL = 0x1461;

        public const ushort S2C_UNKNOWN_37_PROTOCOL = 0x1601;

        public const ushort S2C_CHAT_INITIALIZE_PROTOCOL = 0x1803;

        public const ushort S2C_UNKNOWN_36_PROTOCOL = 0x2332;

        public const ushort C2S_ASK_CREATE_PLAYER = 0xA001;
        public const ushort S2C_ANS_CREATE_PLAYER = 0xA001;
        public const ushort C2S_ASK_DELETE_PLAYER = 0xA002;
        public const ushort S2C_ANS_DELETE_PLAYER = 0xA002;

        private static readonly Dictionary<int, int> _sizes = new Dictionary<int, int>();
        private static readonly Dictionary<int, string> _names = new Dictionary<int, string>();

        static Op()
        {
            _sizes[C2S_LOGIN] = 56;
            _sizes[C2S_SERVER_DETAILS] = 11;
            _sizes[C2S_PREPARE_USER] = 56;
            _sizes[C2S_CHAR_LOGIN] = 37;
            _sizes[C2S_ASK_CREATE_PLAYER] = 35;
            _sizes[C2S_ASK_DELETE_PLAYER] = 33;
            _sizes[C2S_WORLD_LOGIN] = 33;
            _sizes[C2S_CHAR_LOGOUT] = 12;
            _sizes[C2S_UNKNOWN_PROTOCOL] = 0;

            foreach (var field in typeof(Op).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                _names[(ushort)field.GetValue(null)] = field.Name;
            }
        }

        public static int GetSize(ushort op)
        {
            if (!_sizes.TryGetValue(op, out var size))
            {
                return -1;
            }

            return size;
        }

        public static string GetName(ushort op)
        {
            if (!_names.TryGetValue(op, out var name))
            {
                return "?";
            }

            return name;
        }
    }
}
