using PdfCraft.Containers;

namespace PdfCraft.Fonts
{
    internal class FontDescriptor : BasePdfObject
    {
        private const int FixedPitch = 1;
        private const int Serif = 2;
        private const int Symbolic = 4;
        private const int Script = 8;
        private const int Nonsymbolic = 32;
        private const int Italic = 64;

        private readonly FontObject _font;
        private readonly int _flags;

        public FontDescriptor(int objectNumber, FontObject font)
            : base(objectNumber)
        {
            _font = font;

            switch (font.Name)
            {
                case "Helvetica":
                    _flags = Nonsymbolic;
                    break;
                case "Helvetica-Bold":
                    _flags = Nonsymbolic;
                    break;
                case "Helvetica-Oblique":
                    _flags = Nonsymbolic + Italic;
                    break;
                case "Helvetica-BoldOblique":
                    _flags = Nonsymbolic + Italic;
                    break;
                case "Courier":
                    _flags = FixedPitch + Serif + Nonsymbolic;
                    break;
                case "Courier-Bold":
                    _flags = FixedPitch + Serif + Nonsymbolic;
                    break;
                case "Courier-Oblique":
                    _flags = FixedPitch + Serif + Nonsymbolic + Italic;
                    break;
                case "Courier-BoldOblique":
                    _flags = FixedPitch + Serif + Nonsymbolic + Italic;
                    break;
            }
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