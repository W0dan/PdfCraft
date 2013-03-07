namespace PdfCraft.Fonts
{
    public class FontProperties
    {
        public FontProperties()
        {
            
        }

        public FontProperties(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; set; }

        public int Size { get; set; }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}