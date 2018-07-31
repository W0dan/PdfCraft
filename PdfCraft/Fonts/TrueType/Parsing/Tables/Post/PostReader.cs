using System;
using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Post
{
    public static class PostReader
    {
        public static Post Read(byte[] ttfBytes, TtfTableDirectoryEntry entry)
        {
            var converter = new ConversionReader(ttfBytes, (int)entry.Offset);
            var post = new Post
            {
                Format = converter.ReadFixed(),
                ItalicAngle = converter.ReadFixed(),
                UnderlinePosition = converter.ReadFWord(),
                UnderlineThickness = converter.ReadFWord(),
                IsFixedPitch = converter.ReadUInt32(),
                MinMemType42 = converter.ReadUInt32(),
                MaxMemType42 = converter.ReadUInt32(),
                MinMemType1 = converter.ReadUInt32(),
                MaxMemType1 = converter.ReadUInt32()
            };

            if (post.Format.Is("1.0"))
            {
                // nothing special to add, Format1 does not require a subtable
            }
            else if (post.Format.Is("2.0"))
            {
                var format2 = new PostFormat2 { NumberOfGlyphs = converter.ReadUInt16() };

                var maxIndex = 0;
                for (var i = 0; i < format2.NumberOfGlyphs; i++)
                {
                    var index = converter.ReadUInt16();
                    format2.GlyphNameIndex.Add(index);

                    if (index < 258) continue; // only in 'Names' when index >= 258 , otherwise default
                    
                    index -= 258;
                    if (index > maxIndex) maxIndex = index;
                }
                for (var i = 0; i < maxIndex; i++)
                {
                    format2.Names.Add(converter.ReadPascalString());
                }

                post.Format2 = format2;
            }
            else if (post.Format.Is("2.5"))
            {
                throw new NotSupportedException("Font with post format 2.5 is not supported because it is deprecated as of februari 2000.");
            }
            else if (post.Format.Is("3.0"))
            {
                throw new NotSupportedException("Font with post format 3 is not supported because Apple recommends against its use.");
            } 
            else if (post.Format.Is("4.0"))
            {
                throw new NotSupportedException("Font with post format 4 is not supported because as a rule, format 4 'post'-tables are no longer necessary and should be avoided.");
            }

            return post;
        }
    }
}