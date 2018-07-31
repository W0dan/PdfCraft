using System;
using System.Collections.Generic;
using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Loca
{
    public static class LocaReader
    {
        public static Loca Read(byte[] ttfBytes, TtfTableDirectoryEntry entry, Int16 indexToLocFormat, UInt16 numGlyphs)
        {
            var converter = new ConversionReader(ttfBytes, (int)entry.Offset);

            var loca = new Loca();
            if (indexToLocFormat == 0) // short offsets
            {
                loca.Offsets = ReadShortOffsets(converter, numGlyphs);
                loca.Format = LocaFormat.Short;
            }
            else if (indexToLocFormat == 1) // long offsets
            {
                loca.Offsets = ReadLongOffsets(converter, numGlyphs);
                loca.Format = LocaFormat.Long;
            }
            else
            {
                throw new FormatException($"Loca table format not correct in Head table (indexToLocFormat -field), " +
                                          $"expected 0 or 1 but encountered {indexToLocFormat}.");
            }
            return loca;
        }

        /// <summary>
        /// Reads byte-offsets from Long format Loca-table
        /// </summary>
        /// <returns>byte-offsets</returns>
        private static List<uint> ReadLongOffsets(ConversionReader converter, ushort numGlyphs)
        {
            var result = new List<uint>();
            for (var i = 0; i <= numGlyphs; i++)
            {
                result.Add(converter.ReadUInt32());
            }
            return result;
        }

        /// <summary>
        /// Reads word-offsets from Short format Loca-table and converts them to byte-offsets
        /// </summary>
        /// <returns>byte-offsets</returns>
        private static List<uint> ReadShortOffsets(ConversionReader converter, ushort numGlyphs)
        {
            var result = new List<uint>();
            for (var i = 0; i <= numGlyphs; i++)
            {
                result.Add((UInt32)converter.ReadUInt16() * 2);
            }
            return result;
        }
    }
}