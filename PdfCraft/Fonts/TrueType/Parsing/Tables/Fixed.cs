using System;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables
{
    public struct Fixed
    {
        public ushort High { get; set; }
        public ushort Low { get; set; }

        private readonly string formatted;

        public Fixed(ushort[] version) : this()
        {
            this.High = version[0];
            this.Low = version[1];

            formatted = $"{High}.{Low}";
        }

        public bool Is(string value)
        {
            return string.Compare(formatted, value, StringComparison.Ordinal) == 0;
        }

        public override string ToString()
        {
            return formatted;
        }

        public double ToDouble()
        {
            return High + Low / 16384;
        }
    }
}