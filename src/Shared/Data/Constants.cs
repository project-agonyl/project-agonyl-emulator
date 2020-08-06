#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data
{
    public static class Constants
    {
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

        public const uint ItemMountConstant = 0x10000;
        public const uint ItemBlessingConstant = 0x8000;
        public const uint ItemAdditionalAttributeConstant = 0xF;
        public const uint ItemBlueOptionConstant = 0x4000;
        public const uint ItemRedOptionConstant = 0x10000;
        public const uint ItemGreyOptionConstant = 0x4000000;
    }
}
