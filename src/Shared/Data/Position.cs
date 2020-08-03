#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.Globalization;
using Agonyl.Shared.Util;

namespace Agonyl.Shared.Data
{
    public class Position
    {
        /// <summary>
        /// X coordinate (left/right).
        /// </summary>
        public readonly byte X;

        /// <summary>
        /// Y coordinate (up/down).
        /// </summary>
        public readonly byte Y;

        /// <summary>
        /// Returns new position with X and Y being 0.
        /// </summary>
        public static Position Zero { get { return new Position(0, 0); } }

        /// <summary>
        /// Creates new position from coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Position(byte x, byte y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Creates new position from position.
        /// </summary>
        /// <param name="pos"></param>
        public Position(Position pos)
        {
            this.X = pos.X;
            this.Y = pos.Y;
        }

        /// <summary>
        /// Returns distance between this and another position
        /// </summary>
        /// <param name="otherPos"></param>
        /// <returns></returns>
        public double GetDistance(Position otherPos)
        {
            return Math.Sqrt(Math.Pow(this.X - otherPos.X, 2) + Math.Pow(this.Y - otherPos.Y, 2));
        }

        public ushort GetNumberRepresentation()
        {
            return Functions.BytesToUInt16(new byte[] { this.X, this.Y });
        }

        /// <summary>
        /// Returns true if other position is within given range
        /// </summary>
        /// <param name="otherPos"></param>
        /// <returns></returns>
        public bool InRange(Position otherPos, int range)
        {
            return Math.Pow(this.X - otherPos.X, 2) + Math.Pow(this.Y - otherPos.Y, 2) <= Math.Pow(range, 2);
        }

        /// <summary>
        /// Returns true if both positions represent the same position.
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public static bool operator ==(Position pos1, Position pos2)
        {
            return pos1.X == pos2.X && pos1.Y == pos2.Y;
        }

        /// <summary>
        /// Returns true if both positions represent the same position.
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public static bool operator !=(Position pos1, Position pos2)
        {
            return !(pos1 == pos2);
        }

        /// <summary>
        /// Returns string representation of position.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "X: {0}, Y: {1}", this.X, this.Y);
        }

        public static Position GetPositionFromNumber(ushort intPosition)
        {
            if (intPosition == 0)
            {
                return Zero;
            }

            var hexString = string.Format("{0:x}", intPosition);
            while (hexString.Length < 4)
            {
                hexString = "0" + hexString;
            }

            return new Position(Convert.ToByte(hexString[2] + hexString[3].ToString(), 16), Convert.ToByte(hexString[0] + hexString[1].ToString(), 16));
        }
    }
}
