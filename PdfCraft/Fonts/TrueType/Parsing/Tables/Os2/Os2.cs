namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Os2
{
    public class Os2
    {
        public ushort Version { get; set; }
        public short XAvgCharWidth { get; set; }
        public ushort UsWeightClass { get; set; }
        public ushort UsWidthClass { get; set; }
        public short FsType { get; set; }
        public short YSubscriptXSize { get; set; }
        public short YSubscriptYSize { get; set; }
        public short YSubscriptXOffset { get; set; }
        public short YSubscriptYOffset { get; set; }
        public short YSuperscriptXSize { get; set; }
        public short YSuperscriptYSize { get; set; }
        public short YSuperscriptXOffset { get; set; }
        public short YSuperscriptYOffset { get; set; }
        public short YStrikeoutSize { get; set; }
        public short YStrikeoutPosition { get; set; }
        public short SFamilyClass { get; set; }
        public byte[] Panose { get; set; }
        public byte[] UlCharRange { get; set; }
        public string AchVendID { get; set; }
        public ushort FsSelection { get; set; }
        public ushort FsFirstCharIndex { get; set; }
        public ushort FsLastCharIndex { get; set; }
        public short STypoAscender { get; set; }
        public short STypoDescender { get; set; }
        public short STypoLineGap { get; set; }
        public ushort UsWinAscent { get; set; }
        public ushort UsWinDescent { get; set; }
        public uint UlCodePageRange1 { get; set; }
        public uint UlCodePageRange2 { get; set; }
        public short SCapHeight { get; set; }
    }
}