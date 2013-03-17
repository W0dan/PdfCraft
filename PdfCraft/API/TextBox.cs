using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using PdfCraft.Constants;
using PdfCraft.Containers;
using PdfCraft.Contents;
using PdfCraft.Contents.Text;
using PdfCraft.Extensions;
using PdfCraft.Fonts;

namespace PdfCraft.API
{
    public class TextBox : ContentsPart
    {
        private readonly Document _owner;
        private readonly List<TextCommand> _textCommands = new List<TextCommand>();
        private readonly HashSet<string> _fontReferences = new HashSet<string>();
        private readonly Size _size;

        private readonly HashSet<FontStyle> _fontStyles = new HashSet<FontStyle>();
        private FontProperties _currentFont;

        internal TextBox(Rectangle sizeAndPosition, Document owner)
        {
            _size = new Size(sizeAndPosition.Width.ToMillimeters(), sizeAndPosition.Height.ToMillimeters());
            Position = new Point(sizeAndPosition.X.ToMillimeters(), sizeAndPosition.Y.ToMillimeters());

            _owner = owner;
        }

        internal override bool IsText { get { return true; } }

        public Point Position { get; set; }

        public void SetFont(FontProperties properties, bool onlyStyleChanged = false)
        {
            var fontName = properties.Name;
            if (properties.Name == "Helvetica" || properties.Name == "Courier")
            {
                if (_fontStyles.Count > 0)
                    fontName += "-";
                if (_fontStyles.Contains(FontStyle.Bold))
                    fontName += "Bold";
                if (_fontStyles.Contains(FontStyle.Italic))
                    fontName += "Oblique";
            }

            if (!onlyStyleChanged)
                _currentFont = properties;

            var font = _owner.AddFont(fontName);
            _textCommands.Add(new TextCommand(Command.SetFont, new FontDefinition(font, properties.Size)));

            var fontReference = string.Format("{0} {1} 0 R ", font.FontName, font.Number);
            if (!_fontReferences.Contains(fontReference))
                _fontReferences.Add(fontReference);
        }

        public void SetBoldOn()
        {
            if (!_fontStyles.Contains(FontStyle.Bold))
                _fontStyles.Add(FontStyle.Bold);
            SetFont(_currentFont, true);
        }

        public void SetBoldOff()
        {
            if (_fontStyles.Contains(FontStyle.Bold))
                _fontStyles.Remove(FontStyle.Bold);
            SetFont(_currentFont, true);
        }

        public void SetItalicOn()
        {
            if (!_fontStyles.Contains(FontStyle.Italic))
                _fontStyles.Add(FontStyle.Italic);
            SetFont(_currentFont, true);
        }

        public void SetItalicOff()
        {
            if (_fontStyles.Contains(FontStyle.Italic))
                _fontStyles.Remove(FontStyle.Italic);
            SetFont(_currentFont, true);
        }

        public void SetAlignment(TextAlignment alignment)
        {
            _textCommands.Add(new TextCommand(Command.SetAlignment, alignment));
        }

        public IEnumerable<string> GetFontnames()
        {
            return _fontReferences;
        }

        public void SetColor(Color color)
        {
            _textCommands.Add(new TextCommand(Command.SetColor, color));
        }

        public void AddText(string text)
        {
            text = text.Replace(@"\", @"\\");
            text = text.Replace(@"(", @"\(");
            text = text.Replace(@")", @"\)");
            text = text.Replace("Æ", @"\306");
            text = text.Replace("Á", @"\301");
            text = text.Replace("Â", @"\302");
            text = text.Replace("Ä", @"\304");
            text = text.Replace("À", @"\300");
            text = text.Replace("Å", @"\305");
            text = text.Replace("Ã", @"\303");
            text = text.Replace("Ç", @"\307");
            text = text.Replace("É", @"\311");
            text = text.Replace("Ê", @"\312");
            text = text.Replace("Ë", @"\313");
            text = text.Replace("È", @"\310");
            text = text.Replace("Ð", @"\320");
            text = text.Replace("€", @"\200");
            text = text.Replace("Í", @"\315");
            text = text.Replace("Î", @"\316");
            text = text.Replace("Ï", @"\316");
            text = text.Replace("Ì", @"\314");
            text = text.Replace("Ñ", @"\321");
            text = text.Replace("Œ", @"\214");
            text = text.Replace("Ó", @"\323");
            text = text.Replace("Ô", @"\324");
            text = text.Replace("Ö", @"\326");
            text = text.Replace("Ò", @"\322");
            text = text.Replace("Ø", @"\330");
            text = text.Replace("Õ", @"\325");
            text = text.Replace("Š", @"\212");
            text = text.Replace("Þ", @"\336");
            text = text.Replace("Ú", @"\332");
            text = text.Replace("Û", @"\333");
            text = text.Replace("Ü", @"\334");
            text = text.Replace("Ù", @"\331");
            text = text.Replace("Ý", @"\335");
            text = text.Replace("Ÿ", @"\237");
            text = text.Replace("Ž", @"\216");
            text = text.Replace("á", @"\341");
            text = text.Replace("â", @"\342");
            text = text.Replace("´", @"\264");
            text = text.Replace("ä", @"\344");
            text = text.Replace("æ", @"\346");
            text = text.Replace("à", @"\340");

            text = text.Replace("º", @"\272");
            text = text.Replace("°", @"\260");
            text = text.Replace("–", @"\226");
            text = text.Replace("ë", @"\353");
            _textCommands.Add(new TextCommand(Command.AddText, text));
        }

        internal Document Owner
        {
            get { return _owner; }
        }

        internal override IByteContainer Content
        {
            get
            {
                FontDefinition currentFont = null;
                var currentAlignment = TextAlignment.Left;
                var currentColor = Color.Black;

                var buffer = new List<TextboxLineBuffer>();

                TextboxLineBuffer lineBuffer = null;

                var wordWrapper = new WordWrapping(_size.Width);

                foreach (var textCommand in _textCommands)
                {
                    switch (textCommand.Command)
                    {
                        case Command.SetFont:
                            currentFont = (FontDefinition)textCommand.Data;
                            break;
                        case Command.SetColor:
                            currentColor = (Color)textCommand.Data;
                            break;
                        case Command.SetAlignment:
                            currentAlignment = (TextAlignment)textCommand.Data;
                            break;
                        case Command.AddText:
                            if (currentFont == null)
                                throw new ApplicationException("before adding text to a textbox, a font must be set");

                            if (lineBuffer == null)
                            {
                                lineBuffer = new TextboxLineBuffer(currentAlignment);
                                buffer.Add(lineBuffer);
                            }

                            lineBuffer.CurrentAlignment = currentAlignment;
                            var addedText = textCommand.Data.ToString();
                            var texts = addedText.Split('\n');

                            for (var i = 0; i < texts.Length; i++)
                            {
                                if (i > 0) //newline encountered
                                {
                                    lineBuffer.Parts.Last().EndOfLine = true;
                                    lineBuffer.Linefeed = true;
                                    wordWrapper = new WordWrapping(_size.Width);
                                }

                                var breaks = wordWrapper.WrapIt(texts[i], currentFont);

                                foreach (var textItem in breaks)
                                {
                                    if (lineBuffer.Parts.Any() && lineBuffer.Parts.Last().EndOfLine)
                                    {
                                        lineBuffer = new TextboxLineBuffer(currentAlignment);
                                        buffer.Add(lineBuffer);
                                    }

                                    if (!textItem.HasToBreak)
                                    {
                                        var bufferItemPart = new TextboxLinePart(textItem, currentFont, currentColor, false);
                                        lineBuffer.AddPart(bufferItemPart);
                                    }
                                    else
                                    {
                                        var bufferItemPart = new TextboxLinePart(textItem, currentFont, currentColor, true);
                                        lineBuffer.AddPart(bufferItemPart);
                                    }
                                }
                            }
                            break;
                    }
                }

                return WriteBufferToPdf(buffer);
            }
        }

        private IByteContainer WriteBufferToPdf(IEnumerable<TextboxLineBuffer> buffer)
        {
            var sbBuffered = ByteContainerFactory.CreateByteContainer();
            sbBuffered.Append(string.Format("1 0 0 1 {0} {1} Tm ", Position.X, Position.Y));

            var textLeadingIsSet = false;
            var colorIsSet = false;
            FontDefinition previousFont = null;
            var previousColor = Color.Black;

            double previousLineLength = -1;

            var yPosition = Position.Y;
            var textLeading = 0;

            foreach (var line in buffer)
            {
                var sbBufferedLine = new StringBuilder();
                foreach (var part in line.Parts)
                {
                    if (!textLeadingIsSet)
                    {
                        textLeadingIsSet = true;
                        textLeading = (int)(part.Font.Size * 1.21);
                        sbBuffered.Append(string.Format("0 -{0} TD ", textLeading));
                    }

                    if (!colorIsSet || part.Color != previousColor)
                    {
                        colorIsSet = true;
                        previousColor = part.Color;
                        sbBufferedLine.Append(string.Format(part.Color.ToPdfColor() + " rg "));
                    }

                    if (part.Font != previousFont)
                    {
                        previousFont = part.Font;
                        sbBufferedLine.Append(string.Format("{0} {1} Tf ", part.Font.Font.FontName, part.Font.Size));

                        textLeading = (int)(part.Font.Size * 1.21);
                        sbBuffered.Append(string.Format("{0} TL ", textLeading));
                    }

                    if (part.TextItem.Text.Length > 0)
                        sbBufferedLine.Append(string.Format("({0}) Tj", part.TextItem.Text));

                    if (part.EndOfLine)
                        sbBufferedLine.Append(" T*" + StringConstants.NewLine);
                    else
                        sbBufferedLine.Append(StringConstants.NewLine);
                }
                yPosition -= textLeading;

                var currentLineLength = (double)line.Parts.Sum(x => x.TextItem.LengthInPoints) / 1000;
                switch (line.CurrentAlignment)
                {
                    case TextAlignment.Left:
                        sbBuffered = AlignLeft(sbBuffered, Position.X, yPosition, ref previousLineLength);
                        break;
                    case TextAlignment.Right:
                        sbBuffered = AlignRight(sbBuffered, currentLineLength, ref previousLineLength);
                        break;
                    case TextAlignment.Center:
                        sbBuffered = AlignCenter(sbBuffered, currentLineLength, ref previousLineLength);
                        break;
                    case TextAlignment.Justify:
                        sbBuffered = AlignJustify(sbBuffered, currentLineLength, line.Linefeed, Position.X, yPosition, ref previousLineLength);
                        break;
                }

                sbBuffered.Append(sbBufferedLine);
            }

            return sbBuffered;
        }

        private IByteContainer AlignJustify(IByteContainer sbBuffered, double currentLineLength, bool isLastLineOfParagraph, int horizontalMargin, int yPosition, ref double previousLineLength)
        {
            if (previousLineLength >= 0)
            {
                sbBuffered.Append(string.Format("1 0 0 1 {0} {1} Tm ", horizontalMargin, yPosition));
                previousLineLength = -1;
            }

            if (currentLineLength > 0)
            {
                double scaleFactor;
                if (isLastLineOfParagraph)
                    scaleFactor = 100;
                else
                    scaleFactor = _size.Width / currentLineLength * 100;

                sbBuffered.Append(string.Format("{0:0.###} Tz ", scaleFactor).Replace(',', '.'));
            }

            return sbBuffered;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sbBuffered"></param>
        /// <param name="currentLineLength"></param>
        /// <param name="previousLineLength">
        ///     is needed because when we change the start of a line, the starting position of the next line
        ///     will be relative to this new position
        /// </param>
        /// <returns></returns>
        private IByteContainer AlignCenter(IByteContainer sbBuffered, double currentLineLength, ref double previousLineLength)
        {
            double xOffset;
            if (previousLineLength < 0)
                xOffset = (_size.Width - currentLineLength) / 2;
            else
                xOffset = (previousLineLength - currentLineLength) / 2;

            sbBuffered.Append(string.Format("{0:0.###} 0 Td ", xOffset).Replace(',', '.'));

            previousLineLength = currentLineLength;

            return sbBuffered;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sbBuffered"></param>
        /// <param name="currentLineLength"></param>
        /// <param name="previousLineLength">
        ///     is needed because when we change the start of a line, the starting position of the next line
        ///     will be relative to this new position
        /// </param>
        /// <returns></returns>
        private IByteContainer AlignRight(IByteContainer sbBuffered, double currentLineLength, ref double previousLineLength)
        {
            double xOffset;
            if (previousLineLength < 0)
                xOffset = _size.Width - (currentLineLength);
            else
                xOffset = (previousLineLength - currentLineLength);

            sbBuffered.Append(string.Format("{0:0.###} 0 Td ", xOffset).Replace(',', '.'));

            previousLineLength = currentLineLength;
            return sbBuffered;
        }

        private static IByteContainer AlignLeft(IByteContainer sbBuffered, int horizontalMargin, int yPosition, ref double previousLineLength)
        {
            if (previousLineLength >= 0)
            {
                sbBuffered.Append(string.Format("1 0 0 1 {0} {1} Tm ", horizontalMargin, yPosition));
                previousLineLength = -1;
            }

            sbBuffered.Append("100 Tz ");
            return sbBuffered;
        }
    }
}