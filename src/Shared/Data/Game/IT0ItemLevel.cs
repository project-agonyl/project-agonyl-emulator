#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data.Game
{
    public class IT0ItemLevel
    {
        public byte Level { get; set; }

        public ushort AttributeRange { get; set; }

        /// <summary>
        /// Attack/defense.
        /// </summary>
        public ushort Attribute { get; set; }

        public ushort Strength { get; set; }

        public ushort Intelligence { get; set; }

        public ushort Dexterity { get; set; }

        /// <summary>
        /// Additional attack/defense.
        /// </summary>
        public ushort AdditionalAttribute { get; set; }

        public ushort RedOption { get; set; }

        public ushort GreyOption { get; set; }

        public ushort BlueOption { get; set; }
    }
}
