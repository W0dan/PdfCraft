namespace PdfCraft.Extensions
{
    public static class ByteArrayExtension
    {
        public static byte[] Pad4(this byte[] bytes)
        {
            var newArrayLength = bytes.Length + 0b0000_0000_0000_0011 & ~0b0000_0000_0000_0011;
            var buffer = new byte[newArrayLength];

            bytes.CopyTo(buffer, 0);

            return buffer;
        }
    }
}