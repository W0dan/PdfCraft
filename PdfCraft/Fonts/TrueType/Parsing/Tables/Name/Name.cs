using System;
using System.Collections.Generic;
using System.Linq;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Name
{
    public class Name
    {
        public Name()
        {
            NameRecords = new List<NameRecord>();
        }

        public UInt16 Format;
        public UInt16 Count;
        public UInt16 StringOffset;
        public readonly List<NameRecord> NameRecords;

        public string FamilyName => SelectName(NameRecords.Where(x => x.NameID == 1));

        public string FullName => SelectName(NameRecords.Where(x => x.NameID == 4));

        public string FontName => SelectName(NameRecords.Where(x => x.NameID == 6));

        private static string SelectName(IEnumerable<NameRecord> names)
        {
            var nameList = names.ToList();
            if (!nameList.Any()) return "";

            var unicodeNames = nameList
                .Where(x => x.PlatformID == 0 || x.PlatformID == 3 || x.PlatformID == 2 && x.PlatformSpecificID == 1)
                .ToList();

            return unicodeNames.Any()
                ? unicodeNames.Select(x => x.Name).First()
                : nameList.Select(x => x.Name).First();
        }
    }
}