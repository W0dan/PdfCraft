using System.Collections.Generic;
using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Hmtx
{
    public static class HmtxReader
    {
        public static Hmtx Read(byte[] ttfBytes, TtfTableDirectoryEntry entry, ushort numOfLongHorMetrics)
        {
            var converter = new ConversionReader(ttfBytes, (int)entry.Offset);

            var hmtx = new Hmtx
            {
                RawBytes = new ConversionReader(ttfBytes, (int)entry.Offset).ReadBytes(entry.Length),

                LongHorMetrics = new List<LongHorMetric>()
            };

            for (var i = 0; i < numOfLongHorMetrics; i++)
            {
                hmtx.LongHorMetrics.Add(new LongHorMetric
                {
                    AdvanceWidth = converter.ReadUInt16(),
                    LeftSideBearing = converter.ReadInt16()
                });
            }

            return hmtx;
        }
    }
}