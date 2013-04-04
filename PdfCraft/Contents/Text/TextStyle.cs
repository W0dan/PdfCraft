using System.Drawing;
using PdfCraft.API;
using PdfCraft.Fonts;

namespace PdfCraft.Contents.Text
{
    internal class TextStyle
    {
        public TextAlignment Alignment { get; set; }

        public Color Color { get; set; }

        public bool Superscript { get; set; }

        public FontDefinition Font { get; set; }

        public TextStyle Clone()
        {
            return new TextStyle
                {
                    Alignment = Alignment,
                    Color = Color,
                    Superscript = Superscript,
                    Font = Font
                };
        }
    }
}