using System.Collections.Generic;
using PdfCraft.API;

namespace PdfCraft.Contents.Text
{
    /// <summary>
    /// defines a line of text
    /// </summary>
    internal class TextboxLineBuffer
    {
        private readonly List<TextboxLinePart> _parts = new List<TextboxLinePart>();

        public TextboxLineBuffer(TextAlignment currentAlignment)
        {
            CurrentAlignment = currentAlignment;
        }

        public void AddPart(TextboxLinePart part)
        {
            _parts.Add(part);
        }

        public List<TextboxLinePart> Parts { get { return _parts; } }

        public TextAlignment CurrentAlignment { get; set; }

        public bool Linefeed { get; set; }
    }
}