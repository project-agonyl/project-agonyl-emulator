#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;

namespace Agonyl.Shared.Util
{
    public static class Functions
    {
        public static ushort ReverseHexBytesToUInt16(byte[] bytes)
        {
            Array.Reverse(bytes);
            return BytesToUInt16(bytes);
        }

        public static ushort BytesToUInt16(byte[] bytes)
        {
            return BitConverter.ToUInt16(bytes, 0);
        }

        public static uint ReverseHexBytesToUInt32(byte[] bytes)
        {
            Array.Reverse(bytes);
            return BytesToUInt32(bytes);
        }

        public static uint BytesToUInt32(byte[] bytes)
        {
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static ulong ReverseHexBytesToUInt64(byte[] bytes)
        {
            Array.Reverse(bytes);
            return BytesToUInt64(bytes);
        }

        public static ulong BytesToUInt64(byte[] bytes)
        {
            return BitConverter.ToUInt64(bytes, 0);
        }

        public static byte[] ByteSlice(ref byte[] source, int startIndex, int length)
        {
            var dest = new byte[length];
            Array.Copy(source, startIndex, dest, 0, length);
            return dest;
        }

        public static string InsertWearIntoMbody(string mBody, List<string> wear)
        {
            var toInsert = string.Empty;
            foreach (var item in wear)
            {
                var rnd = new Random();
                if (toInsert == string.Empty)
                {
                    toInsert += item + ";" + rnd.Next(10000000, 999999999).ToString();
                }
                else
                {
                    toInsert += ";" + item + ";" + rnd.Next(10000000, 999999999).ToString();
                }
            }

            var splitArray = mBody.Split('\\');
            for (var i = 0; i < splitArray.Length; i++)
            {
                if (splitArray[i].StartsWith("_1WEAR"))
                {
                    splitArray[i] = "_1WEAR=" + toInsert;
                }
            }

            return string.Join("\\", splitArray).Trim();
        }
    }
}
