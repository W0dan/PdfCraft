using System.Collections.Generic;
using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Glyf
{
    public static class GlyfReader
    {
        public static Glyfs Read(byte[] ttfBytes, TtfTableDirectoryEntry entry, Loca.Loca loca)
        {
            var glyfs = new Glyfs();

            // don't read the last 'extra'-entry
            for (var i = 0; i < loca.Offsets.Count - 1; i++)
            {
                var locaOffset = loca.Offsets[i];
                var converter = new ConversionReader(ttfBytes, entry.Offset + locaOffset);

                var glyphLength = loca.Offsets[i + 1] - locaOffset;
                var glyf = new Glyf();

                if (glyphLength > 0)
                    glyf.GlyphData = converter.ReadBytes(glyphLength);

                glyfs.Glyphs.Add(glyf);
            }

            return glyfs;
        }
    }

    public class Glyfs
    {
        public Glyfs()
        {
            Glyphs = new List<Glyf>();
        }

        public List<Glyf> Glyphs;
    }

    public class Glyf
    {
        public byte[] GlyphData;
    }
}
