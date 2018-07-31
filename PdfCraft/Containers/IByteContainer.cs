using System;
using System.Text;

namespace PdfCraft.Containers
{
    public interface IByteContainer
    {
        void Append(string text);
        void Append(byte[] bytes);
        void Append(char value);
        void Append(byte value);
        void Append(StringBuilder text);
        void Append(IByteContainer bytes);
        void AppendUInt32(UInt32 value);
        void AppendUInt16(UInt32 value);
        string ToString();
        byte[] GetBytes();
        int Length { get; }

        string ToHexString();
        UInt32 CalculateChecksum();
    }
}