using System;
using System.Collections.Generic;
using System.Text;
using PdfCraft.Extensions;

namespace PdfCraft.Containers
{
    public class ByteArrayByteContainer : IByteContainer
    {
        private const int InitialSize = 100;
        private const int IncrementSize = 100;
        private long allocatedSize;
        private byte[] data;
        private readonly bool locked;
        private int usedLength;

        public ByteArrayByteContainer(long length)
        {
            data = new byte[length];
            allocatedSize = length;

            locked = true;
        }

        public ByteArrayByteContainer()
        {
            data = new byte[InitialSize];
            allocatedSize = InitialSize;
        }

        public ByteArrayByteContainer(string text)
            : this(text.ToBytes())
        {
        }

        public ByteArrayByteContainer(byte[] bytes)
        {
            Allocate(bytes.Length);
            AppendData(bytes);

            usedLength = bytes.Length;
        }

        private void Allocate(int length)
        {
            var numberOfIncrements = length / IncrementSize;

            allocatedSize = (numberOfIncrements + 1) * IncrementSize;
            data = new byte[allocatedSize];
        }

        private void AppendData(IList<byte> bytes)
        {
            for (var i = 0; i < bytes.Count; i++)
                data[i + usedLength] = bytes[i];

            usedLength += bytes.Count;
        }

        public void Append(byte[] bytes)
        {
            if (allocatedSize < usedLength + bytes.Length)
                ReAllocate(bytes.Length);

            AppendData(bytes);
        }

        public void Append(char value)
        {
            Append(new[] { (byte)value });
        }

        public void Append(byte value)
        {
            Append(new[] { value });
        }

        public void AppendUInt32(UInt32 value)
        {
            if (allocatedSize < usedLength + 4)
                ReAllocate(4);

            var bytes = new[]
            {
                (byte) (value >> 24),
                (byte) ((value >> 16) & 0x000000ff),
                (byte) ((value >> 8) & 0x000000ff),
                (byte) (value & 0x000000ff),
            };

            AppendData(bytes);
        }

        public void AppendUInt16(UInt32 value)
        {
            if (value > UInt16.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(value), "Too large for a 16bit integer");

            if (allocatedSize < usedLength + 2)
                ReAllocate(2);

            var bytes = new[]
            {
                (byte) (value >> 8),
                (byte) (value & 0x000000ff),
            };

            AppendData(bytes);
        }

        private void ReAllocate(int addedLength)
        {
            if (locked)
            {
                throw new Exception("The size of this byte container can not be altered.");
            }

            var numberOfIncrements = addedLength / IncrementSize;

            var tmp = data;
            var neededSize = tmp.Length + ((numberOfIncrements + 1) * IncrementSize);
            if (neededSize > allocatedSize * 2)
                allocatedSize = neededSize;
            else
                allocatedSize *= 2;

            data = new byte[allocatedSize];
            for (var i = 0; i < usedLength; i++)
                data[i] = tmp[i];
        }

        public void Append(string text)
        {
            Append(text.ToBytes());
        }

        public void Append(StringBuilder text)
        {
            Append(text.ToString());
        }

        public void Append(IByteContainer bytes)
        {
            Append(bytes.GetBytes());
        }

        public override string ToString()
        {
            return Encoding.Default.GetString(GetBytes());
        }

        public byte[] GetBytes()
        {
            if (locked)
            {
                return data;
            }

            var tmp = new byte[usedLength];
            for (var i = 0; i < tmp.Length; i++)
            {
                tmp[i] = data[i];
            }
            return tmp;
        }

        public int Length
        {
            get
            {
                if (locked)
                {
                    return (int)allocatedSize;
                }
                return usedLength;
            }
            private set => usedLength = value;
        }

        public string ToHexString()
        {
            return ToString().ToHex();
        }

        public UInt32 CalculateChecksum()
        {
            var arrLength = data.Length / 4;
            int[] checksumArray = { 0, 0, 0, 0 };
            for (var i = 0; i < arrLength; i = i + 4)
            {
                checksumArray[3] += data[i] & 0xff;
                checksumArray[2] += data[i + 1] & 0xff;
                checksumArray[1] += data[i + 2] & 0xff;
                checksumArray[0] += data[i + 3] & 0xff;
            }
            return (UInt32)(checksumArray[0] + (checksumArray[1] << 8) + (checksumArray[2] << 16) + (checksumArray[3] << 24));
        }
    }
}