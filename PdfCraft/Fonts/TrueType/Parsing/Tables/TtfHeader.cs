using System;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables
{
    public struct TtfHeader
    {
        public UInt32 ScalerType;
        public UInt16 NumTables;
        public UInt16 SearchRange;
        public UInt16 EntrySelector;
        public UInt16 RangeShift;
    }
}