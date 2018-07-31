using System;
using System.Collections.Generic;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Post
{
    public class PostFormat2
    {
        public PostFormat2()
        {
            GlyphNameIndex = new List<ushort>();
            Names = new List<PascalString>();
        }

        public UInt16 NumberOfGlyphs;
        public List<UInt16> GlyphNameIndex;
        public List<PascalString> Names;
    }
}