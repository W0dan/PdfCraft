using System;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Name
{
    public class NameRecord
    {
        public UInt16 PlatformID;
        public UInt16 PlatformSpecificID;
        public UInt16 LanguageID;
        public UInt16 NameID;
        public UInt16 Length;
        public UInt16 Offset;

        public string Name;
    }
}