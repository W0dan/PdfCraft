using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Cmap
{
    public static class CmapReader
    {
        public static Cmap Read(byte[] ttfBytes, TtfTableDirectoryEntry entry)
        {
            var converter = new ConversionReader(ttfBytes, (int)entry.Offset);

            var cmap = new Cmap
            {
                RawBytes = new ConversionReader(ttfBytes, (int)entry.Offset).ReadBytes(entry.Length),

                Version = converter.ReadUInt16(),
                NumberSubtables = converter.ReadUInt16(),
            };

            // read encoding subtables
            for (var i = 0; i < cmap.NumberSubtables; i++)
            {
                cmap.Encodings.Add(ReadEncoding(ttfBytes, converter, (int)entry.Offset));
            }

            return cmap;
        }

        private static CmapEncoding ReadEncoding(byte[] ttfBytes, ConversionReader converter, int tableOffset)
        {
            var encoding = new CmapEncoding
            {
                PlatformID = converter.ReadUInt16(),
                PlatformSpecificID = converter.ReadUInt16(),
                Offset = converter.ReadUInt32()
            };

            var formatConverter = new ConversionReader(ttfBytes, (int)encoding.Offset + tableOffset);
            var format = formatConverter.ReadUInt16();
            switch (format)
            {
                case 0:
                    // format 0
                    encoding.Format0 = ReadFormat0(formatConverter);
                    break;
                case 4:
                    // format 4
                    encoding.Format4 = ReadFormat4(formatConverter);
                    break;
                case 6:
                    // format 6
                    encoding.Format6 = ReadFormat6(formatConverter);
                    break;
                case 12:
                    // format 12
                    break;
            }

            return encoding;
        }

        private static CmapEncodingFormat6 ReadFormat6(ConversionReader converter)
        {
            var format6 = new CmapEncodingFormat6
            {
                Format = 6,
                Length = converter.ReadUInt16(),
                Language = converter.ReadUInt16(),
                FirstCode = converter.ReadUInt16(),
                EntryCount = converter.ReadUInt16(),
            };

            for (var i = 0; i < format6.EntryCount; i++)
            {
                format6.GlyphIndexArray.Add(converter.ReadUInt16());
            }

            return format6;
        }

        private static CmapEncodingFormat4 ReadFormat4(ConversionReader converter)
        {
            var format4 = new CmapEncodingFormat4
            {
                Format = 4,
                Length = converter.ReadUInt16(),
                Language = converter.ReadUInt16(),
                SegCountX2 = converter.ReadUInt16(),
                SearchRange = converter.ReadUInt16(),
                EntrySelector = converter.ReadUInt16(),
                RangeShift = converter.ReadUInt16(),
            };

            var segCount = format4.SegCountX2 / 2;
            for (var i = 0; i < segCount; i++)
            {
                format4.Segments.Add(new CmapEncodingFormat4Segment
                {
                    EndCode = converter.ReadUInt16()
                });
            }
            format4.ReservedPad = converter.ReadUInt16();
            for (var i = 0; i < segCount; i++)
            {
                format4.Segments[i].StartCode = converter.ReadUInt16();
            }
            for (var i = 0; i < segCount; i++)
            {
                format4.Segments[i].IdDelta = converter.ReadInt16();
            }
            for (var i = 0; i < segCount; i++)
            {
                format4.Segments[i].IdRangeOffset = converter.ReadUInt16();
            }
            var glyphIndexArrayLength = format4.Length / 2 - 8 - segCount * 4; // kennelijk fout ?!?
            for (var i = 0; i < glyphIndexArrayLength; i++)
            {
                format4.GlyphIndexArray.Add(converter.ReadUInt16());
            }

            return format4;
        }

        private static CmapEncodingFormat0 ReadFormat0(ConversionReader converter)
        {
            var format0 = new CmapEncodingFormat0
            {
                Format = 0,
                Length = converter.ReadUInt16(),
                Language = converter.ReadUInt16(),
            };

            for (var i = 0; i < 256; i++)
            {
                format0.GlyphIndexArray.Add(converter.ReadByte());
            }

            return format0;
        }
    }
}