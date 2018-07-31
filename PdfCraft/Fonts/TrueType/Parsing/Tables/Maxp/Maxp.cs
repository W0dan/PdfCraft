namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Maxp
{
    public class Maxp
    {
        public Fixed Version { get; set; }
        public ushort NumGlyphs { get; set; }
        public byte[] RawBytes { get; set; }

        // omit other fields for now, don't need them probably anyway

    }
}