using System.Collections.Generic;
using System.Text;
using PdfCraft.API;

namespace PdfCraft.Contents.Text
{
    /// <summary>
    /// defines a line of text
    /// </summary>
    internal class TextboxLineBuffer
    {
        public TextboxLineBuffer(TextAlignment currentAlignment)
        {
            CurrentAlignment = currentAlignment;
        }

        public void AddPart(TextboxLinePart part)
        {
            Parts.Add(part);
        }

        public List<TextboxLinePart> Parts { get; } = new List<TextboxLinePart>();

        public TextAlignment CurrentAlignment { get; set; }

        public bool Linefeed { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var part in Parts)
            {
                result.Append(part);
            }

            return result.ToString();
        }
    }
}