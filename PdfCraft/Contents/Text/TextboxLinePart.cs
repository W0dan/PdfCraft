using System.Drawing;
using PdfCraft.Fonts;

namespace PdfCraft.Contents.Text
{
    /// <summary>
    /// defines a part of a line of text, with layout
    /// </summary>
    internal class TextboxLinePart
    {
        private readonly BreakPoint _textItem;
        private readonly FontDefinition _font;
        private readonly Color _color;

        public TextboxLinePart(BreakPoint textItem, FontDefinition font, Color color, bool endOfLine)
        {
            _textItem = textItem;
            _font = font;
            _color = color;
            EndOfLine = endOfLine;
        }

        public bool EndOfLine { get; set; }

        public BreakPoint TextItem
        {
            get { return _textItem; }
        }

        public FontDefinition Font
        {
            get { return _font; }
        }

        public Color Color
        {
            get { return _color; }
        }
    }
}