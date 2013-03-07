using PdfCraft.Containers;

namespace PdfCraft.Fonts
{
    internal class FontDescriptor : BasePdfObject
    {
        private readonly FontObject _font;
        private readonly int _flags;

        public FontDescriptor(int objectNumber, FontObject font)
            : base(objectNumber)
        {
            _font = font;

            if (font.Name == "Helvetica")
                _flags = 32;
            else if (font.Name == "Courier")
                _flags = 35;
        }

        public override IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory.CreateByteContainer();

                const string format = "<< /Type /FontDescriptor /FontName /{0} /Flags {1} /FontBBox [{2}] /ItalicAngle {3} /Ascent {4} /Descent {5} " +
                                      "/CapHeight {6} /StemV {7} >>";
                content.Append(string.Format(format, _font.Name, _flags, "-177 -269 1123 866", 0, 720, -270, 660, 105));

                SetContent(content);

                return base.Content;
            }
        }
    }
}