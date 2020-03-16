#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

namespace Agonyl.Shared.Util.Security
{
    public class Crypt
    {
        private int m_ConstKey1;
        private int m_ConstKey2;
        private int m_DynamicKey;
        private byte p_DynamicKey1;
        private byte p_DynamicKey2;
        private uint p_ConstKey_En;
        private uint p_ConstKey_De;

        public Crypt()
        {
            this.m_ConstKey1 = 0x241AE7;
            this.m_ConstKey2 = 0x15DCB2;
            this.m_DynamicKey = 0x4C478BD;
            this.p_DynamicKey1 = 0x02;
            this.p_DynamicKey2 = 0x01;
            this.p_ConstKey_En = 0xA7F0753B;
            this.p_ConstKey_De = 0xAAF29BF3;
        }

        public Crypt(int constKey1, int constKey2, int dynamicKey)
        {
            this.m_ConstKey1 = constKey1;
            this.m_ConstKey2 = constKey2;
            this.m_DynamicKey = dynamicKey;
            this.p_DynamicKey1 = 0x02;
            this.p_DynamicKey2 = 0x01;
            this.p_ConstKey_En = 0xA7F0753B;
            this.p_ConstKey_De = 0xAAF29BF3;
        }

        public Crypt(int constKey1, int constKey2, int dynamicKey, byte dynamicKey1, byte dynamicKey2, uint constKey_En, uint constKey_De)
        {
            this.m_ConstKey1 = constKey1;
            this.m_ConstKey2 = constKey2;
            this.m_DynamicKey = dynamicKey;
            this.p_DynamicKey1 = dynamicKey1;
            this.p_DynamicKey2 = dynamicKey2;
            this.p_ConstKey_En = constKey_En;
            this.p_ConstKey_De = constKey_De;
        }

        public void Decrypt(ref byte[] buffer)
        {
            int sOffset = 0x0C;
            for (int i = sOffset; i + 4 <= buffer.Length; i += 4)
            {
                int dynamicKey = this.m_DynamicKey;
                for (int j = i; j < i + 4; j++)
                {
                    byte pSrc = buffer[j];
                    buffer[j] = (byte)(buffer[j] ^ (dynamicKey >> 8));
                    dynamicKey = (pSrc + dynamicKey) * this.m_ConstKey1 + this.m_ConstKey2;
                }
            }
        }

        public void Encrypt(ref byte[] buffer)
        {
            int sOffset = 0x0C;
            for (var i = sOffset; i + 4 <= buffer.Length; i += 4)
            {
                int dynamicKey = this.m_DynamicKey;
                for (int j = i; j < i + 4; j++)
                {
                    buffer[j] = (byte)(buffer[j] ^ (dynamicKey >> 8));
                    dynamicKey = (buffer[j] + dynamicKey) * this.m_ConstKey1 + this.m_ConstKey2;
                }
            }
        }
    }
}
