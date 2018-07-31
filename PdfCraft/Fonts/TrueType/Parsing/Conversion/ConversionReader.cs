using System;
using PdfCraft.Fonts.TrueType.Parsing.Tables;

namespace PdfCraft.Fonts.TrueType.Parsing.Conversion
{
    public class ConversionReader
    {
        private readonly byte[] ttfBytes;

        public ConversionReader(byte[] ttfBytes, long startOffset)
        {
            this.ttfBytes = ttfBytes;
            this.Offset = startOffset;
        }

        public long Offset { get; private set; }

        public byte ReadByte()
        {
            var result = Converter.ReadByte(ttfBytes, Offset);
            Offset += 1;
            return result;
        }

        public byte[] ReadBytes(long length)
        {
            var result = Converter.ReadBytes(ttfBytes, Offset, length);
            Offset += length;
            return result;
        }

        public uint ReadUInt32()
        {
            var result = Converter.ReadUInt32(this.ttfBytes, this.Offset);
            Offset += 4;
            return result;
        }

        public PascalString ReadPascalString()
        {
            var result = new PascalString { Length = ReadByte() };
            result.Value = ReadString(result.Length);

            return result;
        }

        public string ReadString(int length)
        {
            var result = Converter.ReadString(ttfBytes, Offset, length);
            Offset += length;
            return result;
        }

        public Int16 ReadInt16()
        {
            var result = Converter.ReadInt16(this.ttfBytes, this.Offset);
            Offset += 2;
            return result;
        }

        public UInt16 ReadUInt16()
        {
            var result = Converter.ReadUInt16(this.ttfBytes, this.Offset);
            Offset += 2;
            return result;
        }

        public Int16 ReadFWord()
        {
            var result = Converter.ReadInt16(this.ttfBytes, this.Offset);
            Offset += 2;
            return result;
        }

        public UInt16 ReadUFWord()
        {
            var result = Converter.ReadUInt16(this.ttfBytes, this.Offset);
            Offset += 2;
            return result;
        }

        public Fixed ReadFixed()
        {
            var result = Converter.ReadFixed(this.ttfBytes, this.Offset);
            Offset += 4;
            return result;
        }

        public LongDateTime ReadLongDateTime()
        {
            var result = Converter.ReadInt64(this.ttfBytes, this.Offset);
            Offset += 8;
            return new LongDateTime { Value = result };
        }
    }

    public struct LongDateTime
    {
        public Int64 Value;
    }
}