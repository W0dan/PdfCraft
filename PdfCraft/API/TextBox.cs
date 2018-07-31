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
        private readonly Document owner;
        private readonly List<TextCommand> textCommands = new List<TextCommand>();
        private readonly HashSet<string> fontReferences = new HashSet<string>();

        private readonly HashSet<FontStyle> fontStyles = new HashSet<FontStyle>();
        private FontProperties currentFont;

        internal TextBox(Rectangle sizeAndPosition, Document owner)
        {
            size = new Size(sizeAndPosition.Width.ToMillimeters(), sizeAndPosition.Height.ToMillimeters());
            Position = new Point(sizeAndPosition.X.ToMillimeters(), sizeAndPosition.Y.ToMillimeters());

            this.owner = owner;
        }

        internal override bool IsText => true;

        public Point Position { get; set; }
        private readonly Size size;

        public void Calculate()
        {
            var dummy = Content;
        }

        /// <summary>
        /// call Calculate() in order to populate this property !!
        /// </summary>
        public int CalculatedHeight { get; private set; }

        public void SetFont(FontProperties properties)
        {
            SetFont(properties, false);
        }

        private void SetFont(FontProperties properties, bool onlyStyleChanged)
        {
            if (!onlyStyleChanged)
                currentFont = properties;

            var font = owner.AddFont(properties.Name, fontStyles, properties);
            textCommands.Add(new TextCommand(Command.SetFont, new FontDefinition(font, properties.Size)));

            var fontReference = $"{font.FontName} {font.Number} 0 R ";
            if (!fontReferences.Contains(fontReference))
                fontReferences.Add(fontReference);
        }

        public void SetBoldOn()
        {
            if (!fontStyles.Contains(FontStyle.Bold))
                fontStyles.Add(FontStyle.Bold);
            SetFont(currentFont, true);
        }

        public void SetBoldOff()
        {
            if (fontStyles.Contains(FontStyle.Bold))
                fontStyles.Remove(FontStyle.Bold);
            SetFont(currentFont, true);
        }

        public void SetItalicOn()
        {
            if (!fontStyles.Contains(FontStyle.Italic))
                fontStyles.Add(FontStyle.Italic);
            SetFont(currentFont, true);
        }

        public void SetItalicOff()
        {
            if (fontStyles.Contains(FontStyle.Italic))
                fontStyles.Remove(FontStyle.Italic);
            SetFont(currentFont, true);
        }

        public void SetSuperscriptOn()
        {
            textCommands.Add(new TextCommand(Command.SetSuperscriptOn, null));
        }

        public void SetSuperscriptOff()
        {
            textCommands.Add(new TextCommand(Command.SetSuperscriptOff, null));
        }

        public void SetAlignment(TextAlignment alignment)
        {
            textCommands.Add(new TextCommand(Command.SetAlignment, alignment));
        }

        public IEnumerable<string> GetFontReferences()
        {
            return fontReferences;
        }

        public void SetColor(Color color)
        {
            textCommands.Add(new TextCommand(Command.SetColor, color));
        }

        public void AddText(string text)
        {
            if (text == null)
            {
                textCommands.Add(new TextCommand(Command.AddText, ""));
                return;
            }

            textCommands.Add(new TextCommand(Command.AddText, text));
        }

        internal Document Owner => owner;

        internal override IByteContainer Content
        {
            get
            {
                var currentStyle = new TextStyle
                {
                    Alignment = TextAlignment.Left,
                    Color = Color.Black,
                    Superscript = false,
                    Font = null
                };

                var buffer = new List<TextboxLineBuffer>();

                TextboxLineBuffer lineBuffer = null;

                var wordWrapper = new WordWrapping(size.Width);

                foreach (var textCommand in textCommands)
                {
                    switch (textCommand.Command)
                    {
                        case Command.SetFont:
                            currentStyle = currentStyle.Clone();
                            currentStyle.Font = (FontDefinition)textCommand.Data;
                            break;
                        case Command.SetColor:
                            currentStyle = currentStyle.Clone();
                            currentStyle.Color = (Color)textCommand.Data;
                            break;
                        case Command.SetAlignment:
                            currentStyle = currentStyle.Clone();
                            currentStyle.Alignment = (TextAlignment)textCommand.Data;
                            break;
                        case Command.SetSuperscriptOn:
                            currentStyle = currentStyle.Clone();
                            currentStyle.Superscript = true;
                            break;
                        case Command.SetSuperscriptOff:
                            currentStyle = currentStyle.Clone();
                            currentStyle.Superscript = false;
                            break;
                        case Command.AddText:
                            if (currentStyle.Font == null)
                                throw new ApplicationException("before adding text to a textbox, a font must be set");

                            if (lineBuffer == null)
                            {
                                lineBuffer = new TextboxLineBuffer(currentStyle.Alignment);
                                buffer.Add(lineBuffer);
                            }

                            lineBuffer.CurrentAlignment = currentStyle.Alignment;
                            var addedText = currentStyle.Font.Map(textCommand.Data.ToString());
                            var texts = addedText.Split('\n');

                            for (var i = 0; i < texts.Length; i++)
                            {
                                if (i > 0) //newline encountered
                                {
                                    lineBuffer.Parts.Last().EndOfLine = true;
                                    lineBuffer.Linefeed = true;
                                    wordWrapper = new WordWrapping(size.Width);
                                }

                                var breaks = wordWrapper.WrapIt(texts[i], currentStyle.Font);

                                foreach (var textItem in breaks)
                                {
                                    if (lineBuffer.Parts.Any() && lineBuffer.Parts.Last().EndOfLine)
                                    {
                                        lineBuffer = new TextboxLineBuffer(currentStyle.Alignment);
                                        buffer.Add(lineBuffer);
                                    }

                                    if (!textItem.HasToBreak)
                                    {
                                        var bufferItemPart = new TextboxLinePart(textItem, currentStyle, false);
                                        lineBuffer.AddPart(bufferItemPart);
                                    }
                                    else
                                    {
                                        var bufferItemPart = new TextboxLinePart(textItem, currentStyle, true);
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
            sbBuffered.Append($"1 0 0 1 {Position.X} {Position.Y} Tm ");

            var colorIsSet = false;
            var superscriptIsSet = false;
            FontDefinition currentFont = null;
            var previousColor = Color.Black;

            double previousLineLength = -1;

            var yPosition = Position.Y;
            var textLeading = 0;
            CalculatedHeight = 0;


            foreach (var line in buffer)
            {
                var firstPartOnLine = true;
                var textLeadingForCurrentLine = "T* ";

                //var sbBufferedLine = new StringBuilder();
                var sbBufferedLine = ByteContainerFactory.CreateByteContainer();
                foreach (var part in line.Parts)
                {
                    if (firstPartOnLine)
                    {
                        firstPartOnLine = false;

                        var tempTextLeading = (int)(part.Style.Font.Size * 1.21);
                        if (textLeading != tempTextLeading)
                        {
                            textLeading = tempTextLeading;
                            textLeadingForCurrentLine = $"{textLeading} TL T* ";
                        }
                    }

                    if (!colorIsSet || part.Style.Color != previousColor)
                    {
                        colorIsSet = true;
                        previousColor = part.Style.Color;
                        sbBufferedLine.Append(string.Format(part.Style.Color.ToPdfColor() + " rg "));
                    }

                    if (part.Style.Font != currentFont)
                    {
                        currentFont = part.Style.Font;
                        sbBufferedLine.Append($"{part.Style.Font.Font.FontName} {part.Style.Font.Size} Tf ");

                        var tempTextLeading = (int)(part.Style.Font.Size * 1.21);
                        if (tempTextLeading > textLeading)
                        {
                            textLeading = tempTextLeading;
                            textLeadingForCurrentLine = $"{textLeading} TL T* ";
                        }
                    }

                    if (!superscriptIsSet && part.Style.Superscript)
                    {
                        superscriptIsSet = true;
                        sbBufferedLine.Append($"{part.Style.Font.Font.FontName} {part.Style.Font.Size / 2} Tf ");
                        sbBufferedLine.Append($"{part.Style.Font.Size / 2} Ts ");
                    }

                    if (superscriptIsSet && !part.Style.Superscript)
                    {
                        superscriptIsSet = false;
                        sbBufferedLine.Append($"{part.Style.Font.Font.FontName} {part.Style.Font.Size} Tf ");
                        sbBufferedLine.Append("0 Ts ");
                    }

                    if (part.TextItem.Text.Length > 0)
                    {
                        sbBufferedLine.Append("(");
                        sbBufferedLine.Append(currentFont.Encode(part.TextItem.Text));
                        sbBufferedLine.Append(") Tj");
                    }

                    if (part.EndOfLine)
                    {
                        if (superscriptIsSet)  // ????
                        {
                            superscriptIsSet = false;
                            //sbBufferedLine.Append($"{part.Style.Font.Font.FontName} {part.Style.Font.Size} Tf ");
                            sbBufferedLine.Append("0 Ts ");
                        }
                    }
                    sbBufferedLine.Append(StringConstants.NewLine);
                }
                yPosition -= textLeading;
                CalculatedHeight += textLeading;

                sbBuffered.Append(textLeadingForCurrentLine);

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

            CalculatedHeight = (int)(CalculatedHeight / 2.54 + textLeading / 2.54 / 1.6);

            return sbBuffered;
        }

        private IByteContainer AlignJustify(IByteContainer sbBuffered, double currentLineLength, bool isLastLineOfParagraph, int horizontalMargin, int yPosition, ref double previousLineLength)
        {
            if (previousLineLength >= 0)
            {
                sbBuffered.Append($"1 0 0 1 {horizontalMargin} {yPosition} Tm ");
                previousLineLength = -1;
            }

            if (currentLineLength > 0)
            {
                double scaleFactor;
                if (isLastLineOfParagraph)
                    scaleFactor = 100;
                else
                    scaleFactor = size.Width / currentLineLength * 100;

                sbBuffered.Append($"{scaleFactor:0.###} Tz ".Replace(',', '.'));
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
                xOffset = (size.Width - currentLineLength) / 2;
            else
                xOffset = (previousLineLength - currentLineLength) / 2;

            sbBuffered.Append($"{xOffset:0.###} 0 Td ".Replace(',', '.'));

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
                xOffset = size.Width - currentLineLength;
            else
                xOffset = previousLineLength - currentLineLength;

            sbBuffered.Append($"{xOffset:0.###} 0 Td ".Replace(',', '.'));

            previousLineLength = currentLineLength;
            return sbBuffered;
        }

        private static IByteContainer AlignLeft(IByteContainer sbBuffered, int horizontalMargin, int yPosition, ref double previousLineLength)
        {
            if (previousLineLength >= 0)
            {
                sbBuffered.Append($"1 0 0 1 {horizontalMargin} {yPosition} Tm ");
                previousLineLength = -1;
            }

            sbBuffered.Append("100 Tz ");
            return sbBuffered;
        }
    }
}