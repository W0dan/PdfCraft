namespace PdfCraft.Contents.Text
{
    /// <summary>
    /// defines a part of a line of text, with layout
    /// </summary>
    internal class TextboxLinePart
    {
        private readonly BreakPoint _textItem;
        private readonly TextStyle _style;

        public TextboxLinePart(BreakPoint textItem, TextStyle style, bool endOfLine)
        {
            _textItem = textItem;
            _style = style;
            EndOfLine = endOfLine;
        }

        public bool EndOfLine { get; set; }

        public BreakPoint TextItem
        {
            get { return _textItem; }
        }

        public TextStyle Style
        {
            get { return _style; }
        }
    }
}