#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.Runtime.InteropServices;
using Agonyl.Shared.Data;
using Agonyl.Shared.Util;

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
        public uint Rank;
        public uint KnightIndex;
        public uint Nation;
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
        public uint Option1;
        public uint Option2;
        public uint SerialKey;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
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
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string Name;

        public byte Type;
        public ushort Level;
        public uint Exp;
        public uint MapIndex;
        public uint CellIndex;
        public SKILL_INFO SkillList;
        public uint PKCount;
        public uint RTime;
        public SOCIAL_INFO SInfo;
        public uint Money;
        public uint StoredHp;
        public uint StoredMp;
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
        public uint HPCapacity;
        public uint MPCapacity;
        public ushort HP;
        public ushort MP;
        public CHARACTER_CALCULATED_STAT CalculatedStat;
        public ushort Unknown; // Investigate what data this is
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
            this.MsgHeader = new MSG_S2C_HEADER(Op.S2C_ERROR);
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
            this.MsgHeader = new MSG_S2C_HEADER(Op.S2C_CHAR_LIST);
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
            this.MsgHeader = new MSG_S2C_HEADER(Op.S2C_ANS_CREATE_PLAYER);
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
            this.MsgHeader = new MSG_S2C_HEADER(Op.S2C_ANS_DELETE_PLAYER);
            this.MsgHeader.Size = this.GetSize();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_CHARACTER_SELECT_ACK : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CharacterName;

        public uint RandomNumer;

        public ushort Map;

        public MSG_S2C_CHARACTER_SELECT_ACK()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Op.S2C_CHAR_LOGIN_OK);
            this.MsgHeader.Size = this.GetSize();
            this.RandomNumer = Functions.GetRandomUint();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_WORLD_LOGIN : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader; // Size 12
        public CHARACTER_INFO CharacterInfo; // Size 84
        public CHARACTER_STAT CharacterStat; // Size 50

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 10)]
        public ITEM_WEAR[] WearList; // Size 200

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 30)]
        public ITEM_INVENTORY[] InventoryList; // Size 600

        public PET_INFO PetActive; // Size 20

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 5)]
        public PET_INFO[] PetInventory; // Size 100

        public MSG_S2C_WORLD_LOGIN()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Op.S2C_WORLD_LOGIN);
            this.MsgHeader.Size = this.GetSize();
            this.CharacterInfo = new CHARACTER_INFO();
            this.CharacterInfo.SkillList = new SKILL_INFO();
            this.CharacterInfo.SInfo = new SOCIAL_INFO();
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
            this.MsgHeader = new MSG_S2C_HEADER(Op.S2C_UNKNOWN_37_PROTOCOL);
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
            this.MsgHeader = new MSG_S2C_HEADER(Op.S2C_UNKNOWN_25_PROTOCOL);
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
            this.MsgHeader = new MSG_S2C_HEADER(Op.S2C_UNKNOWN_36_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
            this.Unknown1 = 0;
            this.Unknown2 = 0xCC;
            this.Unknown3 = 1;
            this.Unknown4 = new byte[18];
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_NPC_INITIALIZE : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;
        public ushort NpcId;
        public uint InternalId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public byte[] Unknown1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public byte[] Unknown2;

        public ushort NpcLocation;
        public ushort Unknown3;
        public ushort NpcDirection;
        public uint Unknown4;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        public byte[] Unknown5;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] Unknown6;

        public MSG_S2C_NPC_INITIALIZE()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Op.S2C_NPC_INITIALIZE_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
            this.Unknown1 = new byte[] { 0xf8, 0x2f, 0x20, 0xa1, 0x07 };
            this.Unknown2 = new byte[5];
            this.Unknown3 = 0;
            this.Unknown4 = 0;
            this.Unknown5 = new byte[]
            {
                0xff, 0xcd, 0x00, 0xff, 0xcd, 0x00, 0xff, 0xcd, 0x00, 0xff, 0xcd, 0x00, 0xff, 0xcd, 0x00,
                0xff, 0xcd, 0x00, 0xff, 0xcd, 0x00, 0xff, 0xcd, 0x00, 0xff, 0xcd, 0x00, 0xff, 0xcd, 0x00,
            };
            this.Unknown2 = new byte[10];
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_S2C_CHAT_INITIALIZE : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Unknown;

        public MSG_S2C_CHAT_INITIALIZE()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Op.S2C_CHAT_INITIALIZE_PROTOCOL);
            this.Unknown = new byte[] { 0xff, 0x00, 0x1f, 0x00, 0xe3, 0x00 };
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MSG_CHK_TIMETICK : Marshalling
    {
        public MSG_CHK_TIMETICK()
        {
            this.MsgHeader = new MSG_HEAD_NO_PROTOCOL();
            this.MsgHeader.Size = this.GetSize();
            this.MsgHeader.Ctrl = 0x01;
            this.MsgHeader.Cmd = 0xF0;
        }

        public MSG_HEAD_NO_PROTOCOL MsgHeader;
        public uint TickCount;
        public uint ServerTick;
        public uint ClientTick;
    }
}
