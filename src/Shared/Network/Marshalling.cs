#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System.Runtime.InteropServices;

namespace Agonyl.Shared.Network
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Marshalling
    {
        /// <summary>
        /// Serialize the class
        /// </summary>
        /// <returns></returns>
        public byte[] Serialize()
        {
            // allocate a byte array for the struct data
            byte[] buffer = new byte[Marshal.SizeOf(this)];

            // Allocate a GCHandle and get the array pointer
            var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            var pBuffer = gch.AddrOfPinnedObject();

            // copy data from struct to array and unpin the gc pointer
            Marshal.StructureToPtr(this, pBuffer, false);
            gch.Free();

            return buffer;
        }

        /// <summary>
        /// Deserialize the class
        /// </summary>
        /// <param name="buffer"></param>
        public void Deserialize(ref byte[] buffer)
        {
            var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.PtrToStructure(gch.AddrOfPinnedObject(), this);
            gch.Free();
        }

        /// <summary>
        /// Deserialize the class from packet
        /// </summary>
        /// <param name="buffer"></param>
        public void Deserialize(ref Packet packet)
        {
            var gch = GCHandle.Alloc(packet.GetBuffer(), GCHandleType.Pinned);
            Marshal.PtrToStructure(gch.AddrOfPinnedObject(), this);
            gch.Free();
        }

        public uint GetSize()
        {
            return (uint)Marshal.SizeOf(this);
        }
    }
}
