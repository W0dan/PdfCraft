using System;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Hhea
{
    public struct Hhea
    {
        public Fixed Version;

        public Int16 Ascent;
        public Int16 Descent;
        public Int16 LineGap;
        public UInt16 AdvanceWidthMax;
        public Int16 MinLeftSideBearing;
        public Int16 MinRightSideBearing;
        public Int16 XMaxExtent;
        public Int16 CaretSlopeRise;
        public Int16 CaretSlopeRun;
        public Int16 CaretOffset;
        public Int16 Reserved1;
        public Int16 Reserved2;
        public Int16 Reserved3;
        public Int16 Reserved4;
        public Int16 MetricDataFormat;
        public UInt16 NumOfLongHorMetrics;
        public byte[] RawBytes { get; set; }
    }
}