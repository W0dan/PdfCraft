using System;
using System.Linq;

namespace PdfCraft.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToBytes(this string text)
        {
            if (text.Any(x => x > (char)127))
                throw new ArgumentOutOfRangeException(text, "only ASCII values are allowed");

            return System.Text.Encoding.Default.GetBytes(text);
        }
    }
}