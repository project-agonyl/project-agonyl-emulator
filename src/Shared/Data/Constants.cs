#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data
{
    public static class Constants
    {
        public const ushort S2C_ERROR_PROTOCOL = 0xfff;
        public const ushort S2C_CHARACTER_LIST_PROTOCOL = 0x1105;
        public const ushort S2C_CHARACTER_CREATE_ACK_PROTOCOL = 0xA001;
        public const ushort S2C_CHARACTER_DELETE_ACK_PROTOCOL = 0xA002;
        public const ushort S2C_CHARACTER_SELECT_ACK_PROTOCOL = 0x1106;
        public const ushort S2C_WORLD_LOGIN_PROTOCOL = 0x1107;
        public const ushort S2C_UNKNOWN_37_PROTOCOL = 0x1601;
        public const ushort S2C_UNKNOWN_25_PROTOCOL = 0x1461;
        public const ushort S2C_UNKNOWN_36_PROTOCOL = 0x2332;

        public const ushort S2C_ERROR_CODE_DUPLICATE_CHARACTER = 0x1104;
        public const ushort S2C_ERROR_CODE_CHARACTER_INVALID = 0x1106;
        public const ushort S2C_ERROR_CODE_CHARACTER_SLOTS_FULL = 0x1201;
        public const ushort S2C_ERROR_CODE_CHARACTER_NOT_FOUND = 0x1202;

        public const byte CHARACTER_TYPE_WARRIOR = 0;
        public const byte CHARACTER_TYPE_HK = 1;
        public const byte CHARACTER_TYPE_MAGE = 2;
        public const byte CHARACTER_TYPE_ARCHER = 3;

        public const byte TOWN_TEMOZ = 0;
        public const byte TOWN_QUANATO = 1;
    }
}
