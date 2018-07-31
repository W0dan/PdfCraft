using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Maxp
{
    public static class MaxpReader
    {
        public static Maxp Read(byte[] ttfBytes, TtfTableDirectoryEntry entry)
        {
            var converter = new ConversionReader(ttfBytes, (int)entry.Offset);

            var maxp = new Maxp
            {
                RawBytes = new ConversionReader(ttfBytes, (int)entry.Offset).ReadBytes(entry.Length),

                Version = converter.ReadFixed(),
                NumGlyphs = converter.ReadUInt16()
            };

            // omit other fields for now, don't need them probably anyway
            // they are dependent on the Version !!

            return maxp;
        }
    }
}