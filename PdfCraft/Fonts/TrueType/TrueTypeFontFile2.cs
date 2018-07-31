using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PdfCraft.Constants;
using PdfCraft.Containers;
using PdfCraft.Extensions;
using PdfCraft.Fonts.TrueType.Parsing.Tables.Loca;

namespace PdfCraft.Fonts.TrueType
{
    public class TrueTypeFontFile2 : BasePdfObject
    {
        private readonly TrueTypeFontObject parent;
        private readonly PdfFontDefinition fontDefinition;
        private static readonly short[] EntrySelectors = { 0, 0, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        private readonly string[] subsetTables = { "cmap", "cvt ", "fpgm", "glyf", "head", "hhea", "hmtx", "loca", "maxp", "prep" };

        public TrueTypeFontFile2(int objectNumber, TrueTypeFontObject parent, PdfFontDefinition fontDefinition) : base(objectNumber)
        {
            this.parent = parent;
            this.fontDefinition = fontDefinition;
        }

        public override IByteContainer Content
        {
            get
            {
                var streamData = ByteContainerFactory.CreateByteContainer();

                streamData.AppendUInt32(0x00010000);
                var numberOfTablesUsed = 10;
                var selector = EntrySelectors[numberOfTablesUsed];
                var searchRange = (1 << selector) * 16;
                streamData.AppendUInt16((UInt16)numberOfTablesUsed);
                streamData.AppendUInt16((UInt16)searchRange);
                streamData.AppendUInt16((UInt16)selector);
                streamData.AppendUInt16((UInt16)((numberOfTablesUsed - (1 << selector)) * 16));

                var glyfTables = GetGlyfTables();

                var td = GetTableDirectory(numberOfTablesUsed, glyfTables);

                var tableData = GetTableData(glyfTables);

                streamData.Append(td);
                streamData.Append(tableData);

                var content = ByteContainerFactory.CreateByteContainer();
                content.Append($"<< /Length {streamData.Length} >>{StringConstants.NewLine}" +
                               $"stream{StringConstants.NewLine}");
                content.Append(streamData);
                content.Append($"{StringConstants.NewLine}endstream");

                SetContent(content);

                return base.Content;
            }
        }

        private IByteContainer GetTableData(GlyfTables glyfTables)
        {
            var tableData = ByteContainerFactory.CreateByteContainer();

            foreach (var table in fontDefinition.TtfData.TtfTableDirectory)
            {
                switch (table.Tag)
                {
                    case "glyf":
                        tableData.Append(glyfTables.GlyfTable.GetBytes().Pad4());
                        break;
                    case "loca":
                        tableData.Append(glyfTables.LocaTable.GetBytes().Pad4());
                        break;
                    case "cmap":
                        tableData.Append(fontDefinition.TtfData.Cmap.RawBytes.Pad4());
                        break;
                    case "cvt ":
                        tableData.Append(ByteContainerFactory.CreateByteContainer(fontDefinition.TtfData.Cvt.RawBytes.Pad4()));
                        break;
                    case "fpgm":
                        tableData.Append(ByteContainerFactory.CreateByteContainer(fontDefinition.TtfData.Fpgm.RawBytes.Pad4()));
                        break;
                    case "head":
                        tableData.Append(ByteContainerFactory.CreateByteContainer(fontDefinition.TtfData.Head.RawBytes.Pad4()));
                        break;
                    case "hhea":
                        tableData.Append(ByteContainerFactory.CreateByteContainer(fontDefinition.TtfData.Hhea.RawBytes.Pad4()));
                        break;
                    case "hmtx":
                        tableData.Append(ByteContainerFactory.CreateByteContainer(fontDefinition.TtfData.Hmtx.RawBytes.Pad4()));
                        break;
                    case "maxp":
                        tableData.Append(ByteContainerFactory.CreateByteContainer(fontDefinition.TtfData.Maxp.RawBytes.Pad4()));
                        break;
                    case "prep":
                        tableData.Append(ByteContainerFactory.CreateByteContainer(fontDefinition.TtfData.Prep.RawBytes.Pad4()));
                        break;
                    default:
                        // do not add a table that is not in the shortlist
                        break;
                }
            }

            return tableData;
        }

        private GlyfTables GetGlyfTables()
        {
            var usedCharacters = parent.UsedCharacters;

            var usedGlyfs = new int[usedCharacters.Count];
            for (var i = 0; i < usedCharacters.Count; i++)
            {
                usedGlyfs[i] = usedCharacters[i].Metric.CharacterMapping;
            }
            Array.Sort(usedGlyfs);

            var originalLocaTable = fontDefinition.TtfData.Loca.Offsets;
            var locaTable = new uint[originalLocaTable.Count];
            var totalUsedGlyfSize = (UInt32)0;

            foreach (var usedCharacter in usedCharacters)
            {
                var mapping = usedCharacter.Metric.CharacterMapping;
                totalUsedGlyfSize += originalLocaTable[mapping + 1] - originalLocaTable[mapping];
            }

            var glyfTable = ByteContainerFactory.CreateByteContainer((totalUsedGlyfSize + 0b0000_0011) & ~0b0000_0011);

            var glyfOffset = (UInt32)0;
            var usedGlyfCounter = 0;
            for (int i = 0; i < locaTable.Length; i++)
            {
                locaTable[i] = glyfOffset;
                if (usedGlyfCounter < usedGlyfs.Length && usedGlyfs[usedGlyfCounter] == i)
                {
                    usedGlyfCounter++;

                    var originalGlyf = fontDefinition.TtfData.Glyf.Glyphs[i];

                    var start = originalLocaTable[i];
                    var len = originalLocaTable[i + 1] - start;

                    if (len <= 0) continue;

                    glyfTable.Append(originalGlyf.GlyphData);
                    glyfOffset += len;
                }
            }

            return new GlyfTables
            {
                GlyfTable = glyfTable,
                LocaTable = ConvertLocaTable(fontDefinition.TtfData.Loca, locaTable)
            };
        }

        private IByteContainer ConvertLocaTable(Loca originalLocaTable, uint[] locaTable)
        {
            switch (originalLocaTable.Format)
            {
                case LocaFormat.Short:
                    return GetLocaTableShortFormat(locaTable);
                case LocaFormat.Long:
                    return GetLocaTableLongFormat(locaTable);
                default:
                    throw new Exception("Invalid LocaTable format");
            }
        }

        private static IByteContainer GetLocaTableLongFormat(uint[] locaTable)
        {
            var locaContent = ByteContainerFactory.CreateByteContainer(locaTable.Length * 4);
            foreach (var locaEntry in locaTable)
                locaContent.AppendUInt16(locaEntry);

            return locaContent;
        }

        private static IByteContainer GetLocaTableShortFormat(uint[] locaTable)
        {
            var locaContent = ByteContainerFactory.CreateByteContainer(locaTable.Length * 2);
            foreach (var locaEntry in locaTable)
                locaContent.AppendUInt16(locaEntry / 2);

            return locaContent;
        }

        private IByteContainer GetTableDirectory(int numberOfTablesUsed, GlyfTables glyfTables)
        {
            var tableDirectory = ByteContainerFactory
                .CreateByteContainer();

            var offset = (UInt32)(16 * numberOfTablesUsed + 12);
            foreach (var table in fontDefinition.TtfData.TtfTableDirectory)
            {
                var length = table.Length;
                switch (table.Tag)
                {
                    case "glyf":
                        AppendDirectoryEntry(tableDirectory, table.Tag, glyfTables.GlyfTable.CalculateChecksum(), offset, (uint)glyfTables.GlyfTable.Length);
                        offset += (UInt32)((glyfTables.GlyfTable.Length + 0b0000_0011) & ~0b0000_0011);
                        break;
                    case "loca":
                        AppendDirectoryEntry(tableDirectory, table.Tag, glyfTables.LocaTable.CalculateChecksum(), offset, (uint)glyfTables.LocaTable.Length);
                        offset += (UInt32)((glyfTables.LocaTable.Length + 0b0000_0011) & ~0b0000_0011);
                        break;
                    case "cmap":
                    case "cvt ":
                    case "fpgm":
                    case "head":
                    case "hhea":
                    case "hmtx":
                    case "maxp":
                    case "prep":
                        AppendDirectoryEntry(tableDirectory, table.Tag, table.CheckSum, offset, length);

                        offset += (UInt32)((length + 0b0000_0011) & ~0b0000_0011);
                        break;
                    default:
                        // do not add a table that is not in the shortlist
                        break;
                }
            }

            return tableDirectory;
        }

        private static void AppendDirectoryEntry(IByteContainer tableDirectory, string tag, UInt32 checksum, UInt32 offset, UInt32 length)
        {
            tableDirectory.Append(tag);
            tableDirectory.AppendUInt32(checksum);
            tableDirectory.AppendUInt32(offset);
            tableDirectory.AppendUInt32(length);
        }
    }

    public class GlyfTables
    {
        public IByteContainer GlyfTable { get; set; }
        public IByteContainer LocaTable { get; set; }
    }
}