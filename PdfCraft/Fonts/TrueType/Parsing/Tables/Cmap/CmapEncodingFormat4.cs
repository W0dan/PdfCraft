using System;
using System.Collections.Generic;
using System.Linq;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Cmap
{
    public class CmapEncodingFormat4 : ICmapEncodingFormat
    {
        public CmapEncodingFormat4()
        {
            Segments = new List<CmapEncodingFormat4Segment>();
            GlyphIndexArray = new List<ushort>();
        }

        public UInt16 Format;
        public UInt16 Length;
        public UInt16 Language;
        public UInt16 SegCountX2;
        public UInt16 SearchRange;
        public UInt16 EntrySelector;
        public UInt16 RangeShift;
        public UInt16 ReservedPad;  // always 0
        public List<CmapEncodingFormat4Segment> Segments;
        public List<UInt16> GlyphIndexArray;

        public UInt16 Map(UInt16 mapFrom)
        {
            var segment = Segments.SingleOrDefault(s => s.StartCode <= mapFrom && s.EndCode >= mapFrom);

            return segment?.Map(mapFrom) ?? 0;
        }
    }
}