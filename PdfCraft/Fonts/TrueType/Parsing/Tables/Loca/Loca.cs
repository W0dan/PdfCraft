using System;
using System.Collections.Generic;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Loca
{
    public class Loca
    {
        /// <summary>
        /// contains byte-offsets
        /// </summary>
        public List<UInt32> Offsets;

        public LocaFormat Format { get; set; }
    }
}