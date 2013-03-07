using PdfCraft.Containers;

namespace PdfCraft.Fonts
{
    public class FontObject : BasePdfObject
    {
        private readonly string _fontName;
        private readonly string _name;

        public FontObject(int objectNumber, string fontName, string name)
            : base(objectNumber)
        {
            _fontName = fontName;
            _name = name;
        }

        public string FontName
        {
            get { return _fontName; }
        }

        public string Name { get { return _name; } }

        public override IByteContainer Content
        {
            get
            {
                const string format = "<< /Type /Font /Subtype /Type1 /Name {0} /BaseFont /{1} /Encoding /WinAnsiEncoding >>";
                var content = ByteContainerFactory.CreateByteContainer(string.Format(format, _fontName, _name));

                SetContent(content);

                return base.Content;
            }
        }
    }
}