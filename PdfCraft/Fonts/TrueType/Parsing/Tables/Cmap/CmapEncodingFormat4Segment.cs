using System;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Cmap
{
    public class CmapEncodingFormat4Segment
    {
        public UInt16 StartCode;
        public UInt16 EndCode;
        public Int16 IdDelta;
        public UInt16 IdRangeOffset;

        public UInt16? Map(UInt16 mapFrom)
        {
            if (mapFrom >= StartCode && mapFrom <= EndCode)
            {
                return (UInt16)(mapFrom + IdDelta);
            }

            return null;
        }
    }
}