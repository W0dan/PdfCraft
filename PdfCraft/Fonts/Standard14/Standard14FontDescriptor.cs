using PdfCraft.Containers;

namespace PdfCraft.Fonts.Standard14
{
    internal class Standard14FontDescriptor : BasePdfObject
    {
        private const int FixedPitch = 1;
        private const int Serif = 2;
        private const int Symbolic = 4;
        private const int Script = 8;
        private const int Nonsymbolic = 32;
        private const int Italic = 64;

        private readonly FontObject font;
        private readonly int flags;

        public Standard14FontDescriptor(int objectNumber, FontObject font)
            : base(objectNumber)
        {
            this.font = font;

            switch (font.Name)
            {
                case "Helvetica":
                    flags = Nonsymbolic;
                    break;
                case "Helvetica-Bold":
                    flags = Nonsymbolic;
                    break;
                case "Helvetica-Oblique":
                    flags = Nonsymbolic + Italic;
                    break;
                case "Helvetica-BoldOblique":
                    flags = Nonsymbolic + Italic;
                    break;
                case "Courier":
                    flags = FixedPitch + Serif + Nonsymbolic;
                    break;
                case "Courier-Bold":
                    flags = FixedPitch + Serif + Nonsymbolic;
                    break;
                case "Courier-Oblique":
                    flags = FixedPitch + Serif + Nonsymbolic + Italic;
                    break;
                case "Courier-BoldOblique":
                    flags = FixedPitch + Serif + Nonsymbolic + Italic;
                    break;
            }
        }

        public override IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory.CreateByteContainer();

                content.Append($"<< " +
                               $"/Type /FontDescriptor " +
                               $"/FontName /{font.Name} " +
                               $"/Flags {flags} " +
                               $"/FontBBox [-177 -269 1123 866] " +
                               $"/ItalicAngle {0} " +
                               $"/Ascent {720} " +
                               $"/Descent {-270} " +
                               $"/CapHeight {660} " +
                               $"/StemV {105} " +
                               $">>");

                SetContent(content);

                return base.Content;
            }
        }
    }
}