#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Agonyl.Shared.Util
{
    public static class Functions
    {
        public static short ReverseHexBytesToInt16(byte[] bytes)
        {
            Array.Reverse(bytes);
            return BytesToInt16(bytes);
        }

        public static short BytesToInt16(byte[] bytes)
        {
            return BitConverter.ToInt16(bytes, 0);
        }

        public static int ReverseHexBytesToInt32(byte[] bytes)
        {
            Array.Reverse(bytes);
            return BytesToInt32(bytes);
        }

        public static int BytesToInt32(byte[] bytes)
        {
            return BitConverter.ToInt32(bytes, 0);
        }

        public static long ReverseHexBytesToInt64(byte[] bytes)
        {
            Array.Reverse(bytes);
            return BytesToInt64(bytes);
        }

        public static long BytesToInt64(byte[] bytes)
        {
            return BitConverter.ToInt64(bytes, 0);
        }

        public static byte[] ByteSlice(ref byte[] source, int StartIndex, int length)
        {
            var dest = new byte[length];
            Array.Copy(source, StartIndex, dest, 0, length);
            return dest;
        }

        public static string InsertWearIntoMbody(string MBody, List<string> Wear)
        {
            var toInsert = "";
            foreach (var item in Wear)
            {
                var rnd = new Random();
                if (toInsert == "")
                {
                    toInsert += item + ";" + rnd.Next(10000000, 999999999).ToString();
                }
                else
                {
                    toInsert += ";" + item + ";" + rnd.Next(10000000, 999999999).ToString();
                }
            }
            var splitArray = MBody.Split('\\');
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
