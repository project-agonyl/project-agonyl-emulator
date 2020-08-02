#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data
{
    public static class Constants
    {
        public const byte WARRIOR_TYPE = 0;
        public const byte PALADIN_TYPE = 1;
        public const byte MAGE_TYPE = 2;
        public const byte ARCHER_TYPE = 3;

        public const byte TEMOZ_TOWN = 0;
        public const byte QUANATO_TOWN = 1;

        public const int MAP_XCELL = 256;
        public const int MAP_YCELL = 256;
        public const byte BLK_XCELL = 12;
        public const byte BLK_YCELL = 12;
        public const byte MAP_XBLK = 15;
        public const byte MAP_YBLK = 15;

        public const ushort S2C_ERROR_CODE_DUPLICATE_CHARACTER = 0x1104;
        public const ushort S2C_ERROR_CODE_CHARACTER_INVALID = 0x1106;
        public const ushort S2C_ERROR_CODE_CHARACTER_SLOTS_FULL = 0x1201;
        public const ushort S2C_ERROR_CODE_CHARACTER_NOT_FOUND = 0x1202;
    }
}
