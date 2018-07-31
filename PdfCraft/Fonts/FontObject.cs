namespace PdfCraft.Fonts
{
    public abstract class FontObject : BasePdfObject
    {
        protected FontObject(int objectNumber, string fontName, string name)
            : base(objectNumber)
        {
            FontName = fontName;
            Name = name;
        }

        public string FontName { get; }
        public string Name { get; }

        public abstract string Map(string text);

        public abstract int GetWidth(char c, int size);

        public abstract byte[] Encode(string text);
    }
}