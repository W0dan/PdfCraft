using System;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Cmap
{
    public class CmapEncoding
    {
        public UInt16 PlatformID;
        public UInt16 PlatformSpecificID;
        public UInt32 Offset;

        public CmapEncodingFormat0 Format0 { get; set; }
        public CmapEncodingFormat4 Format4 { get; set; }
        public CmapEncodingFormat6 Format6 { get; set; }

        public override string ToString()
        {
            var platform = "";
            var platformSpecific = "";

            switch (PlatformID)
            {
                case 0:
                    platform = "Unicode";
                    platformSpecific = UnicodePlatformSpecific();
                    break;
                case 1:
                    platform = "Macintosh";
                    platformSpecific = "Not known";
                    break;
                case 3:
                    platform = "Windows";
                    platformSpecific = WindowsPlatformSpecific();
                    break;
                default:
                    platform = "Unknown PlatformID";
                    break;
            }

            return $"{PlatformID}, {PlatformSpecificID} : {platform}, {platformSpecific}";
        }

        private string WindowsPlatformSpecific()
        {
            switch (PlatformSpecificID)
            {
                case 0:
                    return "Symbol";
                case 1:
                    return "Unicode BMP-only (UCS-2)";
                case 2:
                    return "Shift-JIS";
                case 3:
                    return "PRC";
                case 4:
                    return "BigFive";
                case 5:
                    return "Johab";
                case 10:
                    return "Unicode UCS-4";
            }

            return "Unknown PlatformSpecificID";
        }

        private string UnicodePlatformSpecific()
        {
            switch (PlatformSpecificID)
            {
                case 0:
                    return "Default semantics";
                case 1:
                    return "Version 1.1 semantics";
                case 2:
                    return "ISO 10646 1993 semantics (deprecated)";
                case 3:
                    return "Unicode 2.0 or later semantics (BMP only)";
                case 4:
                    return "Unicode 2.0 or later semantics (non-BMP characters allowed)";
                case 5:
                    return "Unicode Variation Sequences";
                case 6:
                    return "Full Unicode coverage (used with type 13.0 cmaps by OpenType)";
            }

            return "Unknown PlatformSpecificID";
        }
    }
}