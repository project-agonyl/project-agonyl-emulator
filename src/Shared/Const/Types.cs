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
}
