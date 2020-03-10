#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data.Game
{
    public class Item
    {
        public uint ItemCode { get; set; }
        public byte SlotIndex { get; set; }
        public string ItemName { get; set; }
        public byte ItemType { get; set; }
        public uint NPCPrice { get; set; }
        public byte ItemDiscardType { get; set; }
        public IT0ItemProperty IT0ItemProperty { get; set; }
        public IT1ItemProperty IT1ItemProperty { get; set; }
        public IT2ItemProperty IT2ItemProperty { get; set; }

        public Item()
        {
            this.IT0ItemProperty = new IT0ItemProperty();
            this.IT1ItemProperty = new IT1ItemProperty();
            this.IT2ItemProperty = new IT2ItemProperty();
        }
    }
}
