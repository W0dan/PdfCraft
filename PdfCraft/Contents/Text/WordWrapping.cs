using System.Collections.Generic;
using System.Linq;
using PdfCraft.Fonts;

namespace PdfCraft.Contents.Text
{
    internal class WordWrapping
    {
        private int _runningLineLength;

        public WordWrapping(int width)
        {
            LineLength = width * 1000;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="currentFont"></param>
        /// <returns>an array of positions where to break the text, empty when nothing has to change</returns>
        public List<BreakPoint> WrapIt(string text, FontDefinition currentFont)
        {
            var breakPositions = new List<BreakPoint>();

            var lastBreakPosition = -1;
            var previousBreakPoint = 0;

            var lengthSinceLastBreak = 0;
            var runningPartLength = 0;

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (c == ' ')
                {
                    lengthSinceLastBreak = 0;
                    lastBreakPosition = i;
                }
                else if (c == '\\')
                {
                    //next value(s) are escaped
                    c = text[++i];
                    if (!IsRegularEscapedValue(c))
                    {
                        var octalCode = new string(c, 1);
                        octalCode += text[++i];
                        octalCode += text[++i];

                        var charCode = OctalToDecimal(octalCode);

                        c = (char)charCode;
                    }
                }

                var glyphWidth = currentFont.GetWidth(c);

                _runningLineLength += glyphWidth;
                runningPartLength += glyphWidth;
                lengthSinceLastBreak += glyphWidth;

                if (_runningLineLength > LineLength)
                {
                    var textPart = text.Substring(previousBreakPoint, lastBreakPosition + 1 - previousBreakPoint);

                    breakPositions.Add(new BreakPoint(
                        true,
                        runningPartLength - lengthSinceLastBreak,
                        textPart));

                    runningPartLength = lengthSinceLastBreak;

                    if (textPart.Any() && textPart.Last() == ' ')
                    {
                        lengthSinceLastBreak -= glyphWidth;
                        runningPartLength -= currentFont.GetWidth(' ');
                    }

                    previousBreakPoint = lastBreakPosition + 1;
                    _runningLineLength = lengthSinceLastBreak;
                }
            }

            breakPositions.Add(new BreakPoint(
                false,
                runningPartLength,
                text.Substring(previousBreakPoint)));

            return breakPositions;
        }

        private static bool IsRegularEscapedValue(char c)
        {
            return c == '\\' || c == '(' || c == ')';
        }

        private static int OctalToDecimal(string octalValue)
        {
            var factor = 1;
            var result = 0;
            for (var i = octalValue.Length - 1; i >= 0; i--)
            {
                result += int.Parse(new string(octalValue[i], 1)) * factor;
                factor *= 8;
            }
            return result;
        }

        public int LineLength { get; set; }
    }
}