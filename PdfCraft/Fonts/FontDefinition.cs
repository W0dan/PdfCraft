namespace PdfCraft.Fonts
{
    internal class FontDefinition
    {
        private readonly FontObject _font;
        private readonly int _size;

        public FontDefinition(FontObject font, int size)
        {
            _font = font;
            _size = size;
        }

        public FontObject Font
        {
            get { return _font; }
        }

        public int Size
        {
            get { return _size; }
        }
    }
}