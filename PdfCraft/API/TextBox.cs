using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using System.Text;
using PdfCraft.Constants;
using PdfCraft.Extensions;
using PdfCraft.Fonts;
using PdfCraft.Text;

namespace PdfCraft.API
{
    public class TextBox : ContentsPart
    {
        private readonly Document _owner;
        private readonly List<TextCommand> _textCommands = new List<TextCommand>();
        private readonly HashSet<string> _fontNames = new HashSet<string>();
        private Size _size;
        private ResourceManager _fontWidths;

        internal TextBox(Rectangle sizeAndPosition, Document owner)
        {
            _size = new Size(sizeAndPosition.Width.ToMillimeters(), sizeAndPosition.Height.ToMillimeters());
            Position = new Point(sizeAndPosition.X.ToMillimeters(), sizeAndPosition.Y.ToMillimeters());

            _owner = owner;
        }

        internal override bool IsText { get { return true; } }

        public Point Position { get; set; }

        public void SetFont(FontProperties properties)
        {
            var font = _owner.AddFont(properties.Name);
            _textCommands.Add(new TextCommand(Command.SetFont, new FontDefinition(font, properties.Size)));

            var fontname = string.Format("{0} {1} 0 R ", font.FontName, font.Number);
            if (!_fontNames.Contains(fontname))
                _fontNames.Add(fontname);
        }

        public void SetAlignment(TextAlignment alignment)
        {
            _textCommands.Add(new TextCommand(Command.SetAlignment, alignment));
        }

        public IEnumerable<string> GetFontnames()
        {
            return _fontNames;
        }

        public void SetColor(Color color)
        {
            _textCommands.Add(new TextCommand(Command.SetColor, color));
        }

        public void AddText(string text)
        {
            _textCommands.Add(new TextCommand(Command.AddText, text));
        }

        internal Document Owner
        {
            get { return _owner; }
        }

        internal override string Content
        {
            get
            {
                var sb = new StringBuilder();
                FontDefinition currentFont = null;
                var currentAlignment = TextAlignment.Left;
                var currentColor = Color.Black;

                sb.Append(string.Format("1 0 0 1 {0} {1} Tm ", Position.X, Position.Y));

                WordWrapping wordWrapper = null;

                foreach (var textCommand in _textCommands)
                {
                    switch (textCommand.Command)
                    {
                        case Command.SetFont:
                            currentFont = (FontDefinition)textCommand.Data;

                            sb.Append(string.Format("{0} {1} Tf ", currentFont.Font.FontName, currentFont.Size));

                            if (wordWrapper == null)
                            {
                                wordWrapper = CreateWordWrapper();

                                var textLeading = (int)(currentFont.Size * 1.21);
                                sb.Append(string.Format("0 -{0} TD ", textLeading));
                            }

                            break;
                        case Command.SetColor:
                            currentColor = (Color)textCommand.Data;

                            sb.Append(currentColor.ToPdfColor() + " rg ");
                            break;
                        case Command.SetAlignment:
                            currentAlignment = (TextAlignment)textCommand.Data;
                            break;
                        case Command.AddText:
                            if (currentFont == null)
                                throw new ApplicationException("before adding text to a textbox, a font must be set");

                            var addedText = (string)textCommand.Data;
                            var texts = addedText.Split('\n');

                            for (var i = 0; i < texts.Length; i++)
                            {
                                if (i > 0) //newline encountered
                                {
                                    sb.Append(" T*");
                                    wordWrapper = CreateWordWrapper();
                                }
                                sb = AddText(wordWrapper, currentFont, texts[i], sb, currentAlignment);
                            }

                            break;
                    }
                }

                return sb.ToString();
            }
        }

        private WordWrapping CreateWordWrapper()
        {
            _fontWidths = new ResourceManager("PdfCraft.Fonts.fontwidths", typeof(Document).Assembly);

            var wordWrapper = new WordWrapping
            {
                LineLength = _size.Width * 1000,
                GetGlyphWidth = (c, f) =>
                {
                    var name = f.Font.Name + "-" + ((byte)c).ToString("000");
                    var dictWidth = _fontWidths.GetString(name);
                    var width = string.IsNullOrEmpty(dictWidth) ? 610 : int.Parse(dictWidth);
                    return width * f.Size;
                }
            };
            return wordWrapper;
        }

        private static StringBuilder AddText(WordWrapping wordWrapper, FontDefinition currentFont, string text, StringBuilder sb, TextAlignment currentAlignment)
        {
            if (wordWrapper == null)
                throw new ArgumentNullException("wordWrapper", "please set a font for the textbox before adding text");

            var breaks = wordWrapper.WrapIt(text, currentFont);

            switch (currentAlignment)
            {
                case TextAlignment.Left:
                    sb.Append(GetTextLeftAligned(breaks));
                    break;
                case TextAlignment.Right:
                    break;
                case TextAlignment.Center:
                    break;
                case TextAlignment.Justify:
                    break;
            }

            return sb;
        }

        private static StringBuilder GetTextLeftAligned(IEnumerable<BreakPoint> breaks)
        {
            var sb = new StringBuilder();

            foreach (var breakPoint in breaks)
            {
                if (breakPoint.PositionToBreak < 0)
                    sb.Append(string.Format("({0}) Tj", breakPoint.Text) + StringConstants.NewLine);
                else
                    sb.Append(string.Format("({0}) Tj T*", breakPoint.Text) + StringConstants.NewLine);
            }

            return sb;
        }
    }
}