using System;
using System.Collections.Generic;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Cvt
{
    public class Cvt
    {
        public Cvt()
        {
            ControlValues = new List<int>();
        }

        public List<Int32> ControlValues;
        public byte[] RawBytes { get; set; }
    }
}