#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Agonyl.Shared.Network
{
    public class Packet
    {
        private const int DefaultSize = 1024;

        private byte[] _buffer;
        private int _ptr;

        /// <summary>
        /// Length of the packet.
        /// </summary>
        public int Length { get; protected set; }

        /// <summary>
        /// Packet's op.
        /// </summary>
        public ushort Op { get; protected set; }

        /// <summary>
        /// Creates packet from buffer coming from client.
        /// </summary>
        /// <param name="buffer"></param>
        public Packet(byte[] buffer)
        {
            this._buffer = buffer;

            this.Length = buffer.Length;
            this.Op = this.GetOpCode();
        }

        /// <summary>
        /// Creates packet from buffer coming from client.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="opIndex"></param>
        public Packet(byte[] buffer, int opIndex)
        {
            this._buffer = buffer;

            this.Length = buffer.Length;
            this.Op = buffer[opIndex];
        }

        /// <summary>
        /// Creates new packet with given op.
        /// </summary>
        /// <param name="op"></param>
        public Packet(ushort op)
        {
            this._buffer = new byte[DefaultSize];
            this.Op = op;
        }

        /// <summary>
        /// Throws if not enough bytes are left to read a value with the given length.
        /// </summary>
        /// <param name="needed"></param>
        private void AssertGotEnough(int needed)
        {
            if (this._ptr + needed > this.Length)
            {
                throw new InvalidOperationException("Not enough bytes left to read a '" + needed + "' byte value.");
            }
        }

        /// <summary>
        /// Increases buffer size if more space is needed to fit the given
        /// amount of bytes.
        /// </summary>
        /// <param name="needed"></param>
        private void EnsureSpace(int needed)
        {
            if (this._ptr + needed > this.Length)
            {
                Array.Resize(ref this._buffer, this._buffer.Length + DefaultSize);
            }
        }

        /// <summary>
        /// Reads byte from buffer.
        /// </summary>
        public byte GetByte()
        {
            this.AssertGotEnough(1);

            var val = this._buffer[this._ptr];
            this._ptr += sizeof(byte);

            return val;
        }

        /// <summary>
        /// Reads byte from buffer and returns it as bool (true != 0).
        /// </summary>
        public bool GetBool()
        {
            return (this.GetByte() != 0);
        }

        /// <summary>
        /// Reads short from buffer.
        /// </summary>
        public short GetShort()
        {
            this.AssertGotEnough(2);

            var val = BitConverter.ToInt16(this._buffer, this._ptr);
            this._ptr += sizeof(short);

            return val;
        }

        /// <summary>
        /// Reads int from buffer.
        /// </summary>
        public int GetInt()
        {
            this.AssertGotEnough(4);

            var val = BitConverter.ToInt32(this._buffer, this._ptr);
            this._ptr += sizeof(int);

            return val;
        }

        /// <summary>
        /// Reads long from buffer.
        /// </summary>
        public long GetLong()
        {
            this.AssertGotEnough(8);

            var val = BitConverter.ToInt64(this._buffer, this._ptr);
            this._ptr += sizeof(long);

            return val;
        }

        /// <summary>
        /// Reads float from buffer.
        /// </summary>
        public float GetFloat()
        {
            this.AssertGotEnough(4);

            var val = BitConverter.ToSingle(this._buffer, this._ptr);
            this._ptr += sizeof(float);

            return val;
        }

        /// <summary>
        /// Reads given number of bytes from buffer and returns them as
        /// UTF8 string. Stops reading at the first null-byte.
        /// </summary>
        /// <param name="length"></param>
        public string GetString(int length)
        {
            this.AssertGotEnough(length);

            var val = Encoding.UTF8.GetString(this._buffer, this._ptr, length);

            // Relatively fast way to get rid of null bytes.
            var nullIndex = val.IndexOf((char)0);
            if (nullIndex != -1)
            {
                val = val.Substring(0, nullIndex);
            }

            this._ptr += length;

            return val;
        }

        /// <summary>
        /// Reads length-prefixed string from buffer and returns it as
        /// UTF8 string.
        /// </summary>
        public string GetLpString()
        {
            this.AssertGotEnough(sizeof(short));

            var length = this.GetShort();
            return this.GetString(length);
        }

        /// <summary>
        /// Reads null-terminated string from buffer and returns it as UTF8.
        /// </summary>
        /// <param name="length"></param>
        public string GetString()
        {
            for (var i = this._ptr; i < this._buffer.Length; ++i)
            {
                if (this._buffer[i] == 0)
                {
                    var val = Encoding.UTF8.GetString(this._buffer, this._ptr, i - this._ptr);
                    this._ptr += val.Length + 1;
                    return val;
                }
            }

            throw new Exception("String not null-terminated.");
        }

        /// <summary>
        /// Reads struct from buffer.
        /// </summary>
        /// <typeparam name="TStruct"></typeparam>
        public TStruct GetStruct<TStruct>() where TStruct : new()
        {
            var type = typeof(TStruct);
            if (!type.IsValueType || type.IsPrimitive)
            {
                throw new Exception("GetObj can only marshal to structs.");
            }

            var size = Marshal.SizeOf(typeof(TStruct));
            var buffer = this.GetBin(size);

            var intPtr = Marshal.AllocHGlobal(buffer.Length);
            Marshal.Copy(buffer, 0, intPtr, buffer.Length);
            var val = Marshal.PtrToStructure(intPtr, typeof(TStruct));
            Marshal.FreeHGlobal(intPtr);

            return (TStruct)val;
        }

        /// <summary>
        /// Reads given amount of bytes from buffer.
        /// </summary>
        /// <param name="length"></param>
        public byte[] GetBin(int length)
        {
            this.AssertGotEnough(length);

            var val = new byte[length];
            Buffer.BlockCopy(this._buffer, this._ptr, val, 0, length);
            this._ptr += length;

            return val;
        }

        /// <summary>
        /// Returns all the bytes from buffer.
        /// </summary>
        public byte[] GetBuffer()
        {
            return this._buffer;
        }

        /// <summary>
        /// Reads given amount of bytes from buffer and returns them as hex string.
        /// </summary>
        /// <param name="length"></param>
        public string GetBinAsHex(int length)
        {
            return BitConverter.ToString(this.GetBin(length)).ToString().Replace("-", string.Empty).ToUpper();
        }

        /// <summary>
        /// Writes byte array to buffer.
        /// </summary>
        /// <param name="val"></param>
        public void PutBytes(byte[] val)
        {
            for (var i = 0; i < val.Length; i++)
            {
                this.PutByte(val[i]);
            }
        }

        /// <summary>
        /// Writes byte to buffer.
        /// </summary>
        /// <param name="val"></param>
        public void PutByte(byte val)
        {
            this.EnsureSpace(1);

            this._buffer[this._ptr++] = (byte)(val);
            this.Length += sizeof(byte);
        }

        /// <summary>
        /// Writes value as byte (0|1) to buffer.
        /// </summary>
        /// <param name="val"></param>
        public void PutByte(bool val)
        {
            this.PutByte(val ? (byte)1 : (byte)0);
        }

        /// <summary>
        /// Writes short to buffer.
        /// </summary>
        /// <param name="val"></param>
        public void PutShort(int val)
        {
            this.EnsureSpace(2);

            this._buffer[this._ptr++] = (byte)(val >> (8 * 0));
            this._buffer[this._ptr++] = (byte)(val >> (8 * 1));
            this.Length += sizeof(short);
        }

        /// <summary>
        /// Writes int to buffer.
        /// </summary>
        /// <param name="val"></param>
        public void PutInt(int val)
        {
            this.EnsureSpace(4);

            this._buffer[this._ptr++] = (byte)(val >> (8 * 0));
            this._buffer[this._ptr++] = (byte)(val >> (8 * 1));
            this._buffer[this._ptr++] = (byte)(val >> (8 * 2));
            this._buffer[this._ptr++] = (byte)(val >> (8 * 3));
            this.Length += sizeof(int);
        }

        public void PutLong(long val)
        {
            this.EnsureSpace(8);

            this._buffer[this._ptr++] = (byte)(val >> (8 * 0));
            this._buffer[this._ptr++] = (byte)(val >> (8 * 1));
            this._buffer[this._ptr++] = (byte)(val >> (8 * 2));
            this._buffer[this._ptr++] = (byte)(val >> (8 * 3));
            this._buffer[this._ptr++] = (byte)(val >> (8 * 4));
            this._buffer[this._ptr++] = (byte)(val >> (8 * 5));
            this._buffer[this._ptr++] = (byte)(val >> (8 * 6));
            this._buffer[this._ptr++] = (byte)(val >> (8 * 7));
            this.Length += sizeof(long);
        }

        /// <summary>
        /// Writes float to buffer.
        /// </summary>
        /// <param name="val"></param>
        public void PutFloat(float val)
        {
            this.EnsureSpace(4);

            var bVal = BitConverter.GetBytes(val);
            this._buffer[this._ptr++] = bVal[0];
            this._buffer[this._ptr++] = bVal[1];
            this._buffer[this._ptr++] = bVal[2];
            this._buffer[this._ptr++] = bVal[3];
            this.Length += sizeof(float);
        }

        /// <summary>
        /// Writes string of given length to buffer.
        /// </summary>
        /// <remarks>
        /// If length is greater than string's length it's filled with null bytes.
        /// </remarks>
        /// <param name="val"></param>
        /// <param name="length"></param>
        public void PutString(string val, int length)
        {
            this.EnsureSpace(length);

            if (val == null)
            {
                val = string.Empty;
            }

            var bytes = Encoding.UTF8.GetBytes(val);
            Buffer.BlockCopy(bytes, 0, this._buffer, this._ptr, Math.Min(bytes.Length, length));
            this._ptr += length;
            this.Length += length;
        }

        /// <summary>
        /// Writes null-terminated string to buffer.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="terminator"></param>
        public void PutString(string val, bool terminator = false)
        {
            if (val == null)
            {
                val = string.Empty;
            }

            // Append terminator
            if (val == string.Empty || (val.Length > 0 && val[val.Length - 1] != '\0' && terminator))
            {
                val += '\0';
            }

            this.PutBin(Encoding.UTF8.GetBytes(val));
        }

        /// <summary>
        /// Writes string to buffer, incl. null terminator, and prefixed
        /// with a short describing its length.
        /// </summary>
        /// <param name="val"></param>
        public void PutLpString(string val)
        {
            if (val == null)
            {
                val = string.Empty;
            }

            // Append terminator
            if (val == string.Empty || (val.Length > 0 && val[val.Length - 1] != '\0'))
            {
                val += '\0';
            }

            var bytes = Encoding.UTF8.GetBytes(val);
            this.PutShort(bytes.Length);
            this.PutBin(bytes);
        }

        /// <summary>
        /// Writes bytes to buffer.
        /// </summary>
        /// <param name="val"></param>
        public void PutBin(params byte[] val)
        {
            this.EnsureSpace(val.Length);

            Buffer.BlockCopy(val, 0, this._buffer, this._ptr, val.Length);
            this._ptr += val.Length;
            this.Length += val.Length;
        }

        /// <summary>
        /// Writes bytes parsed from given hex string to buffer.
        /// </summary>
        /// <param name="hex"></param>
        public void PutBinFromHex(string hex)
        {
            if (hex == null)
            {
                throw new ArgumentNullException("hex");
            }

            hex = hex.Trim().Replace(" ", string.Empty).Replace("-", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);

            if (hex == string.Empty)
            {
                return;
            }

            var val =
                Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();

            this.PutBin(val);
        }

        /// <summary>
        /// Writes the given amount of bytes to the buffer.
        /// </summary>
        /// <param name="amount"></param>
        public void PutEmptyBin(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            this.PutBin(new byte[amount]);
        }

        /// <summary>
        /// Writes struct to buffer.
        /// </summary>
        /// <param name="val"></param>
        public void PutBin(object val)
        {
            var type = val.GetType();
            if (!type.IsValueType || type.IsPrimitive)
            {
                throw new Exception("PutBin only takes byte[] and structs.");
            }

            var size = Marshal.SizeOf(val);
            var arr = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(val, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            this.PutBin(arr);
        }

        /// <summary>
        /// Writes the reverse hex of the given integer.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        public void PutReverseHexOfInt(int value, int length = 4)
        {
            var tempByte = this.IntToReverseHexBytes(value, length);
            this.PutBytes(tempByte);
        }

        public void Zlib(bool compress, Action<Packet> zlibPacketFunc)
        {
            // If uncompressed, empty zlib header, followed by the raw data.
            if (compress == false)
            {
                this.PutShort(0);
                zlibPacketFunc(this);
            }

            // If compressed, write data into a new, temporary packet,
            // and then write the data from that packet into this one,
            // after compressing it.
            else
            {
                var zlibPacket = new Packet(this.Op);
                zlibPacketFunc(zlibPacket);

                var buffer = zlibPacket._buffer;
                var len = zlibPacket.Length;

                using (var ms = new MemoryStream())
                {
                    using (var ds = new DeflateStream(ms, CompressionMode.Compress))
                    {
                        ds.Write(buffer, 0, len);
                    }

                    var compressedVal = ms.ToArray();

                    this.PutShort(0xFA8D); // zlib header
                    this.PutInt(compressedVal.Length);
                    this.PutBin(compressedVal);
                }
            }
        }

        /// <summary>
        /// Copies packet data to given buffer at offset.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        public byte[] Build(ref byte[] buffer, int offset)
        {
            Buffer.BlockCopy(this._buffer, 0, buffer, offset, this.Length);

            return this._buffer;
        }

        /// <summary>
        /// Returns buffer as formatted hex string.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            var buffer = this._buffer;
            var length = this.Length;
            var tableSize = Network.Op.GetSize(this.Op);
            var opName = Network.Op.GetName(this.Op);
            var spacer = string.Empty.PadLeft(78, '-');

            sb.AppendLine(spacer);
            sb.AppendFormat("Op: {0:X4} {1}, Size: {2}", this.Op, opName, length);
            if (tableSize != 0)
            {
                sb.AppendFormat(" (Table: {0}, Garbage: {1})", tableSize, length - tableSize);
            }

            sb.AppendLine();
            sb.AppendLine(spacer);

            for (var i = 0; i < length; i += 16)
            {
                sb.AppendFormat("{0:X4}   ", i);

                var k = 0;
                for (var j = 0; j < 16; ++j, ++k)
                {
                    if (i + j > length - 1)
                    {
                        break;
                    }

                    sb.Append(buffer[i + j].ToString("X2") + ' ');
                    if (j == 7)
                    {
                        sb.Append(' ');
                    }
                }

                sb.Append(string.Empty.PadLeft((16 - k) * 3, ' '));
                sb.Append("  ");

                for (var j = 0; j < 16; ++j)
                {
                    if (i + j > length - 1)
                    {
                        break;
                    }

                    var b = buffer[i + j];
                    // Only display ANSI characters, and no curly braces.
                    if (b >= 32 && b <= 126 && b != 123 && b != 125)
                    {
                        sb.Append((char)b);
                    }
                    else
                    {
                        sb.Append('.');
                    }
                }

                sb.AppendLine();
            }

            sb.AppendLine(spacer);

            return sb.ToString().Trim();
        }

        public void SetReadPointer(int index = 0)
        {
            if (index >= this.Length)
            {
                index = this.Length - 1;
            }

            this._ptr = index;
        }

        /// <summary>
        /// Returns packet with byte equivalent of int.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="length"></param>
        private byte[] IntToReverseHexBytes(int num, int length = 4)
        {
            var hexPort = string.Format("{0:x}", num);
            while (hexPort.Length < length * 2)
            {
                hexPort = "0" + hexPort;
            }

            var tempByte = new byte[length];
            var j = 0;
            for (var i = hexPort.Length - 1; i > 0; i -= 2)
            {
                if (j == length)
                {
                    break;
                }

                tempByte[j] = Convert.ToByte(hexPort[i - 1] + hexPort[i].ToString(), 16);
                j++;
            }

            return tempByte;
        }

        private ushort GetOpCode()
        {
            ushort opCode;
            switch (this._buffer[11])
            {
                case 0x0F:
                    switch (this._buffer[10])
                    {
                        case 0xF2:
                            opCode = Network.Op.C2S_KEEP_ALIVE;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x11:
                    switch (this._buffer[10])
                    {
                        case 0x06:
                            opCode = Network.Op.C2S_CHAR_LOGIN;
                            break;

                        case 0x07:
                            opCode = Network.Op.C2S_WORLD_LOGIN;
                            break;

                        case 0x08:
                            opCode = Network.Op.C2S_CHAR_LOGOUT;
                            break;

                        case 0x11:
                            opCode = Network.Op.C2S_WARP;
                            break;

                        case 0x12:
                            opCode = Network.Op.C2S_RETURN2HERE;
                            break;

                        case 0x14:
                            opCode = Network.Op.C2S_SUBMAP_INFO;
                            break;

                        case 0x15:
                            opCode = Network.Op.C2S_ENTER;
                            break;

                        case 0xA1:
                            opCode = Network.Op.C2S_ACTIVE_PET;
                            break;

                        case 0xA2:
                            opCode = Network.Op.C2S_INACTIVE_PET;
                            break;

                        case 0xA5:
                            opCode = Network.Op.C2S_PET_BUY;
                            break;

                        case 0xA6:
                            opCode = Network.Op.C2S_PET_SELL;
                            break;

                        case 0xA7:
                            opCode = Network.Op.C2S_FEED_PET;
                            break;

                        case 0xA8:
                            opCode = Network.Op.C2S_REVIVE_PET;
                            break;

                        case 0xB0:
                            opCode = Network.Op.C2S_SHUE_COMBINATION;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x12:
                    switch (this._buffer[10])
                    {
                        case 0x00:
                            opCode = Network.Op.C2S_ASK_MOVE;
                            break;

                        case 0x02:
                            opCode = Network.Op.C2S_PC_MOVE;
                            break;

                        case 0x05:
                            opCode = Network.Op.C2S_ASK_HS_MOVE;
                            break;

                        case 0x08:
                            opCode = Network.Op.C2S_HS_MOVE;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x13:
                    switch (this._buffer[10])
                    {
                        case 0x07:
                            opCode = Network.Op.C2S_OBJECT_NPC;
                            break;

                        case 0x08:
                            opCode = Network.Op.C2S_ASK_NPC_FAVOR;
                            break;

                        case 0x09:
                            opCode = Network.Op.C2S_NPC_FAVOR_UP;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x14:
                    switch (this._buffer[10])
                    {
                        case 0x00:
                            opCode = Network.Op.C2S_ASK_ATTACK;
                            break;

                        case 0x51:
                            opCode = Network.Op.C2S_LEARN_SKILL;
                            break;

                        case 0x53:
                            opCode = Network.Op.C2S_ASK_SKILL;
                            break;

                        case 0x61:
                            opCode = Network.Op.C2S_SKILL_SLOT_INFO;
                            break;

                        case 0x62:
                            opCode = Network.Op.C2S_ANS_RECALL;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x16:
                    switch (this._buffer[10])
                    {
                        case 0x02:
                            opCode = Network.Op.C2S_ALLOT_POINT;
                            break;

                        case 0x06:
                            opCode = Network.Op.C2S_ASK_HEAL;
                            break;

                        case 0x09:
                            opCode = Network.Op.C2S_RETRIEVE_POINT;
                            break;

                        case 0x0C:
                            opCode = Network.Op.C2S_RESTORE_EXP;
                            break;

                        case 0x11:
                            opCode = Network.Op.C2S_LEARN_PSKILL;
                            break;

                        case 0x13:
                            opCode = Network.Op.C2S_FORGET_ALL_PSKILL;
                            break;

                        case 0x51:
                            opCode = Network.Op.C2S_ASK_OPEN_STORAGE;
                            break;

                        case 0x52:
                            opCode = Network.Op.C2S_ASK_INVEN2STORAGE;
                            break;

                        case 0x53:
                            opCode = Network.Op.C2S_ASK_STORAGE2INVEN;
                            break;

                        case 0x54:
                            opCode = Network.Op.C2S_ASK_DEPOSITE_MONEY;
                            break;

                        case 0x55:
                            opCode = Network.Op.C2S_ASK_WITHDRAW_MONEY;
                            break;

                        case 0x56:
                            opCode = Network.Op.C2S_ASK_CLOSE_STORAGE;
                            break;

                        case 0x57:
                            opCode = Network.Op.C2S_ASK_MOVE_ITEMINSTORAGE;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x17:
                    switch (this._buffer[10])
                    {
                        case 0x02:
                            opCode = Network.Op.C2S_PICKUP_ITEM;
                            break;

                        case 0x04:
                            opCode = Network.Op.C2S_DROP_ITEM;
                            break;

                        case 0x06:
                            opCode = Network.Op.C2S_MOVE_ITEM;
                            break;

                        case 0x08:
                            opCode = Network.Op.C2S_WEAR_ITEM;
                            break;

                        case 0x11:
                            opCode = Network.Op.C2S_STRIP_ITEM;
                            break;

                        case 0x14:
                            opCode = Network.Op.C2S_BUY_ITEM;
                            break;

                        case 0x16:
                            opCode = Network.Op.C2S_SELL_ITEM;
                            break;

                        case 0x18:
                            opCode = Network.Op.C2S_GIVE_ITEM;
                            break;

                        case 0x21:
                            opCode = Network.Op.C2S_USE_POTION;
                            break;

                        case 0x23:
                            opCode = Network.Op.C2S_ASK_DEAL;
                            break;

                        case 0x25:
                            opCode = Network.Op.C2S_ANS_DEAL;
                            break;

                        case 0x27:
                            opCode = Network.Op.C2S_PUTIN_ITEM;
                            break;

                        case 0x29:
                            opCode = Network.Op.C2S_PUTOUT_ITEM;
                            break;

                        case 0x31:
                            opCode = Network.Op.C2S_DECIDE_DEAL;
                            break;

                        case 0x33:
                            opCode = Network.Op.C2S_CONFIRM_DEAL;
                            break;

                        case 0x36:
                            opCode = Network.Op.C2S_USE_ITEM;
                            break;

                        case 0x42:
                            opCode = Network.Op.C2S_CONFIRM_ITEM;
                            break;

                        case 0x44:
                            opCode = Network.Op.C2S_REMODEL_ITEM;
                            break;

                        case 0x48:
                            opCode = Network.Op.C2S_USESCROLL;
                            break;

                        case 0x50:
                            opCode = Network.Op.C2S_PUTIN_PET;
                            break;

                        case 0x51:
                            opCode = Network.Op.C2S_PUTOUT_PET;
                            break;

                        case 0x53:
                            opCode = Network.Op.C2S_ITEM_COMBINATION;
                            break;

                        case 0x54:
                            opCode = Network.Op.C2S_LOTTO_PURCHASE;
                            break;

                        case 0x55:
                            opCode = Network.Op.C2S_LOTTO_QUERY_PRIZE;
                            break;

                        case 0x56:
                            opCode = Network.Op.C2S_LOTTO_QUERY_HISTORY;
                            break;

                        case 0x57:
                            opCode = Network.Op.C2S_LOTTO_SALE;
                            break;

                        case 0x60:
                            opCode = Network.Op.C2S_TAKEITEM_IN_BOX;
                            break;

                        case 0x61:
                            opCode = Network.Op.C2S_TAKEITEM_OUT_BOX;
                            break;

                        case 0x67:
                            opCode = Network.Op.C2S_USE_POTION_EX;
                            break;

                        case 0x70:
                            opCode = Network.Op.C2S_OPEN_MARKET;
                            break;

                        case 0x71:
                            opCode = Network.Op.C2S_CLOSE_MARKET;
                            break;

                        case 0x73:
                            opCode = Network.Op.C2S_ENTER_MARKET;
                            break;

                        case 0x75:
                            opCode = Network.Op.C2S_BUYITEM_MARKET;
                            break;

                        case 0x76:
                            opCode = Network.Op.C2S_LEAVE_MARKET;
                            break;

                        case 0x77:
                            opCode = Network.Op.C2S_MODIFY_MARKET;
                            break;

                        case 0x80:
                            opCode = Network.Op.C2S_ASK_ITEM_SERIAL;
                            break;

                        case 0x81:
                            opCode = Network.Op.C2S_SOCKET_ITEM;
                            break;

                        case 0x85:
                            opCode = Network.Op.C2S_BUY_BATTLEFIELD_ITEM;
                            break;

                        case 0x90:
                            opCode = Network.Op.C2S_BUY_CASH_ITEM;
                            break;

                        case 0x91:
                            opCode = Network.Op.C2S_CASH_INFO;
                            break;

                        case 0xA9:
                            opCode = Network.Op.C2S_DERBY_INDEX_QUERY;
                            break;

                        case 0xAA:
                            opCode = Network.Op.C2S_DERBY_MONSTER_QUERY;
                            break;

                        case 0xAB:
                            opCode = Network.Op.C2S_DERBY_RATIO_QUERY;
                            break;

                        case 0xAC:
                            opCode = Network.Op.C2S_DERBY_PURCHASE;
                            break;

                        case 0xAE:
                            opCode = Network.Op.C2S_DERBY_RESULT_QUERY;
                            break;

                        case 0xAF:
                            opCode = Network.Op.C2S_DERBY_HISTORY_QUERY;
                            break;

                        case 0xB0:
                            opCode = Network.Op.C2S_DERBY_EXCHANGE;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x18:
                    switch (this._buffer[10])
                    {
                        case 0x00:
                            opCode = Network.Op.C2S_SAY;
                            break;

                        case 0x01:
                            opCode = Network.Op.C2S_GESTURE;
                            break;

                        case 0x03:
                            opCode = Network.Op.C2S_CHAT_WINDOW_OPT;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x19:
                    switch (this._buffer[10])
                    {
                        case 0x00:
                            opCode = Network.Op.C2S_OPTION;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x21:
                    switch (this._buffer[10])
                    {
                        case 0x10:
                            opCode = Network.Op.C2S_PARTY_QUEST;
                            break;

                        case 0x20:
                            opCode = Network.Op.C2S_QUESTEX_DIALOGUE_REQ;
                            break;

                        case 0x22:
                            opCode = Network.Op.C2S_QUESTEX_DIALOGUE_ANS;
                            break;

                        case 0x26:
                            opCode = Network.Op.C2S_QUESTEX_CANCEL;
                            break;

                        case 0x28:
                            opCode = Network.Op.C2S_QUESTEX_LIST;
                            break;

                        case 0x40:
                            opCode = Network.Op.C2S_SQUEST_START;
                            break;

                        case 0x44:
                            opCode = Network.Op.C2S_SQUEST_STEP_END;
                            break;

                        case 0x45:
                            opCode = Network.Op.C2S_SQUEST_HISTORY;
                            break;

                        case 0x49:
                            opCode = Network.Op.C2S_SQUEST_MINIGAME_MOVE;
                            break;

                        case 0x4A:
                            opCode = Network.Op.C2S_SQUEST_WALL_QUIZ;
                            break;

                        case 0x4C:
                            opCode = Network.Op.C2S_SQUEST_WALL_OK;
                            break;

                        case 0x4E:
                            opCode = Network.Op.C2S_SQUEST_A3_QUIZ_SELECT;
                            break;

                        case 0x4F:
                            opCode = Network.Op.C2S_SQUEST_A3_QUIZ;
                            break;

                        case 0x51:
                            opCode = Network.Op.C2S_SQUEST_A3_QUIZ_OK;
                            break;

                        case 0x52:
                            opCode = Network.Op.C2S_SQUEST_END_OK;
                            break;

                        case 0x53:
                            opCode = Network.Op.C2S_SQUEST_222_NUM_QUIZ;
                            break;

                        case 0x55:
                            opCode = Network.Op.C2S_SQUEST_312_ITEM_CREATE;
                            break;

                        case 0x57:
                            opCode = Network.Op.C2S_SQUEST_HBOY_RUNE;
                            break;

                        case 0x59:
                            opCode = Network.Op.C2S_SQUEST_HBOY_HANOI;
                            break;

                        case 0x5E:
                            opCode = Network.Op.C2S_SQUEST_346_ITEM_COMBI;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x22:
                    switch (this._buffer[10])
                    {
                        case 0x00:
                            opCode = Network.Op.C2S_ASK_PARTY;
                            break;

                        case 0x02:
                            opCode = Network.Op.C2S_ANS_PARTY;
                            break;

                        case 0x05:
                            opCode = Network.Op.C2S_OUT_PARTY;
                            break;

                        case 0xA0:
                            opCode = Network.Op.C2S_ASK_APPRENTICE_IN;
                            break;

                        case 0xA1:
                            opCode = Network.Op.C2S_ANS_APPRENTICE_IN;
                            break;

                        case 0xA4:
                            opCode = Network.Op.C2S_ASK_APPRENTICE_OUT;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x23:
                    switch (this._buffer[10])
                    {
                        case 0x00:
                            opCode = Network.Op.C2S_CLAN;
                            break;

                        case 0x01:
                            opCode = Network.Op.C2S_JOIN_CLAN;
                            break;

                        case 0x02:
                            opCode = Network.Op.C2S_ANS_CLAN;
                            break;

                        case 0x03:
                            opCode = Network.Op.C2S_BOLT_CLAN;
                            break;

                        case 0x04:
                            opCode = Network.Op.C2S_REQ_CLAN_INFO;
                            break;

                        case 0x20:
                            opCode = Network.Op.C2Z_REGISTER_MARK;
                            break;

                        case 0x22:
                            opCode = Network.Op.C2S_TRANSFER_MARK;
                            break;

                        case 0x23:
                            opCode = Network.Op.C2S_ASK_MARK;
                            break;

                        case 0x31:
                            opCode = Network.Op.C2S_FRIEND_INFO;
                            break;

                        case 0x32:
                            opCode = Network.Op.C2S_FRIEND_STATE;
                            break;

                        case 0x33:
                            opCode = Network.Op.C2S_FRIEND_GROUP;
                            break;

                        case 0x34:
                            opCode = Network.Op.C2S_ASK_FRIEND;
                            break;

                        case 0x35:
                            opCode = Network.Op.C2S_ANS_FRIEND;
                            break;

                        case 0x40:
                            opCode = Network.Op.C2S_ASK_CLAN_BATTLE;
                            break;

                        case 0x41:
                            opCode = Network.Op.C2S_ANS_CLAN_BATTLE;
                            break;

                        case 0x42:
                            opCode = Network.Op.C2S_ASK_CLAN_BATTLE_END;
                            break;

                        case 0x43:
                            opCode = Network.Op.C2S_ANS_CLAN_BATTLE_END;
                            break;

                        case 0x45:
                            opCode = Network.Op.C2S_ASK_CLAN_BATTLE_SCORE;
                            break;

                        case 0x50:
                            opCode = Network.Op.C2S_LETTER_BASE_INFO;
                            break;

                        case 0x51:
                            opCode = Network.Op.C2S_LETTER_SIMPLE_INFO;
                            break;

                        case 0x53:
                            opCode = Network.Op.C2S_LETTER_DEL;
                            break;

                        case 0x54:
                            opCode = Network.Op.C2S_LETTER_SEND;
                            break;

                        case 0x56:
                            opCode = Network.Op.C2S_LETTER_KEEPING;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x24:
                    switch (this._buffer[10])
                    {
                        case 0x00:
                            opCode = Network.Op.C2S_CHANGE_NATION;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x25:
                    switch (this._buffer[10])
                    {
                        case 0x10:
                            opCode = Network.Op.C2S_CAO_MITIGATION;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x26:
                    switch (this._buffer[10])
                    {
                        case 0x00:
                            opCode = Network.Op.C2S_AGIT_INFO;
                            break;

                        case 0x01:
                            opCode = Network.Op.C2S_AUCTION_INFO;
                            break;

                        case 0x02:
                            opCode = Network.Op.C2S_AGIT_ENTER;
                            break;

                        case 0x03:
                            opCode = Network.Op.C2S_AGIT_PUTUP_AUCTION;
                            break;

                        case 0x04:
                            opCode = Network.Op.C2S_AGIT_BIDON;
                            break;

                        case 0x05:
                            opCode = Network.Op.C2S_AGIT_PAY_EXPENSE;
                            break;

                        case 0x06:
                            opCode = Network.Op.C2S_AGIT_CHANGE_NAME;
                            break;

                        case 0x07:
                            opCode = Network.Op.C2S_AGIT_REPAY_MONEY;
                            break;

                        case 0x08:
                            opCode = Network.Op.C2S_AGIT_OBTAIN_SALEMONEY;
                            break;

                        case 0x0A:
                            opCode = Network.Op.C2S_AGIT_MANAGE_INFO;
                            break;

                        case 0x0B:
                            opCode = Network.Op.C2S_AGIT_OPTION;
                            break;

                        case 0x0C:
                            opCode = Network.Op.C2S_AGIT_OPTION_INFO;
                            break;

                        case 0x0D:
                            opCode = Network.Op.C2S_AGIT_PC_BAN;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x27:
                    switch (this._buffer[10])
                    {
                        case 0x30:
                            opCode = Network.Op.C2S_CHRISTMAS_CARD;
                            break;

                        case 0x31:
                            opCode = Network.Op.C2S_SPEAK_CARD;
                            break;

                        case 0x40:
                            opCode = Network.Op.C2S_PROCESS_INFO;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x28:
                    switch (this._buffer[10])
                    {
                        case 0x95:
                            opCode = Network.Op.C2S_PREPARE_USER;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x35:
                    switch (this._buffer[10])
                    {
                        case 0x00:
                            opCode = Network.Op.C2S_ASK_WARP_Z2B;
                            break;

                        case 0x10:
                            opCode = Network.Op.C2S_ASK_WARP_B2Z;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x38:
                    switch (this._buffer[10])
                    {
                        case 0x11:
                            opCode = Network.Op.C2S_PREPARE_USER;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x39:
                    switch (this._buffer[10])
                    {
                        case 0x15:
                            opCode = Network.Op.C2S_ASK_SHOP_INFO;
                            break;

                        case 0x16:
                            opCode = Network.Op.C2S_ASK_GIVE_MY_TAX;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x40:
                    switch (this._buffer[10])
                    {
                        case 0x01:
                            opCode = Network.Op.C2S_TYR_UNIT_LIST;
                            break;

                        case 0x02:
                            opCode = Network.Op.C2S_TYR_UNIT_INFO;
                            break;

                        case 0x03:
                            opCode = Network.Op.C2S_TYR_ENTRY;
                            break;

                        case 0x04:
                            opCode = Network.Op.C2S_TYR_JOIN;
                            break;

                        case 0x80:
                            opCode = Network.Op.C2S_TYR_REWARD_INFO;
                            break;

                        case 0x81:
                            opCode = Network.Op.C2S_TYR_REWARD;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x41:
                    switch (this._buffer[10])
                    {
                        case 0x02:
                            opCode = Network.Op.C2S_TYR_UPGRADE;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x42:
                    switch (this._buffer[10])
                    {
                        case 0x03:
                            opCode = Network.Op.C2S_TYR_RTMM_END;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x50:
                    switch (this._buffer[10])
                    {
                        case 0x01:
                            opCode = Network.Op.C2S_HS_SEAL;
                            break;

                        case 0x02:
                            opCode = Network.Op.C2S_HS_RECALL;
                            break;

                        case 0x05:
                            opCode = Network.Op.C2S_HS_REVIVE;
                            break;

                        case 0x06:
                            opCode = Network.Op.C2S_HS_ASK_ATTACK;
                            break;

                        case 0x08:
                            opCode = Network.Op.C2S_HSSTONE_BUY;
                            break;

                        case 0x09:
                            opCode = Network.Op.C2S_HSSTONE_SELL;
                            break;

                        case 0x0A:
                            opCode = Network.Op.C2S_HS_LEARN_SKILL;
                            break;

                        case 0x0B:
                            opCode = Network.Op.C2S_HS_ALLOT_POINT;
                            break;

                        case 0x0C:
                            opCode = Network.Op.C2S_HS_RETRIEVE_POINT;
                            break;

                        case 0x0D:
                            opCode = Network.Op.C2S_HS_WEAR_ITEM;
                            break;

                        case 0x10:
                            opCode = Network.Op.C2S_HS_STRIP_ITEM;
                            break;

                        case 0x1B:
                            opCode = Network.Op.C2S_HS_OPTION;
                            break;

                        case 0x1C:
                            opCode = Network.Op.C2S_HS_HEAL;
                            break;

                        case 0x1E:
                            opCode = Network.Op.C2S_HS_SKILL_RESET;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0x90:
                    switch (this._buffer[10])
                    {
                        case 0x00:
                            opCode = Network.Op.C2S_ASK_MIGRATION;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0xA0:
                    switch (this._buffer[10])
                    {
                        case 0x01:
                            opCode = Network.Op.C2S_ASK_CREATE_PLAYER;
                            break;

                        case 0x02:
                            opCode = Network.Op.C2S_ASK_DELETE_PLAYER;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0xA3:
                    switch (this._buffer[10])
                    {
                        case 0x40:
                            opCode = Network.Op.C2S_LEAGUE;
                            break;

                        case 0x45:
                            opCode = Network.Op.C2S_REQ_LEAGUE_CLAN_INFO;
                            break;

                        case 0x47:
                            opCode = Network.Op.C2S_LEAGUE_ALLOW;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                case 0xC0:
                    switch (this._buffer[10])
                    {
                        case 0x00:
                            opCode = Network.Op.C2S_PAYINFO;
                            break;

                        default:
                            opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                            break;
                    }

                    break;

                default:

                    if (this._buffer.Length == 22)
                    {
                        opCode = Network.Op.C2S_PING;
                    }
                    else
                    {
                        opCode = Network.Op.C2S_UNKNOWN_PROTOCOL;
                    }

                    break;
            }

            return opCode;
        }
    }
}
