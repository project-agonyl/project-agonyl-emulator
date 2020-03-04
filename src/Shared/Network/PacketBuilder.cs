#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Network
{
    public class PacketBuilder
    {
        protected int _packetSize;
        protected int _uid = 0;
        protected byte _ctrl = 0;
        protected byte _cmd = 0;
        protected int _protocol = 0;

        public PacketBuilder(int PacketSize, byte Control, byte Command, int Protocol, int uid = 0)
        {
            _packetSize = PacketSize;
            _ctrl = Control;
            _cmd = Command;
            _protocol = Protocol;
            _uid = uid;
        }

        /// <summary>
        /// Builds the packet
        /// </summary>
        /// <param name="packet"></param>
        public void Build(ref Packet packet)
        {
            this._buildHeader(ref packet);
            packet.PutEmptyBin(_packetSize - packet.Length);
        }

        /// <summary>
        /// Builds the packet header
        /// </summary>
        /// <param name="packet"></param>
        protected void _buildHeader(ref Packet packet)
        {
            packet.PutReverseHexOfInt(_packetSize);
            packet.PutReverseHexOfInt(_uid);
            packet.PutByte(_ctrl);
            packet.PutByte(_cmd);
            packet.PutReverseHexOfInt(_protocol, 2);
        }
    }
}
