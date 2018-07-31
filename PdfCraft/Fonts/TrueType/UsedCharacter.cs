using System;

namespace PdfCraft.Fonts.TrueType
{
    public class UsedCharacter : IComparable
    {
        public int Char { get; set; }
        public PdfCharacterMetric Metric { get; set; }

        public int CompareTo(object obj)
        {
            if (!(obj is UsedCharacter other))
                throw new InvalidCastException();

            return Char.CompareTo(other.Char);
        }
    }
}