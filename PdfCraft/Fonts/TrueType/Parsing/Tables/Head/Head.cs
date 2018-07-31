using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Head
{
    public class Head
    {
        public Fixed Version { get; set; }
        public Fixed FontRevision { get; set; }
        public uint CheckSumAdjustment { get; set; }
        public uint MagicNumber { get; set; }
        public ushort Flags { get; set; }
        public ushort UnitsPerEm { get; set; }
        public LongDateTime Created { get; set; }
        public LongDateTime Modified { get; set; }
        public short XMin { get; set; }
        public short YMin { get; set; }
        public short XMax { get; set; }
        public short YMax { get; set; }
        public ushort MacStyle { get; set; }
        public ushort LowestRecPPEM { get; set; }
        public short FontDirectionHint { get; set; }
        public short IndexToLocFormat { get; set; }
        public short GlyphDataFormat { get; set; }
        public byte[] RawBytes { get; set; }
    }
}