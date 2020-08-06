#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Const
{
    public enum CharacterType : byte
    {
        Warrior = 0,
        Paladin = 1,
        Mage = 2,
        Archer = 3,
    }

    public enum PlayerState : byte
    {
        STANDBY = 0,
        TIMEOUT = 1,
        DIED = 2,
        STANDING = 3,
        MOVEOK = 4,
        MOVING = 5,
        OUTSIDE = 6,
        WARPWAIT = 7,
        LOGOUT = 8,
        PRISONER = 9,
    }

    public enum Town : byte
    {
        Temoz = 0,
        Quanato = 1,
    }

    public enum SlotType : byte
    {
        Quiver = 0,
        Shield = 0,
        Mace = 1,
        PaladinSword = 1,
        WarriorSword = 2,
        Spear = 2,
        Axe = 2,
        Staff = 2,
        Helmet = 3,
        Armor = 4,
        Pants = 5,
        Gloves = 6,
        Boots = 7,
        Necklace = 8,
        Ring = 9,
    }

    public enum ItemType : byte
    {
        Helmet = 0,
        Armor = 0,
        Pants = 0,
        Gloves = 0,
        Boots = 0,
        WarriorSword = 1,
        Axe = 2,
        Spear = 3,
        Necklace = 4,
        Ring = 5,
        PaladinSword = 5,
        Mace = 6,
        Shield = 7,
        Quiver = 10,
        Staff = 10,
    }
}
