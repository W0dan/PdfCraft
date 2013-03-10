using System.Collections.Generic;
using PdfCraft.API;

namespace PdfCraft.Contents.Text
{
    /// <summary>
    /// defines a line of text
    /// </summary>
    internal class TextboxLineBuffer
    {
        private readonly TextAlignment _currentAlignment;
        private readonly List<TextboxLinePart> _parts = new List<TextboxLinePart>();

        public TextboxLineBuffer(TextAlignment currentAlignment)
        {
            _currentAlignment = currentAlignment;
        }

        public void AddPart(TextboxLinePart part)
        {
            _parts.Add(part);
        }

        public List<TextboxLinePart> Parts { get { return _parts; } }

        public TextAlignment CurrentAlignment
        {
            get { return _currentAlignment; }
        }

        public bool Linefeed { get; set; }
    }
}