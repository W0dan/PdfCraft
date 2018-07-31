using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Os2
{
    public static class Os2Reader
    {
        public static Os2 Read(byte[] ttfBytes, TtfTableDirectoryEntry entry, int unitsPerEm)
        {
            var converter = new ConversionReader(ttfBytes, (int)entry.Offset);

            var os2 = new Os2
            {
                Version = converter.ReadUInt16(),
                XAvgCharWidth = converter.ReadInt16(),
                UsWeightClass = converter.ReadUInt16(),
                UsWidthClass = converter.ReadUInt16(),
                FsType = converter.ReadInt16(),
                YSubscriptXSize = converter.ReadInt16(),
                YSubscriptYSize = converter.ReadInt16(),
                YSubscriptXOffset = converter.ReadInt16(),
                YSubscriptYOffset = converter.ReadInt16(),
                YSuperscriptXSize = converter.ReadInt16(),
                YSuperscriptYSize = converter.ReadInt16(),
                YSuperscriptXOffset = converter.ReadInt16(),
                YSuperscriptYOffset = converter.ReadInt16(),
                YStrikeoutSize = converter.ReadInt16(),
                YStrikeoutPosition = converter.ReadInt16(),
                SFamilyClass = converter.ReadInt16(),
                Panose = converter.ReadBytes(10),
                UlCharRange = converter.ReadBytes(16),
                AchVendID = converter.ReadString(4),
                FsSelection = converter.ReadUInt16(),
                FsFirstCharIndex = converter.ReadUInt16(),
                FsLastCharIndex = converter.ReadUInt16(),
                STypoAscender = converter.ReadInt16(),
                STypoDescender = converter.ReadInt16(),
                STypoLineGap = converter.ReadInt16(),
                UsWinAscent = converter.ReadUInt16(),
                UsWinDescent = converter.ReadUInt16(),
                UlCodePageRange1 = 0,
                UlCodePageRange2 = 0
            };

            if (os2.Version > 0)
            {
                os2.UlCodePageRange1 = converter.ReadUInt16();
                os2.UlCodePageRange2 = converter.ReadUInt16();
            }
            if (os2.Version > 1)
            {
                converter.ReadBytes(2);
                os2.SCapHeight = converter.ReadInt16();
            }
            else
            {
                os2.SCapHeight = (short)(.7 * unitsPerEm);
            }

            return os2;
        }
    }
}