#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Data.Game
{
    public class IT0ItemLevel
    {
        /// <summary>
        /// Additional attack/defense
        /// </summary>
        public byte AdditionalAttribute { get; set; }

        public byte Level { get; set; }

        /// <summary>
        /// Attack/defense
        /// </summary>
        public short Attribute { get; set; }

        public short AttackRange { get; set; }
        public byte RedOption { get; set; }
        public byte GreyOption { get; set; }
        public byte BlueOption { get; set; }
    }
}
