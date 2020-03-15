#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System.Runtime.InteropServices;
using Agonyl.Shared.Data;

namespace Agonyl.Shared.Network
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MSG_ITEM_WEAR
    {
        public uint ItemPtr;
        public uint ItemCode;
        public uint ItemOption;
        public uint WearSlot;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CHARACTER_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CharacterName;

        public byte Used;
        public byte Class;
        public byte Town;
        public uint Level;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 10)]
        public MSG_ITEM_WEAR[] WearList;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CHARACTER_DATA
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CharacterName;

        public byte Class;
        public ushort Level;
        public uint Exp;
        public uint MapId;
        public uint Location;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] SkillInfo;

        public uint Woonz;
        public uint CurrentHPPot;
        public uint CurrentMPPot;
        public uint Lore;
        public ushort RemainingStats;
        public ushort Strength;
        public ushort Intelligence;
        public ushort Dexterity;
        public ushort Vitality;
        public ushort Mana;
        public uint MaxHPPot;
        public uint MaxMPPot;
        public ushort CurrentHP;
        public ushort CurrentMP;
        public ushort Damage;
        public ushort MagicDamage;
        public ushort Defense;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] ShueInfo;

        public ushort MaxHP;
        public ushort MaxMP;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 944)]
        public byte[] Unknown;
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
        public CHARACTER_INFO[] CharInfo;

        public MSG_S2C_CHARACTER_LIST()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_CHARACTER_LIST_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
            this.CharInfo = new CHARACTER_INFO[5];
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
        public MSG_ITEM_WEAR[] WearList;

        public MSG_S2C_CHARACTER_CREATE_ACK()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_CHARACTER_CREATE_ACK_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
            this.WearList = new MSG_ITEM_WEAR[10];
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
    public class MSG_S2C_WORLD_LOGIN : Marshalling
    {
        public MSG_S2C_HEADER MsgHeader;
        public CHARACTER_DATA CharacterData;

        public MSG_S2C_WORLD_LOGIN()
        {
            this.MsgHeader = new MSG_S2C_HEADER(Constants.S2C_WORLD_LOGIN_PROTOCOL);
            this.MsgHeader.Size = this.GetSize();
            this.CharacterData = new CHARACTER_DATA();
        }
    }
}
