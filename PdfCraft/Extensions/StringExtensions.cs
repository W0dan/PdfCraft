using System;
using System.Linq;
using System.Text;

namespace PdfCraft.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToBytes(this string text)
        {
            if (text.Any(x => x > (char)127))
                throw new ArgumentOutOfRangeException(text, "only ASCII values are allowed");

            return Encoding.Default.GetBytes(text);
        }

        public static string ToHex(this string text)
        {
            var result = new StringBuilder(text.Length * 2);
            foreach (var c in text)
            {
                result.Append(Convert.ToByte(c).ToString("X2"));
            }
            return result.ToString();
        }
    }
}