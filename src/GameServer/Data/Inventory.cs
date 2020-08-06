#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.Collections.Generic;
using Agonyl.Shared.Network;

namespace Agonyl.Game.Data
{
    public class Inventory
    {
        private Character _character;
        private object _inventorySyncLock = new object();
        private object _wearSyncLock = new object();
        private InventoryItem[] _inventoryItems;
        private WearItem[] _wearItems;

        public Inventory(Character character)
        {
            this._inventoryItems = new InventoryItem[30];
            for (var i = 0; i < 30; i++)
            {
                this._inventoryItems[i] = new InventoryItem((byte)i);
            }

            this._wearItems = new WearItem[10];
            for (var i = 0; i < 10; i++)
            {
                this._wearItems[i] = new WearItem(0);
            }

            this._character = character;
        }

        public void AddInventoryItem(byte slot, InventoryItem inventoryItem)
        {
            lock (this._inventorySyncLock)
            {
                this._inventoryItems[slot] = inventoryItem;
            }
        }

        public InventoryItem RemoveInventoryItem(byte slot)
        {
            lock (this._inventorySyncLock)
            {
                var inventoryItem = this._inventoryItems[slot];
                this._inventoryItems[slot] = new InventoryItem(slot);
                return inventoryItem;
            }
        }

        public InventoryItem[] GetInventoryItems()
        {
            lock (this._inventorySyncLock)
            {
                return this._inventoryItems;
            }
        }

        public ITEM_INVENTORY[] GetInventoryMsgStructure()
        {
            lock (this._inventorySyncLock)
            {
                var items = new List<ITEM_INVENTORY>();
                for (byte i = 0; i < this._inventoryItems.Length; i++)
                {
                    if (this._inventoryItems[i].BaseId != 0)
                    {
                        var item = new ITEM();
                        item.ItemId = new ITEM_ID();
                        item.ItemId.ItemPtr = (uint)this._inventoryItems[i].WorldItemHandle;
                        item.ItemId.ItemCode = this._inventoryItems[i].CurrentId;
                        item.ItemOption = this._inventoryItems[i].CurrentOption;
                        item.ItemKey = this._inventoryItems[i].SerialKey;
                        items.Add(new ITEM_INVENTORY()
                        {
                            InventoryIndex = this._inventoryItems[i].Slot,
                            Item = item,
                        });
                    }
                }

                while (items.Count != 30)
                {
                    items.Add(default(ITEM_INVENTORY));
                }

                return items.ToArray();
            }
        }

        public void AddWearItem(byte slot, WearItem wearItem)
        {
            lock (this._wearSyncLock)
            {
                this._wearItems[slot] = wearItem;
            }
        }

        public WearItem RemoveWearItem(byte slot)
        {
            lock (this._wearSyncLock)
            {
                var wearItem = this._wearItems[slot];
                this._wearItems[slot] = new WearItem(slot);
                return wearItem;
            }
        }

        public WearItem[] GetWearItems()
        {
            lock (this._wearSyncLock)
            {
                return this._wearItems;
            }
        }

        public ITEM_WEAR[] GetWearMsgStructure()
        {
            lock (this._wearSyncLock)
            {
                var items = new List<ITEM_WEAR>();
                for (byte i = 0; i < this._wearItems.Length; i++)
                {
                    var item = new ITEM();
                    item.ItemId = new ITEM_ID();
                    item.ItemId.ItemPtr = (uint)this._wearItems[i].WorldItemHandle;
                    item.ItemId.ItemCode = this._wearItems[i].CurrentId;
                    item.ItemOption = this._wearItems[i].CurrentOption;
                    item.ItemKey = this._wearItems[i].SerialKey;
                    items.Add(new ITEM_WEAR()
                    {
                        WearIndex = this._wearItems[i].Slot,
                        Item = item,
                    });
                }

                while (items.Count != 10)
                {
                    items.Add(default(ITEM_WEAR));
                }

                return items.ToArray();
            }
        }
    }
}
