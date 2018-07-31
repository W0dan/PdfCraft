using System;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables
{
    public struct TtfTableDirectoryEntry
    {
        public string Tag;
        public UInt32 CheckSum;
        public UInt32 Offset;
        public UInt32 Length;
    }
}