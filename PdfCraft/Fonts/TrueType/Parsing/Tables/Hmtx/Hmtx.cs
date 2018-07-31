using System;
using System.Collections.Generic;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Hmtx
{
    public struct Hmtx
    {
        public List<LongHorMetric> LongHorMetrics;
        public List<Int16> LeftSideBearing;
        public byte[] RawBytes { get; set; }
    }
}