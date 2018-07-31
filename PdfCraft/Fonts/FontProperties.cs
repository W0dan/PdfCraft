namespace PdfCraft.Fonts
{
    public class FontProperties
    {
        public FontProperties() { }

        public FontProperties(string name, int size)
        {
            Name = name;
            Size = size;
            FontType = FontType.Standard14Font;
        }

        public FontProperties(string name, int size, byte[] fontBytes)
        {
            Name = name;
            Size = size;
            FontType = FontType.TrueType;
            Bytes = fontBytes;
        }

        public string Name { get; }
        public int Size { get; }
        public FontType FontType { get; }
        public byte[] Bytes { get; }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}