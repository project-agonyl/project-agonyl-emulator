#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System.Collections.Generic;
using System.Reflection;

namespace Agonyl.Shared.Network
{
    public static class Op
    {
        public const int C2S_LOGIN = 0xE0; // Size 56
        public const int C2S_SERVER_DETAILS = 0xE1; // Size 11
        public const int S2C_LOGIN_MESSAGE = 0xE2; // Size 92
        public const int S2C_SERVER_LIST = 0xE3;  // Size 111
        public const int S2C_LOGIN_OK = 0xE4;  // Size 10
        public const int S2C_SERVER_DETAILS = 0xE5;  // Size 34
        public const int C2S_CHARACTER_LIST = 0x01; // Size 56
        public const int S2C_CHARACTER_LIST = 0x02; // Size 952
        public const int C2S_SELECT_CHARACTER = 0x03; // Size 37
        public const int S2C_CHARACTER_DETAILS = 0x04;
        public const int C2S_CHARACTER_CREATE_REQUEST = 0x05; // Size 35
        public const int S2C_CHARACTER_CREATE_ACK = 0x06;
        public const int S2C_DUPLICATE_CHARACTER = 0x07;
        public const int C2S_CHARACTER_DELETE_REQUEST = 0x08; // Size 33
        public const int C2S_WORLD_ENTER = 0x09; // Size 33
        public const int S2C_CHARACTER_WORLD_ENTER = 0x0A;
        public const int S2C_PACKET_37 = 0x0B;
        public const int S2C_PACKET_25 = 0x0C;
        public const int S2C_INIT_CHAT = 0x0D;
        public const int S2C_FRIEND_LIST = 0x0E;
        public const int S2C_PACKET_36 = 0x0F;
        public const int S2C_NPC_DATA = 0x10;
        public const int C2S_CAN_INTERACT_NPC = 0x11; // Size 14
        public const int S2C_INTERACT_NPC = 0x12; // Size 14
        public const int C2S_HEALER_WINDOW_OPEN = 0x13; // Size 16
        public const int S2C_MAXED_HP_MP = 0x14;
        public const int S2C_RECOVERED_FROM_EXHAUSTION = 0x15;
        public const int C2S_RECHARGE_POTION = 0x16; // Size 14
        public const int S2C_RECHARGE_POTION_SUCCESS = 0x17;
        public const int C2S_WARP_REQUEST = 0x18; // Size 18
        public const int S2C_WARP_ACK = 0x19;
        public const int S2C_WARP_MAP = 0x20;
        public const int C2S_WARP_LOCATION = 0x2A; // Size 14
        public const int S2C_WARP_LOCATION = 0x2B;
        public const int C2S_WARP_COMPLETE = 0x2C; // Size 13
        public const int S2C_WARP_COMPLETE_ACK = 0x2D;
        public const int C2S_MOVE_CHARACTER = 0x2E; // Size 17
        public const int S2C_MOVE_CHARACTER_ACK = 0x2F;
        public const int C2S_MOVED_CHARACTER = 0x21; // Size 17
        public const int C2S_PAYMENT_INFO = 0x22; // Size 14
        public const int C2S_RECHARGE_POTIONS_FULL = 0x23; // Size 16
        public const int C2S_CLIENT_EXIT = 0xF7; // Size 12
        public const int S2C_CHAT_GENERAL = 0xF8;
        public const int S2C_CHAT_COUNTRY = 0xF9;
        public const int S2C_CHAT_WHISPER = 0xFA;
        public const int S2C_CHAT_SHOUT = 0xFB;
        public const int S2C_TOP_BAR_MSG = 0xFC;
        public const int S2C_ANNOUNCEMENT = 0xFD;
        public const int C2S_PING = 0xFE; // Size 22
        public const int C2S_UNKNOWN = 0xFF;

        private static readonly Dictionary<int, int> _sizes = new Dictionary<int, int>();
        private static readonly Dictionary<int, string> _names = new Dictionary<int, string>();

        static Op()
        {
            _sizes[C2S_LOGIN] = 56;
            _sizes[C2S_SERVER_DETAILS] = 11;
            _sizes[C2S_CHARACTER_LIST] = 56;
            _sizes[C2S_SELECT_CHARACTER] = 37;
            _sizes[C2S_CHARACTER_CREATE_REQUEST] = 35;
            _sizes[C2S_CHARACTER_DELETE_REQUEST] = 33;
            _sizes[C2S_WORLD_ENTER] = 33;
            _sizes[C2S_CAN_INTERACT_NPC] = 14;
            _sizes[C2S_HEALER_WINDOW_OPEN] = 16;
            _sizes[C2S_RECHARGE_POTION] = 14;
            _sizes[C2S_WARP_REQUEST] = 18;
            _sizes[C2S_WARP_LOCATION] = 14;
            _sizes[C2S_WARP_COMPLETE] = 13;
            _sizes[C2S_MOVE_CHARACTER] = 17;
            _sizes[C2S_MOVED_CHARACTER] = 17;
            _sizes[C2S_PAYMENT_INFO] = 14;
            _sizes[C2S_RECHARGE_POTIONS_FULL] = 16;
            _sizes[C2S_PING] = 22;
            _sizes[C2S_CLIENT_EXIT] = 12;
            _sizes[C2S_UNKNOWN] = 0;

            foreach (var field in typeof(Op).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                _names[(int)field.GetValue(null)] = field.Name;
            }
        }

        public static int GetSize(int op)
        {
            if (!_sizes.TryGetValue(op, out var size))
            {
                return -1;
            }

            return size;
        }

        public static string GetName(int op)
        {
            if (!_names.TryGetValue(op, out var name))
            {
                return "?";
            }

            return name;
        }
    }
}
