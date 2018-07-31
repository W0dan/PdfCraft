using System.Collections.Generic;
using PdfCraft.Fonts.TrueType.Parsing.Tables;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Cmap;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Cvt;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Fpgm;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Glyf;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Head;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Hhea;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Hmtx;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Loca;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Maxp;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Name;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Os2;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Post;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Prep;

namespace PdfCraft.Fonts.TrueType.Parsing
{
    public class TtfData
    {
        public TtfData()
        {
            TtfTableDirectory = new List<TtfTableDirectoryEntry>();
        }

        public TtfHeader Header { get; set; }
        public List<TtfTableDirectoryEntry> TtfTableDirectory { get; }
        public Hhea Hhea { get; set; }
        public Hmtx Hmtx { get; set; }
        public Post Post { get; set; }
        public Cmap Cmap { get; set; }
        public Name Name { get; set; }
        public Head Head { get; set; }
        public Os2 Os2 { get; set; }

        // these tables are needed to be put binary into the PDF:
        public Cvt Cvt { get; set; }
        public Maxp Maxp { get; set; }
        public Loca Loca { get; set; }
        public Fpgm Fpgm { get; set; }
        public Prep Prep { get; set; }
        public Glyfs Glyf { get; set; }

        public int ConvertUnit(int value)
        {
            return value * 1000 / Head.UnitsPerEm;
        }

        public int ConvertUnit(Fixed value)
        {
            return (int)(value.ToDouble() * 1000 / Head.UnitsPerEm);
        }
    }
}