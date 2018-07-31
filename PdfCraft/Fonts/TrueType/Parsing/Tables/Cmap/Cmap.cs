using System;
using System.Collections.Generic;
using System.Linq;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Cmap
{
    public class Cmap
    {
        public Cmap()
        {
            Encodings = new List<CmapEncoding>();
        }

        public UInt16 Version;
        public UInt16 NumberSubtables;

        public readonly List<CmapEncoding> Encodings;
        public byte[] RawBytes { get; set; }

        public ICmapEncodingFormat GetCmapEncoding()
        {
            var encoding = Encodings.FirstOrDefault(e => e.PlatformID == 3 && e.PlatformSpecificID == 1) 
                ?? Encodings.FirstOrDefault(e => e.PlatformID == 1 && e.PlatformSpecificID == 0);

            if (encoding == null)
            {
                return null;
            }

            if (encoding.Format0 != null)
            {
                return encoding.Format0;
            }
            if (encoding.Format4 != null)
            {
                return encoding.Format4;
            }
            if (encoding.Format6 != null)
            {
                return encoding.Format6;
            }

            return null;
        }
    }
}