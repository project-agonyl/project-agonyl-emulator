#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data
{
    public static class Constants
    {
        public const ushort S2C_ERROR_PROTOCOL = 0xfff;
        public const ushort S2C_CHARACTER_LIST_PROTOCOL = 0x1105;
        public const ushort S2C_CHARACTER_CREATE_ACK_PROTOCOL = 0xA001;
        public const ushort S2C_CHARACTER_DELETE_ACK_PROTOCOL = 0xA002;

        public const ushort S2C_ERROR_CODE_DUPLICATE_CHARACTER = 0x1104;
        public const ushort S2C_ERROR_CODE_CHARACTER_SLOTS_FULL = 0x1201;

        public const byte CHARACTER_TYPE_WARRIOR = 0;
        public const byte CHARACTER_TYPE_HK = 1;
        public const byte CHARACTER_TYPE_MAGE = 2;
        public const byte CHARACTER_TYPE_ARCHER = 3;

        public const byte TOWN_TEMOZ = 0;
        public const byte TOWN_QUANATO = 1;
    }
}
