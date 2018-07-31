using System;
using System.Collections.Generic;
using System.Drawing;
using PdfCraft.Fonts.Standard14;
using PdfCraft.Fonts.TrueType;
using PdfCraft.Fonts.TrueType.Parsing;

namespace PdfCraft.Fonts
{
    internal class FontFactory
    {
        private int nextFontNumber;
        private readonly Dictionary<int, FontObject> fonts = new Dictionary<int, FontObject>();

        public Dictionary<int, FontObject> ToDictionary()
        {
            return fonts;
        }

        public FontObject AddFont(string name, Func<int> getNextObjectNumber, HashSet<FontStyle> fontStyles, FontProperties properties)
        {
            var hash = name.GetHashCode();

            return fonts.ContainsKey(hash)
                ? fonts[hash]
                : CreateFont(getNextObjectNumber, properties, name, fontStyles, hash);
        }

        private FontObject CreateFont(Func<int> getNextObjectNumber, FontProperties properties, string name, HashSet<FontStyle> fontStyles, int hash)
        {
            var pdfFontName = "/F" + nextFontNumber++;

            FontObject font;

            switch (properties.FontType)
            {
                case FontType.TrueType:
                    font = CreateTrueTypeFont(getNextObjectNumber, name, pdfFontName, properties.Bytes);
                    break;
                case FontType.Standard14Font:
                    font = CreateStandard14Font(getNextObjectNumber, name, pdfFontName, fontStyles);
                    break;
                case FontType.Unknown:
                    font = CreateStandard14Font(getNextObjectNumber, "Helvetica", pdfFontName, fontStyles);
                    break;
                default:
                    font = CreateStandard14Font(getNextObjectNumber, "Helvetica", pdfFontName, fontStyles);
                    break;
            }

            fonts.Add(hash, font);

            return font;
        }

        private static FontObject CreateStandard14Font(Func<int> getNextObjectNumber, string name, string pdfFontName, ICollection<FontStyle> fontStyles)
        {
            var fontName = name;
            if (name == "Helvetica" || name == "Courier")
            {
                if (fontStyles.Count > 0)
                    fontName += "-";
                if (fontStyles.Contains(FontStyle.Bold))
                    fontName += "Bold";
                if (fontStyles.Contains(FontStyle.Italic))
                    fontName += "Oblique";
            }

            var font = new Standard14FontObject(getNextObjectNumber(), pdfFontName, fontName);
            font.SetFontDescriptor(new Standard14FontDescriptor(getNextObjectNumber(), font));
            font.SetFontWidths(new Standard14FontWidths(getNextObjectNumber(), font));

            return font;
        }

        private FontObject CreateTrueTypeFont(Func<int> getNextObjectNumber, string name, string pdfFontName, byte[] ttfBytes)
        {
            var ttfParser = new TtfParser();
            var ttfData = ttfParser.ParseTtf(ttfBytes);

            var fontDefinition = new PdfFontDefinition(ttfData);

            var font = new TrueTypeFontObject(getNextObjectNumber(), pdfFontName, fontDefinition, name);
            var descendantFont = new TrueTypeDescendantFont(getNextObjectNumber(), font, fontDefinition);
            var fontDescriptor = new TrueTypeFontDescriptor(getNextObjectNumber(), fontDefinition);
            var fontFile2 = new TrueTypeFontFile2(getNextObjectNumber(), font, fontDefinition);
            var toUnicode = new TrueTypeToUnicode(getNextObjectNumber(), font);

            fontDescriptor.SetFontFile(fontFile2);
            descendantFont.SetFontDescriptor(fontDescriptor);
            font.SetDescendantFont(descendantFont);
            font.SetToUnicode(toUnicode);

            return font;
        }
    }
}