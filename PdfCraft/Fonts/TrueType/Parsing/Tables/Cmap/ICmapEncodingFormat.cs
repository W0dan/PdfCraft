using System;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Cmap
{
    public interface ICmapEncodingFormat
    {
        UInt16 Map(UInt16 mapFrom);
    }
}