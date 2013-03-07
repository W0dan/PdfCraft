using System;
using System.Collections.Generic;
using System.Linq;
using PdfCraft.Fonts;

namespace PdfCraft.Text
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

        public int LineLength { get; set; }
    }

    internal class BreakPoint
    {
        private readonly bool _hasToBreak;
        private readonly int _lengthInPoints;
        private readonly string _text;

        public BreakPoint(bool hasToBreak, int lengthInPoints, string text)
        {
            _hasToBreak = hasToBreak;
            _lengthInPoints = lengthInPoints;
            _text = text;
        }

        public bool HasToBreak
        {
            get { return _hasToBreak; }
        }

        public string Text
        {
            get { return _text; }
        }

        public int LengthInPoints
        {
            get { return _lengthInPoints; }
        }
    }
}