namespace PdfCraft.Fonts
{
    public class FontProperties
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}