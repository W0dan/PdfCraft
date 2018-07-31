using System;
using System.Linq;
using System.Text;
using PdfCraft.Fonts.TrueType.Parsing.Tables;

namespace PdfCraft.Fonts.TrueType.Parsing.Conversion
{
    public static class Converter
    {
        /// <summary>
        /// reads 4 bytes into an UInt32 at offset
        /// </summary>
        /// <param name="ttfBytes">an array of bytes</param>
        /// <param name="offset">the offset at which to start reading 4 bytes</param>
        /// <returns></returns>
        public static uint ReadUInt32(byte[] ttfBytes, long offset)
        {
            return ReadUInt32(GetRange(ttfBytes, offset, 4));
        }

        private static string ReadString(byte[] bytes, int length)
        {
            return Encoding.ASCII.GetString(bytes, 0, length);
        }

        public static string ReadString(byte[] ttfBytes, long offset, int length)
        {
            return ReadString(GetRange(ttfBytes, offset, length), length);
        }

        public static Int16 ReadInt16(byte[] ttfBytes, long offset)
        {
            return ReadInt16(GetRange(ttfBytes, offset, 2));
        }

        private static Int16 ReadInt16(byte[] bytes)
        {
            return BitConverter.ToInt16(bytes.Reverse().ToArray(), 0);
        }

        public static UInt16 ReadUInt16(byte[] ttfBytes, long offset)
        {
            return ReadUInt16(GetRange(ttfBytes, offset, 2));
        }

        private static UInt16 ReadUInt16(byte[] bytes)
        {
            return BitConverter.ToUInt16(bytes.Reverse().ToArray(), 0);
        }

        private static UInt32 ReadUInt32(byte[] bytes)
        {
            return BitConverter.ToUInt32(bytes.Reverse().ToArray(), 0);
        }

        public static Int16 ReadFWord(byte[] ttfBytes, int offset)
        {
            return ReadFWord(GetRange(ttfBytes, offset, 2));
        }

        private static Int16 ReadFWord(byte[] bytes)
        {
            return BitConverter.ToInt16(bytes.Reverse().ToArray(), 0);
        }

        public static UInt16 ReadUFWord(byte[] ttfBytes, int offset)
        {
            return ReadUFWord(GetRange(ttfBytes, offset, 2));
        }

        private static UInt16 ReadUFWord(byte[] bytes)
        {
            return BitConverter.ToUInt16(bytes.Reverse().ToArray(), 0);
        }

        public static long ReadInt64(byte[] bytes, long offset)
        {
            return BitConverter.ToInt64(bytes.Reverse().ToArray(), 0);
        }

        public static Fixed ReadFixed(byte[] ttfBytes, long offset)
        {
            return ReadFixed(GetRange(ttfBytes, offset, 4));
        }

        public static byte ReadByte(byte[] ttfBytes, long offset)
        {
            return ttfBytes[offset];
        }

        public static byte[] ReadBytes(byte[] ttfBytes, long offset, long length)
        {
            return GetRange(ttfBytes, offset, length);
        }

        private static Fixed ReadFixed(byte[] bytes)
        {
            return new Fixed( new[]
            {
                BitConverter.ToUInt16(bytes.Reverse().ToArray(), 2),
                BitConverter.ToUInt16(bytes.Reverse().ToArray(), 0),
            });
        }

        private static byte[] GetRange(byte[] bytes, long offset, long length)
        {
            var result = new byte[length];

            for (var i = 0; i < length; i++)
            {
                result[i] = bytes[offset + i];
            }

            return result;
        }
    }
}