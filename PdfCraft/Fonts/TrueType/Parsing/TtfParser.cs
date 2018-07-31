using System.Linq;
using PdfCraft.Fonts.TrueType.Parsing.Conversion;
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
    public class TtfParser
    {
        public TtfData ParseTtf(byte[] ttfBytes)
        {
            var result = new TtfData
            {
                Header = new TtfHeader
                {
                    ScalerType = Converter.ReadUInt32(ttfBytes, 0),
                    NumTables = Converter.ReadUInt16(ttfBytes, 4),
                    SearchRange = Converter.ReadUInt16(ttfBytes, 6),
                    EntrySelector = Converter.ReadUInt16(ttfBytes, 8),
                    RangeShift = Converter.ReadUInt16(ttfBytes, 10)
                }
            };

            var currentOffset = 12;
            for (var i = 0; i < result.Header.NumTables; i++)
            {
                var tableDirectoryEntry = new TtfTableDirectoryEntry
                {
                    Tag = Converter.ReadString(ttfBytes, currentOffset, 4),
                    CheckSum = Converter.ReadUInt32(ttfBytes, currentOffset + 4),
                    Offset = Converter.ReadUInt32(ttfBytes, currentOffset + 8),
                    Length = Converter.ReadUInt32(ttfBytes, currentOffset + 12),
                };

                result.TtfTableDirectory.Add(tableDirectoryEntry);
                currentOffset += 16;
            }

            var hheaEntry = result.TtfTableDirectory.Single(e => e.Tag == "hhea");
            result.Hhea = HheaReader.Read(ttfBytes, hheaEntry);

            var hmtxEntry = result.TtfTableDirectory.Single(e => e.Tag == "hmtx");
            result.Hmtx = HmtxReader.Read(ttfBytes, hmtxEntry, result.Hhea.NumOfLongHorMetrics);

            var postEntry = result.TtfTableDirectory.Single(e => e.Tag == "post");
            result.Post = PostReader.Read(ttfBytes, postEntry);

            var cmapEntry = result.TtfTableDirectory.Single(e => e.Tag == "cmap");
            result.Cmap = CmapReader.Read(ttfBytes, cmapEntry);

            var nameEntry = result.TtfTableDirectory.Single(e => e.Tag == "name");
            result.Name = NameReader.Read(ttfBytes, nameEntry);

            var headEntry = result.TtfTableDirectory.Single(e => e.Tag == "head");
            result.Head = HeadReader.Read(ttfBytes, headEntry);

            var os2Entry = result.TtfTableDirectory.Single(e => e.Tag == "OS/2");
            result.Os2 = Os2Reader.Read(ttfBytes, os2Entry, result.Head.UnitsPerEm);



            var cvtEntry = result.TtfTableDirectory.Single(e => e.Tag == "cvt ");
            result.Cvt = CvtReader.Read(ttfBytes, cvtEntry);

            var maxpEntry = result.TtfTableDirectory.Single(e => e.Tag == "maxp");
            result.Maxp = MaxpReader.Read(ttfBytes, maxpEntry);

            var locaEntry = result.TtfTableDirectory.Single(e => e.Tag == "loca");
            result.Loca = LocaReader.Read(ttfBytes, locaEntry, result.Head.IndexToLocFormat, result.Maxp.NumGlyphs);

            var fpgmEntry = result.TtfTableDirectory.Single(e => e.Tag == "fpgm");
            result.Fpgm = FpgmReader.Read(ttfBytes, fpgmEntry);

            var prepEntry = result.TtfTableDirectory.Single(e => e.Tag == "prep");
            result.Prep = PrepReader.Read(ttfBytes, prepEntry);

            var glyfEntry = result.TtfTableDirectory.Single(e => e.Tag == "glyf");
            result.Glyf = GlyfReader.Read(ttfBytes, glyfEntry, result.Loca);

            return result;
        }
    }
}