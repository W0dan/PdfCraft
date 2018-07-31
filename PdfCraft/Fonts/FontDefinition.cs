namespace PdfCraft.Fonts
{
    internal class FontDefinition
    {
        public FontDefinition(FontObject font, int size)
        {
            Font = font;
            Size = size;
        }

        public FontObject Font { get; }
        public int Size { get; }

        public int GetWidth(char c)
        {
            return Font.GetWidth(c, Size);
        }

        public string Map(string text)
        {
            return Font.Map(text);
        }

        public byte[] Encode(string text)
        {
            return Font.Encode(text);
        }
    }
}