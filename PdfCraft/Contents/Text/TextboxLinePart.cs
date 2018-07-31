namespace PdfCraft.Contents.Text
{
    /// <summary>
    /// defines a part of a line of text, with layout
    /// </summary>
    internal class TextboxLinePart
    {
        public TextboxLinePart(BreakPoint textItem, TextStyle style, bool endOfLine)
        {
            TextItem = textItem;
            Style = style;
            EndOfLine = endOfLine;
        }

        public bool EndOfLine { get; set; }

        public BreakPoint TextItem { get; }

        public TextStyle Style { get; }

        /// <summary>
        /// for diagnostics !
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return TextItem.Text;
        }
    }
}