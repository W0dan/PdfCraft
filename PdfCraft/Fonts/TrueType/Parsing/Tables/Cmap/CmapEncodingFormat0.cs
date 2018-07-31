using System;
using System.Collections.Generic;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Cmap
{
    public class CmapEncodingFormat0 : ICmapEncodingFormat
    {
        public CmapEncodingFormat0()
        {
            GlyphIndexArray = new List<byte>();
        }

        public UInt16 Format;
        public UInt16 Length;
        public UInt16 Language;
        public List<byte> GlyphIndexArray;

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