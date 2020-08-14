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

        public const ushort S2C_ERROR = 0x0FFF;
        public const ushort C2S_KEEP_ALIVE = 0x0FF2;
        public const ushort C2S_UNKNOWN_PROTOCOL = 0x000F;

        public const ushort S2C_PC_APPEAR = 0x1100;
        public const ushort S2C_PC_DISAPPEAR = 0x1101;
        public const ushort C2S_PREPARE_USER = 0x1105;
        public const ushort S2C_CHAR_LIST = 0x1105;
        public const ushort C2S_CHAR_LOGIN = 0x1106;
        public const ushort S2C_CHAR_LOGIN_OK = 0x1106;
        public const ushort C2S_WORLD_LOGIN = 0x1107;
        public const ushort S2C_WORLD_LOGIN = 0x1107;
        public const ushort C2S_CHAR_LOGOUT = 0x1108;
        public const ushort S2C_CHAR_LOGOUT = 0x1108;
        public const ushort C2S_WARP = 0x1111;
        public const ushort C2S_RETURN2HERE = 0x1112;
        public const ushort C2S_SUBMAP_INFO = 0x1114;
        public const ushort C2S_ENTER = 0x1115;
        public const ushort C2S_ACTIVE_PET = 0x11A1;
        public const ushort C2S_INACTIVE_PET = 0x11A2;
        public const ushort C2S_PET_BUY = 0x11A5;
        public const ushort C2S_PET_SELL = 0x11A6;
        public const ushort C2S_FEED_PET = 0x11A7;
        public const ushort C2S_REVIVE_PET = 0x11A8;
        public const ushort C2S_SHUE_COMBINATION = 0x11B0;

        public const ushort C2S_ASK_MOVE = 0x1200;
        public const ushort S2C_ANS_MOVE = 0x1200;
        public const ushort S2C_SEE_MOVE = 0x1201;
        public const ushort C2S_PC_MOVE = 0x1202;
        public const ushort S2C_FIX_MOVE = 0x1203;
        public const ushort S2C_SEE_STOP = 0x1204;
        public const ushort C2S_ASK_HS_MOVE = 0x1205;
        public const ushort C2S_HS_MOVE = 0x1208;

        public const ushort S2C_NPC_INITIALIZE_PROTOCOL = 0x1300;
        public const ushort C2S_OBJECT_NPC = 0x1307;
        public const ushort C2S_ASK_NPC_FAVOR = 0x1308;
        public const ushort C2S_NPC_FAVOR_UP = 0x1309;

        public const ushort C2S_ASK_ATTACK = 0x1400;
        public const ushort C2S_LEARN_SKILL = 0x1451;
        public const ushort C2S_ASK_SKILL = 0x1453;
        public const ushort C2S_SKILL_SLOT_INFO = 0x1461;
        public const ushort S2C_SKILL_SLOT_INFO = 0x1461;
        public const ushort C2S_ANS_RECALL = 0x1462;

        public const ushort C2S_ALLOT_POINT = 0x1602;
        public const ushort C2S_ASK_HEAL = 0x1606;
        public const ushort C2S_RETRIEVE_POINT = 0x1609;
        public const ushort C2S_RESTORE_EXP = 0x160C;
        public const ushort S2C_UNKNOWN_37_PROTOCOL = 0x1610;
        public const ushort C2S_LEARN_PSKILL = 0x1611;
        public const ushort C2S_FORGET_ALL_PSKILL = 0x1613;
        public const ushort C2S_ASK_OPEN_STORAGE = 0x1651;
        public const ushort C2S_ASK_INVEN2STORAGE = 0x1652;
        public const ushort C2S_ASK_STORAGE2INVEN = 0x1653;
        public const ushort C2S_ASK_DEPOSITE_MONEY = 0x1654;
        public const ushort C2S_ASK_WITHDRAW_MONEY = 0x1655;
        public const ushort C2S_ASK_CLOSE_STORAGE = 0x1656;
        public const ushort C2S_ASK_MOVE_ITEMINSTORAGE = 0x1657;

        public const ushort C2S_PICKUP_ITEM = 0x1702;
        public const ushort C2S_DROP_ITEM = 0x1704;
        public const ushort C2S_MOVE_ITEM = 0x1706;
        public const ushort C2S_WEAR_ITEM = 0x1708;
        public const ushort C2S_STRIP_ITEM = 0x1711;
        public const ushort C2S_BUY_ITEM = 0x1714;
        public const ushort C2S_SELL_ITEM = 0x1716;
        public const ushort C2S_GIVE_ITEM = 0x1718;
        public const ushort S2C_GIVE_ITEM = 0x1720;
        public const ushort C2S_USE_POTION = 0x1721;
        public const ushort C2S_ASK_DEAL = 0x1723;
        public const ushort S2C_ASK_DEAL = 0x1724;
        public const ushort C2S_ANS_DEAL = 0x1725;
        public const ushort C2S_PUTIN_ITEM = 0x1727;
        public const ushort S2C_PUTIN_ITEM = 0x1728;
        public const ushort C2S_PUTOUT_ITEM = 0x1729;
        public const ushort S2C_PUTOUT_ITEM = 0x1730;
        public const ushort C2S_DECIDE_DEAL = 0x1731;
        public const ushort C2S_CONFIRM_DEAL = 0x1733;
        public const ushort C2S_USE_ITEM = 0x1736;
        public const ushort C2S_CONFIRM_ITEM = 0x1742;
        public const ushort C2S_REMODEL_ITEM = 0x1744;
        public const ushort C2S_USESCROLL = 0x1748;
        public const ushort C2S_PUTIN_PET = 0x1750;
        public const ushort C2S_PUTOUT_PET = 0x1751;
        public const ushort C2S_ITEM_COMBINATION = 0x1753;
        public const ushort C2S_LOTTO_PURCHASE = 0x1754;
        public const ushort C2S_LOTTO_QUERY_PRIZE = 0x1755;
        public const ushort C2S_LOTTO_QUERY_HISTORY = 0x1756;
        public const ushort C2S_LOTTO_SALE = 0x1757;
        public const ushort C2S_TAKEITEM_IN_BOX = 0x1760;
        public const ushort C2S_TAKEITEM_OUT_BOX = 0x1761;
        public const ushort C2S_USE_POTION_EX = 0x1767;
        public const ushort C2S_OPEN_MARKET = 0x1770;
        public const ushort C2S_CLOSE_MARKET = 0x1771;
        public const ushort C2S_ENTER_MARKET = 0x1773;
        public const ushort C2S_BUYITEM_MARKET = 0x1775;
        public const ushort C2S_LEAVE_MARKET = 0x1776;
        public const ushort C2S_MODIFY_MARKET = 0x1777;
        public const ushort C2S_ASK_ITEM_SERIAL = 0x1780;
        public const ushort C2S_SOCKET_ITEM = 0x1781;
        public const ushort C2S_BUY_BATTLEFIELD_ITEM = 0x1785;
        public const ushort C2S_BUY_CASH_ITEM = 0x1790;
        public const ushort C2S_CASH_INFO = 0x1791;
        public const ushort C2S_DERBY_INDEX_QUERY = 0x17A9;
        public const ushort C2S_DERBY_MONSTER_QUERY = 0x17AA;
        public const ushort C2S_DERBY_RATIO_QUERY = 0x17AB;
        public const ushort C2S_DERBY_PURCHASE = 0x17AC;
        public const ushort C2S_DERBY_RESULT_QUERY = 0x17AE;
        public const ushort C2S_DERBY_HISTORY_QUERY = 0x17AF;
        public const ushort C2S_DERBY_EXCHANGE = 0x17B0;

        public const ushort C2S_SAY = 0x1800;
        public const ushort C2S_GESTURE = 0x1801;
        public const ushort C2S_CHAT_WINDOW_OPT = 0x1803;
        public const ushort S2C_CHAT_WINDOW_OPT = 0x1803;

        public const ushort C2S_OPTION = 0x1900;

        public const ushort C2S_PARTY_QUEST = 0x2110;
        public const ushort C2S_QUESTEX_DIALOGUE_REQ = 0x2120;
        public const ushort C2S_QUESTEX_DIALOGUE_ANS = 0x2122;
        public const ushort C2S_QUESTEX_CANCEL = 0x2126;
        public const ushort C2S_QUESTEX_LIST = 0x2128;
        public const ushort C2S_SQUEST_START = 0x2140;
        public const ushort C2S_SQUEST_STEP_END = 0x2144;
        public const ushort C2S_SQUEST_HISTORY = 0x2145;
        public const ushort C2S_SQUEST_MINIGAME_MOVE = 0x2149;
        public const ushort C2S_SQUEST_WALL_QUIZ = 0x214A;
        public const ushort C2S_SQUEST_WALL_OK = 0x214C;
        public const ushort C2S_SQUEST_A3_QUIZ_SELECT = 0x214E;
        public const ushort C2S_SQUEST_A3_QUIZ = 0x214F;
        public const ushort C2S_SQUEST_A3_QUIZ_OK = 0x2151;
        public const ushort C2S_SQUEST_END_OK = 0x2152;
        public const ushort C2S_SQUEST_222_NUM_QUIZ = 0x2153;
        public const ushort C2S_SQUEST_312_ITEM_CREATE = 0x2155;
        public const ushort C2S_SQUEST_HBOY_RUNE = 0x2157;
        public const ushort C2S_SQUEST_HBOY_HANOI = 0x2159;
        public const ushort C2S_SQUEST_346_ITEM_COMBI = 0x215E;

        public const ushort C2S_ASK_PARTY = 0x2200;
        public const ushort C2S_ANS_PARTY = 0x2202;
        public const ushort C2S_OUT_PARTY = 0x2205;
        public const ushort C2S_ASK_APPRENTICE_IN = 0x22A0;
        public const ushort C2S_ANS_APPRENTICE_IN = 0x22A1;
        public const ushort C2S_ASK_APPRENTICE_OUT = 0x22A4;

        public const ushort C2S_CLAN = 0x2300;
        public const ushort C2S_JOIN_CLAN = 0x2301;
        public const ushort C2S_ANS_CLAN = 0x2302;
        public const ushort C2S_BOLT_CLAN = 0x2303;
        public const ushort C2S_REQ_CLAN_INFO = 0x2304;
        public const ushort C2Z_REGISTER_MARK = 0x2320;
        public const ushort C2S_TRANSFER_MARK = 0x2322;
        public const ushort C2S_ASK_MARK = 0x2323;
        public const ushort C2S_FRIEND_INFO = 0x2331;
        public const ushort C2S_FRIEND_STATE = 0x2332;
        public const ushort S2C_FRIEND_STATE = 0x2332;
        public const ushort C2S_FRIEND_GROUP = 0x2333;
        public const ushort C2S_ASK_FRIEND = 0x2334;
        public const ushort C2S_ANS_FRIEND = 0x2335;
        public const ushort C2S_ASK_CLAN_BATTLE = 0x2340;
        public const ushort C2S_ANS_CLAN_BATTLE = 0x2341;
        public const ushort C2S_ASK_CLAN_BATTLE_END = 0x2342;
        public const ushort C2S_ANS_CLAN_BATTLE_END = 0x2343;
        public const ushort C2S_ASK_CLAN_BATTLE_SCORE = 0x2345;
        public const ushort C2S_LETTER_BASE_INFO = 0x2350;
        public const ushort C2S_LETTER_SIMPLE_INFO = 0x2351;
        public const ushort C2S_LETTER_DEL = 0x2353;
        public const ushort C2S_LETTER_SEND = 0x2354;
        public const ushort C2S_LETTER_KEEPING = 0x2356;

        public const ushort C2S_CHANGE_NATION = 0x2400;

        public const ushort C2S_CAO_MITIGATION = 0x2510;

        public const ushort C2S_AGIT_INFO = 0x2600;
        public const ushort C2S_AUCTION_INFO = 0x2601;
        public const ushort C2S_AGIT_ENTER = 0x2602;
        public const ushort C2S_AGIT_PUTUP_AUCTION = 0x2603;
        public const ushort C2S_AGIT_BIDON = 0x2604;
        public const ushort C2S_AGIT_PAY_EXPENSE = 0x2605;
        public const ushort C2S_AGIT_CHANGE_NAME = 0x2606;
        public const ushort C2S_AGIT_REPAY_MONEY = 0x2607;
        public const ushort C2S_AGIT_OBTAIN_SALEMONEY = 0x2608;
        public const ushort C2S_AGIT_MANAGE_INFO = 0x260A;
        public const ushort C2S_AGIT_OPTION = 0x260B;
        public const ushort C2S_AGIT_OPTION_INFO = 0x260C;
        public const ushort C2S_AGIT_PC_BAN = 0x260D;

        public const ushort C2S_CHRISTMAS_CARD = 0x2730;
        public const ushort C2S_SPEAK_CARD = 0x2731;
        public const ushort C2S_PROCESS_INFO = 0x2740;

        public const ushort C2S_ASK_WARP_Z2B = 0x3500;
        public const ushort C2S_ASK_WARP_B2Z = 0x3510;

        public const ushort C2S_ASK_SHOP_INFO = 0x3915;
        public const ushort C2S_ASK_GIVE_MY_TAX = 0x3916;

        public const ushort C2S_TYR_UNIT_LIST = 0x4001;
        public const ushort C2S_TYR_UNIT_INFO = 0x4002;
        public const ushort C2S_TYR_ENTRY = 0x4003;
        public const ushort C2S_TYR_JOIN = 0x4004;
        public const ushort C2S_TYR_REWARD_INFO = 0x4080;
        public const ushort C2S_TYR_REWARD = 0x4081;

        public const ushort C2S_TYR_UPGRADE = 0x4102;

        public const ushort C2S_TYR_RTMM_END = 0x4203;

        public const ushort C2S_HS_SEAL = 0x5001;
        public const ushort C2S_HS_RECALL = 0x5002;
        public const ushort C2S_HS_REVIVE = 0x5005;
        public const ushort C2S_HS_ASK_ATTACK = 0x5006;
        public const ushort C2S_HSSTONE_BUY = 0x5008;
        public const ushort C2S_HSSTONE_SELL = 0x5009;
        public const ushort C2S_HS_LEARN_SKILL = 0x500A;
        public const ushort C2S_HS_ALLOT_POINT = 0x500B;
        public const ushort C2S_HS_RETRIEVE_POINT = 0x500C;
        public const ushort C2S_HS_WEAR_ITEM = 0x500D;
        public const ushort C2S_HS_STRIP_ITEM = 0x5010;
        public const ushort C2S_HS_OPTION = 0x501B;
        public const ushort C2S_HS_HEAL = 0x501C;
        public const ushort C2S_HS_SKILL_RESET = 0x501E;

        public const ushort C2S_ASK_MIGRATION = 0x9000;

        public const ushort C2S_ASK_CREATE_PLAYER = 0xA001;
        public const ushort S2C_ANS_CREATE_PLAYER = 0xA001;
        public const ushort C2S_ASK_DELETE_PLAYER = 0xA002;
        public const ushort S2C_ANS_DELETE_PLAYER = 0xA002;

        public const ushort C2S_LEAGUE = 0xA340;
        public const ushort C2S_REQ_LEAGUE_CLAN_INFO = 0xA345;
        public const ushort C2S_LEAGUE_ALLOW = 0xA347;

        public const ushort C2S_PAYINFO = 0xC000;

        public const ushort C2S_PING = 0xF001;

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
            _sizes[C2S_PING] = 22;
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
