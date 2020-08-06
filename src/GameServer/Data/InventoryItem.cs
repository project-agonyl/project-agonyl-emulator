#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.ComponentModel.DataAnnotations;

namespace Agonyl.Game.Data
{
    public class InventoryItem : BaseItem
    {
        [Range(0, 29, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public byte Slot;

        public InventoryItem(uint itemId, uint itemOption, uint serialKey, int worldItemHandle, byte slot)
            : base(itemId, itemOption, serialKey, worldItemHandle)
        {
            this.Slot = slot;
        }

        public InventoryItem(byte slot)
            : base(0, 0, 0, 0)
        {
            this.Slot = slot;
        }
    }
}
