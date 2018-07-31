using PdfCraft.Fonts.TrueType.Parsing;

namespace PdfCraft.Fonts.TrueType
{
    public class TtfFontSubset
    {
        private readonly TtfData ttfData;

        private readonly string[] subsetTables =
        {
            "cmap", "cvt ", "fpgm", "glyf", "head",
            "hhea", "hmtx", "loca", "maxp", "prep"
        };

        public TtfFontSubset(TtfData ttfData)
        {
            this.ttfData = ttfData;
        }

        public byte[] GetBytes()
        {
            // read cvt
            // read loca
            // read fpgm
            // read glyf
            // read maxp
            // read prep

            return new byte[] { };
        }
    }
}