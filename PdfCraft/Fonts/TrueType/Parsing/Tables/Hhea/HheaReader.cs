using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Hhea
{
    public static class HheaReader
    {
        public static Hhea Read(byte[] ttfBytes, TtfTableDirectoryEntry entry)
        {
            var converter = new ConversionReader(ttfBytes, (int)entry.Offset);
            return new Hhea
            {
                RawBytes = new ConversionReader(ttfBytes, (int)entry.Offset).ReadBytes(entry.Length),

                Version = converter.ReadFixed(),
                Ascent = converter.ReadFWord(),
                Descent = converter.ReadFWord(),
                LineGap = converter.ReadFWord(),
                AdvanceWidthMax = converter.ReadUFWord(),
                MinLeftSideBearing = converter.ReadFWord(),
                MinRightSideBearing = converter.ReadFWord(),
                XMaxExtent = converter.ReadFWord(),
                CaretSlopeRise = converter.ReadInt16(),
                CaretSlopeRun = converter.ReadInt16(),
                CaretOffset = converter.ReadFWord(),
                Reserved1 = converter.ReadInt16(),
                Reserved2 = converter.ReadInt16(),
                Reserved3 = converter.ReadInt16(),
                Reserved4 = converter.ReadInt16(),
                MetricDataFormat = converter.ReadInt16(),
                NumOfLongHorMetrics = converter.ReadUInt16()
            };
        }
    }
}