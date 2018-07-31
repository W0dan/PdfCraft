using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Cvt
{
    public static class CvtReader
    {
        public static Cvt Read(byte[] ttfBytes, TtfTableDirectoryEntry entry)
        {
            var converter = new ConversionReader(ttfBytes, (int)entry.Offset);

            var cvtCount = entry.Length / 4;

            var cvt = new Cvt
            {
                RawBytes = new ConversionReader(ttfBytes, (int)entry.Offset).ReadBytes(entry.Length),
            };
            for (var i = 0; i < cvtCount; i++)
            {
                cvt.ControlValues.Add(converter.ReadFWord());
            }

            return cvt;
        }
    }
}