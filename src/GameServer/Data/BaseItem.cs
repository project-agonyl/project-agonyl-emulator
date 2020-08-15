#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.ComponentModel.DataAnnotations;
using Agonyl.Shared.Data;

namespace Agonyl.Game.Data
{
    public abstract class BaseItem
    {
        public ushort BaseId { get; set; }

        public uint CurrentId { get; set; }

        [Range(0, 3, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public byte MountLevel { get; set; }

        public bool HasBlessing { get; set; }

        public ushort BaseOption { get; set; }

        public uint CurrentOption { get; set; }

        public bool HasAdditionalAttribute { get; set; }

        public byte Level { get; set; }

        public byte BlueOption { get; set; }

        public byte RedOption { get; set; }

        public byte GreyOption { get; set; }

        public uint SerialKey { get; set; }

        // Use it in the future to identify each item in server
        public int WorldItemHandle { get; set; }

        public BaseItem(uint itemId, uint itemOption, uint serialKey, int worldItemHandle)
        {
            this.CurrentId = itemId;
            this.CurrentOption = itemOption;
            this.SerialKey = serialKey;
            this.WorldItemHandle = worldItemHandle;
            this.BaseId = (ushort)(itemId & 0x3FFF);
            while (itemId > Constants.ItemMountConstant)
            {
                this.MountLevel++;
                itemId -= Constants.ItemMountConstant;
            }

            while (itemId > Constants.ItemBlessingConstant)
            {
                this.HasBlessing = true;
                itemId -= Constants.ItemBlessingConstant;
            }

            var additionalAttribute = itemOption & 0x10;
            this.HasAdditionalAttribute = additionalAttribute != 0;
            this.Level = (byte)(itemOption & 0xF) == 0 ? (byte)1 : (byte)(itemOption & 0xF);
            while (itemOption > Constants.ItemGreyOptionConstant)
            {
                itemOption -= Constants.ItemGreyOptionConstant;
                this.GreyOption++;
                if (this.GreyOption == 63)
                {
                    break;
                }
            }

            while (itemOption > Constants.ItemBlueOptionConstant)
            {
                itemOption -= Constants.ItemBlueOptionConstant;
                this.BlueOption++;
                if (this.BlueOption == 63)
                {
                    break;
                }
            }

            while (itemOption > Constants.ItemRedOptionConstant)
            {
                itemOption -= Constants.ItemRedOptionConstant;
                this.RedOption++;
                if (this.RedOption == 63)
                {
                    break;
                }
            }

            if (this.HasAdditionalAttribute)
            {
                itemOption -= Constants.ItemAdditionalAttributeConstant;
            }

            this.BaseOption = (ushort)(itemOption - this.Level);
        }
    }
}
