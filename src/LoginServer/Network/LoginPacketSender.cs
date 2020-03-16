#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using Agonyl.Shared.Network;

namespace Agonyl.Login.Network
{
    public static class Send
    {
        /// <summary>
        /// Sends custom message to client.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="msg"></param>
        public static void S2C_LOGIN_MESSAGE(LoginConnection conn, string msg)
        {
            var packet = new Packet(Op.S2C_LOGIN_MESSAGE);
            var header = new byte[] { 0x5c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0xe0, 0x01 };
            if (msg.Length > 70)
            {
                msg = msg.Substring(0, 69);
            }

            packet.PutBytes(header);
            packet.PutString(msg);
            packet.PutEmptyBin(92 - 11 - msg.Length);
            conn.Send(packet);
        }

        /// <summary>
        /// Sends server list.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="serverName"></param>
        public static void S2C_SERVER_LIST(LoginConnection conn, string serverName)
        {
            var packet = new Packet(Op.S2C_SERVER_LIST);
            var header = new byte[] { 0x6f, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0xe1, 0x01, 0x00, 0x00 };
            packet.PutBytes(header);
            packet.PutString(serverName);
            packet.PutEmptyBin(13 - serverName.Length);
            packet.PutByte(0x0e);
            packet.PutEmptyBin(3);
            packet.PutString("ONLINE");
            packet.PutEmptyBin(111 - packet.Length);
            conn.Send(packet);
        }

        /// <summary>
        /// Sends login ok message to client.
        /// </summary>
        /// <param name="conn"></param>
        public static void S2C_LOGIN_OK(LoginConnection conn)
        {
            var packet = new Packet(Op.S2C_LOGIN_OK);
            packet.PutByte(0x0a);
            packet.PutEmptyBin(7);
            packet.PutByte(0x01);
            packet.PutByte(0xff);
            conn.Send(packet);
        }

        /// <summary>
        /// Sends game server details to client.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="serverHost"></param>
        /// <param name="serverPort"></param>
        public static void S2C_SERVER_DETAILS(LoginConnection conn, string serverHost, int serverPort)
        {
            var packet = new Packet(Op.S2C_SERVER_DETAILS);
            var header = new byte[] { 0x22, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0xe2, 0x11, 0x38, 0x54, 0x00 };
            packet.PutBytes(header);
            packet.PutString(serverHost);
            packet.PutEmptyBin(16 - serverHost.Length);
            packet.PutReverseHexOfInt(serverPort);
            conn.Send(packet);
        }
    }
}
