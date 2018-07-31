using System;
using System.Collections.Generic;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Cmap
{
    public class CmapEncodingFormat6 : ICmapEncodingFormat
    {
        public CmapEncodingFormat6()
        {
            GlyphIndexArray = new List<ushort>();
        }

        public UInt16 Format;
        public UInt16 Length;
        public UInt16 Language;
        public UInt16 FirstCode;
        public UInt16 EntryCount;
        public List<UInt16> GlyphIndexArray;

        public ushort Map(ushort mapFrom)
        {
            if (mapFrom >= GlyphIndexArray.Count)
            {
                return 0;
            }

            return GlyphIndexArray[mapFrom];
        }
    }
}