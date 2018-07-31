using System;
using System.Text;
using PdfCraft.Fonts.TrueType.Parsing.Conversion;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Name
{
    public class NameReader
    {
        public static Name Read(byte[] ttfBytes, TtfTableDirectoryEntry entry)
        {
            var offset = (int)entry.Offset;
            var converter = new ConversionReader(ttfBytes, offset);

            var name = new Name
            {
                Format = converter.ReadUInt16(),
                Count = converter.ReadUInt16(),
                StringOffset = converter.ReadUInt16()
            };

            for (var i = 0; i < name.Count; i++)
            {
                var record = new NameRecord
                {
                    PlatformID = converter.ReadUInt16(),
                    PlatformSpecificID = converter.ReadUInt16(),
                    LanguageID = converter.ReadUInt16(),
                    NameID = converter.ReadUInt16(),
                    Length = converter.ReadUInt16(),
                    Offset = converter.ReadUInt16()
                };

                var nameBytes = ReadName(ttfBytes, offset + name.StringOffset + record.Offset, record.Length);
                if (record.PlatformID == 1 && record.PlatformSpecificID == 0)
                {
                    record.Name = Encoding.UTF8.GetString(nameBytes);
                }
                else if (record.PlatformID == 3 && record.PlatformSpecificID == 1)
                {
                    //record.Name = Encoding.GetEncoding(1252).GetString(nameBytes);
                    //record.Name = Encoding.Unicode.GetString(nameBytes);
                    record.Name = Encoding.BigEndianUnicode.GetString(nameBytes);
                }

                name.NameRecords.Add(record);
            }

            return name;
        }

        private static byte[] ReadName(byte[] ttfBytes, int offset, ushort length)
        {
            var result = new Byte[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = ttfBytes[i + offset];
            }

            return result;
        }
    }
}