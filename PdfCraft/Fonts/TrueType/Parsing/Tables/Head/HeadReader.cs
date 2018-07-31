using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Head
{
    public static class HeadReader
    {
        public static Head Read(byte[] ttfBytes, TtfTableDirectoryEntry entry)
        {
            var converter = new ConversionReader(ttfBytes, (int)entry.Offset);

            return new Head
            {
                RawBytes = new ConversionReader(ttfBytes, (int)entry.Offset).ReadBytes(entry.Length),

                Version = converter.ReadFixed(),
                FontRevision = converter.ReadFixed(),
                CheckSumAdjustment = converter.ReadUInt32(),
                MagicNumber = converter.ReadUInt32(),
                Flags = converter.ReadUInt16(),
                UnitsPerEm = converter.ReadUInt16(),
                Created = converter.ReadLongDateTime(),
                Modified = converter.ReadLongDateTime(),
                XMin = converter.ReadFWord(),
                YMin = converter.ReadFWord(),
                XMax = converter.ReadFWord(),
                YMax = converter.ReadFWord(),
                MacStyle = converter.ReadUInt16(),
                LowestRecPPEM = converter.ReadUInt16(),
                FontDirectionHint = converter.ReadInt16(),
                IndexToLocFormat = converter.ReadInt16(),
                GlyphDataFormat = converter.ReadInt16()
            };
        }
    }
}