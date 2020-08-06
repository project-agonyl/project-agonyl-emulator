#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.ComponentModel.DataAnnotations;

namespace Agonyl.Game.Data
{
    public class WearItem : BaseItem
    {
        [Range(0, 9, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public byte Slot;

        public WearItem(uint itemId, uint itemOption, uint serialKey, int worldItemHandle, byte slot)
            : base(itemId, itemOption, serialKey, worldItemHandle)
        {
            this.Slot = slot;
        }

        public WearItem(byte slot)
            : base(0, 0, 0, 0)
        {
            this.Slot = slot;
        }
    }
}
