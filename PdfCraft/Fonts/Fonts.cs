using System;
using System.Collections.Generic;

namespace PdfCraft.Fonts
{
    internal class Fonts
    {
        private int _nextFontNumber;
        private readonly Dictionary<int, FontObject> _fonts = new Dictionary<int, FontObject>();

        public Dictionary<int, FontObject> ToDictionary()
        {
            return _fonts;
        }

        public FontObject AddFont(string name, Func<int> getNextObjectNumber)
        {
            var hash = name.GetHashCode();

            FontObject font;
            if (!_fonts.ContainsKey(hash))
            {
                font = FontFactory.CreateFont(getNextObjectNumber(), _nextFontNumber++, name);
                var fontDescriptor = new FontDescriptor(getNextObjectNumber(), font);
                font.FontDescriptor = fontDescriptor;
                var fontWidths = new Fontwidths(getNextObjectNumber(), font);
                font.FontWidths = fontWidths;

                _fonts.Add(hash, font);
            }
            else
                font = _fonts[hash];

            return font;
        }
    }
}