namespace PdfCraft.Fonts
{
    public class FontFactory
    {
        public static FontObject CreateFont(int objectNumber, int fontNumber, string name)
        {
            var fontName = "/F" + fontNumber;
            return new FontObject(objectNumber, fontName, name);
        }
    }
}