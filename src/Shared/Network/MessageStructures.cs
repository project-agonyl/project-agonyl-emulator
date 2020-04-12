#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.Runtime.InteropServices;
using Agonyl.Shared.Data;

namespace Agonyl.Shared.Network
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ACL_ITEM_WEAR
    {
        public uint ItemPtr;
        public uint ItemCode;
        public uint ItemOption;
        public uint WearIndex;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SKILL_INFO
    {
        public uint Skill0;
        public uint Skill1;
        public uint Skill2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SOCIAL_INFO
    {
        public uint Nation; // Default = 8
        public uint Rank; //  Default = 8
        public uint KnightIndex; //  Default = 16
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ITEM_ID
    {
        public uint ItemPtr;
        public uint ItemCode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PET_ID
    {
        public uint PetPtr;
        public uint PetCode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PET_INFO
    {
        public PET_ID PetId;
        public uint SerialKey;
        public uint Option1;
        public uint Option2;
    }

    public struct ITEM
    {
        public ITEM_ID ItemId;
        public uint ItemOption;
        public uint ItemKey;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ITEM_WEAR
    {
        public ITEM Item;
        public uint WearIndex;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ITEM_INVENTORY
    {
        public ITEM Item;
        public uint InventoryIndex;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ACL_CHARACTER_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CharacterName;

        public byte Used;
        public byte Class;
        public byte Town;
        public uint Level;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 10)]
        public ACL_ITEM_WEAR[] WearList;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CHARACTER_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string CharacterName;

        public byte Type;
        public ushort Level;
        public uint Exp;
        public ushort MapIndex;
        public uint CellIndex;
        public SKILL_INFO SkillList;
        public byte PKCount;
        public ushort RTime;
        public SOCIAL_INFO SInfo;
        public uint Money;
        public ushort StoredHp;
        public ushort StoredMp;
        public uint Lore;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CHARACTER_CALCULATED_STAT
    {
        public ushort HitAttack;
        public ushort MagicAttack;
        public ushort Defense;
        public ushort FireAttack;
        public ushort FireDefence;
        public ushort IceAttack;
        public ushort IceDefense;
        public ushort LightAttack;
        public ushort LightDefense;
        public ushort MaxHp;
        public ushort MaxMp;
        public ushort HitAddition;
        public ushort MagAddition;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CHARACTER_STAT
    {
        public ushort Point;
        public ushort Str;
        public ushort Magic;
        public ushort Dex;
        public ushort Vit;
        public ushort Mana;
        public ushort HPCapacity;
        public ushort MPCapacity;
        public ushort HP;
        public ushort MP;
        public CHARACTER_CALCULATED_STAT CalculatedStat;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_HEAD_WITH_PROTOCOL : Marshalling
    {
        public uint Size;
        public uint PCID;
        public byte Ctrl;
        public byte Cmd;
        public ushort Protocol;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_HEAD_NO_PROTOCOL : Marshalling
    {
        public uint Size;
        public uint PCID;
        public byte Ctrl;
        public byte Cmd;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_HEADER : Marshalling
    {
        public uint Size;
        public uint PCID;
        public byte Ctrl;
        public byte Cmd;
        public ushort Protocol;

        public MSG_S2C_HEADER()
        {
            this.Ctrl = 0x03;
            this.Cmd = 0xFF;
        }

        public MSG_S2C_HEADER(uint uid, ushort protocol)
        {
            this.PCID = uid;
            this.Ctrl = 0x03;
            this.Cmd = 0xFF;
            this.Protocol = protocol;
        }

        public MSG_S2C_HEADER(ushort protocol)
        {
            this.Ctrl = 0x03;
            this.Cmd = 0xFF;
            this.Protocol = protocol;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_ERROR : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;
        public ushort ErrorCode;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string ErrorMsg;

        public MSG_S2C_ERROR()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_ERROR_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_CHARACTER_LIST : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 5)]
        public ACL_CHARACTER_INFO[] CharInfo;

        public MSG_S2C_CHARACTER_LIST()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_CHARACTER_LIST_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
            this.CharInfo = new ACL_CHARACTER_INFO[5];
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_C2S_CHARACTER_CREATE_REQUEST : Marshalling
    {
        public MSG_HEAD_WITH_PROTOCOL MsgHeader;
        public byte CharacterType;
        public byte CharacterTown;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CharacterName;

        public MSG_C2S_CHARACTER_CREATE_REQUEST()
        {
            this.MsgHeader = new MSG_HEAD_WITH_PROTOCOL();
            this.MsgHeader.Size = this.GetSize();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_CHARACTER_CREATE_ACK : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;
        public byte CharacterType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CharacterName;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 10)]
        public ACL_ITEM_WEAR[] WearList;

        public MSG_S2C_CHARACTER_CREATE_ACK()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_CHARACTER_CREATE_ACK_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
            this.WearList = new ACL_ITEM_WEAR[10];
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_CHARACTER_DELETE_ACK : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CharacterName;

        public MSG_S2C_CHARACTER_DELETE_ACK()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_CHARACTER_DELETE_ACK_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_CHARACTER_SELECT_ACK : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string CharacterName;

        public uint InternalId;

        public MSG_S2C_CHARACTER_SELECT_ACK()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_CHARACTER_SELECT_ACK_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_WORLD_LOGIN : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;
        public CHARACTER_INFO CharacterInfo;
        public CHARACTER_STAT CharacterStat;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 10)]
        public ITEM_WEAR[] WearList;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 30)]
        public ITEM_INVENTORY[] InventoryList;

        public PET_INFO PetActive;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 5)]
        public PET_INFO[] PetInventory;

        public MSG_S2C_WORLD_LOGIN()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_WORLD_LOGIN_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
            this.CharacterInfo = new CHARACTER_INFO();
            this.CharacterInfo.SkillList = new SKILL_INFO();
            this.CharacterInfo.SInfo = new SOCIAL_INFO();
            this.CharacterInfo.SInfo.Nation = 8;
            this.CharacterInfo.SInfo.Rank = 8;
            this.CharacterInfo.SInfo.KnightIndex = 16;
            this.CharacterStat = new CHARACTER_STAT();
            this.CharacterStat.CalculatedStat = new CHARACTER_CALCULATED_STAT();
            this.WearList = new ITEM_WEAR[10];
            this.InventoryList = new ITEM_INVENTORY[30];
            this.PetActive = new PET_INFO();
            this.PetActive.PetId = new PET_ID();
            this.PetInventory = new PET_INFO[5];
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_UNKNOWN_37 : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
        public byte[] Unknown;

        public MSG_S2C_UNKNOWN_37()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_UNKNOWN_37_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
            this.Unknown = new byte[25];
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_UNKNOWN_25 : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public byte[] Unknown;

        public MSG_S2C_UNKNOWN_25()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_UNKNOWN_25_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
            this.Unknown = new byte[13];
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_UNKNOWN_36 : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;
        public byte Unknown1;
        public byte Unknown2;
        public byte Unknown3;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string CharacterName;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Unknown4;

        public MSG_S2C_UNKNOWN_36()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_UNKNOWN_36_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
            this.Unknown1 = 0;
            this.Unknown2 = 0xCC;
            this.Unknown3 = 1;
            this.Unknown4 = new byte[18];
        }
    }
}
