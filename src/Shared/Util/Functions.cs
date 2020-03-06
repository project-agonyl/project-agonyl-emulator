#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;

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
    }
}
