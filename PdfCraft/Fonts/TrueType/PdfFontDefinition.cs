using System;
using PdfCraft.Fonts.TrueType.Parsing;

namespace PdfCraft.Fonts.TrueType
{
    public class PdfFontDefinition
    {
        public PdfFontDefinition(TtfData ttfData)
        {
            this.TtfData = ttfData;

            FontName = "BBCDEE+" + ttfData.Name.FontName;
            FullFontName = ttfData.Name.FullName;
            FamilyName = ttfData.Name.FamilyName;

            Ascender = ttfData.ConvertUnit(ttfData.Os2.STypoAscender);
            Descender = ttfData.ConvertUnit(ttfData.Os2.STypoDescender);
            CapHeight = ttfData.ConvertUnit(ttfData.Os2.SCapHeight);

            FontBBox = new[]
            {
                ttfData.ConvertUnit(ttfData.Head.XMin),
                ttfData.ConvertUnit(ttfData.Head.YMin),
                ttfData.ConvertUnit(ttfData.Head.XMax),
                ttfData.ConvertUnit(ttfData.Head.YMax)
            };

            FontHeight = Convert.ToInt32(Math.Round((FontBBox[3] - (double)FontBBox[1]) / 1000));
            if (FontHeight == 0) FontHeight = 1;

            ItalicAngle = ttfData.ConvertUnit(ttfData.Post.ItalicAngle);
            UnderlinePosition = ttfData.Post.UnderlinePosition;
            UnderlineThickness = ttfData.Post.UnderlineThickness;
            IsFixedPitch = ttfData.Post.IsFixedPitch != 0;

            var cmapEncoding = ttfData.Cmap.GetCmapEncoding();

            if (cmapEncoding != null)
            {
                for (ushort i = 0; i < 65535; i++)
                {
                    var characterMapping = cmapEncoding.Map(i);
                    if (characterMapping >= ttfData.Hmtx.LongHorMetrics.Count) // fix for Arial ?
                    {
                        // todo: https://docs.microsoft.com/en-us/typography/opentype/spec/hmtx
                        continue;
                    }

                    var advanceWidth = ttfData.Hmtx.LongHorMetrics[characterMapping].AdvanceWidth;

                    var metric = new PdfCharacterMetric
                    {
                        CharacterMapping = characterMapping,
                        CharacterWidth = ttfData.ConvertUnit(advanceWidth)
                    };

                    FontMetrics[i] = metric;
                }
            }
        }

        public TtfData TtfData { get; }

        public string FontName { get; }
        public string FullFontName { get; }
        public string FamilyName { get; }
        public bool IsFixedPitch { get; }
        public int ItalicAngle { get; }
        public int UnderlinePosition { get; }
        public int UnderlineThickness { get; }
        public int CapHeight { get; }
        public int Ascender { get; }
        public int Descender { get; }
        public int[] FontBBox { get; }
        public int FontHeight { get; }
        public PdfCharacterMetric[] FontMetrics { get; } = new PdfCharacterMetric[65535];

        /// <summary>
        /// not used for truetype
        /// </summary>
        public string FontWeight { get; set; } = "";
        /// <summary>
        /// not used for truetype
        /// </summary>
        public string CharacterSet { get; set; } = "";
        /// <summary>
        /// not used for truetype
        /// </summary>
        public string EncodingScheme { get; set; } = "";
        /// <summary>
        /// not used for truetype
        /// </summary>
        public int StdHw { get; set; } = 0;
        /// <summary>
        /// not used for truetype
        /// </summary>
        public int StdVw { get; set; } = 0;
    }
}