using System.IO;

namespace PdfCraft.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ReadAllBytes(this Stream stream)
        {
            if (stream == null)
                return new byte[] { };

            stream.Position = 0;

            var length = (int)stream.Length;
            var result = new byte[length];
            stream.Read(result, 0, length);

            return result;
        }
    }
}