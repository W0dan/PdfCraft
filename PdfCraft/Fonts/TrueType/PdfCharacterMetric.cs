using System;

namespace PdfCraft.Fonts.TrueType
{
    public class PdfCharacterMetric : IComparable
    {
        public int CharacterMapping;
        public int CharacterWidth;

        public int CompareTo(object obj)
        {
            if (obj is PdfCharacterMetric other)
            {
                return CharacterMapping.CompareTo(other.CharacterMapping);
            }
            throw new ArgumentException($"The object is not a {nameof(PdfCharacterMetric)} !");
        }
    }
}