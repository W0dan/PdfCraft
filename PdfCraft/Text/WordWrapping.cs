using System;
using System.Collections.Generic;
using PdfCraft.API;
using PdfCraft.Fonts;

namespace PdfCraft.Text
{
    internal class WordWrapping
    {
        private int _runningLengthFromLastBreak;

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
            var currentPosition = 0;
            var runningLengthFromLastBreakPosition = 0;
            var previousBreakPoint = 0;
            foreach (var c in text)
            {
                if (c == ' ')
                {
                    runningLengthFromLastBreakPosition = 0;
                    lastBreakPosition = currentPosition;
                }

                var cWidth = GetGlyphWidth(c, currentFont);

                _runningLengthFromLastBreak += cWidth;
                runningLengthFromLastBreakPosition += cWidth;

                if (_runningLengthFromLastBreak > LineLength)
                {
                    breakPositions.Add(new BreakPoint(
                        lastBreakPosition + 1, 
                        _runningLengthFromLastBreak - runningLengthFromLastBreakPosition, 
                        text.Substring(previousBreakPoint, lastBreakPosition + 1 - previousBreakPoint)));

                    previousBreakPoint = lastBreakPosition + 1;
                    if (c == ' ')
                        runningLengthFromLastBreakPosition -= GetGlyphWidth(c, currentFont);
                    _runningLengthFromLastBreak = runningLengthFromLastBreakPosition;
                }

                currentPosition++;
            }

            breakPositions.Add(new BreakPoint(
                -1, 
                _runningLengthFromLastBreak - runningLengthFromLastBreakPosition, 
                text.Substring(previousBreakPoint)));

            return breakPositions;
        }

        public int LineLength { get; set; }

        public Func<char, FontDefinition, int> GetGlyphWidth { get; set; }
    }

    internal class BreakPoint
    {
        private readonly int _positionToBreak;
        private readonly int _lengthInPoints;
        private readonly string _text;

        public BreakPoint(int positionToBreak, int lengthInPoints, string text)
        {
            _positionToBreak = positionToBreak;
            _lengthInPoints = lengthInPoints;
            _text = text;
        }

        public int PositionToBreak
        {
            get { return _positionToBreak; }
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