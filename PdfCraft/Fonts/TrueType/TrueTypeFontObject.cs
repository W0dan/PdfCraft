using System.Linq;
using System.Text;
using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft.Fonts.TrueType
{
    public class TrueTypeFontObject : FontObject
    {
        private readonly PdfFontDefinition fontDefinition;
        public UsedCharacterList UsedCharacters { get; set; }

        private TrueTypeDescendantFont descendantFont;
        private TrueTypeToUnicode toUnicode;

        public TrueTypeFontObject(int objectNumber, string fontName, PdfFontDefinition fontDefinition, string name)
            : base(objectNumber, fontName, name)
        {
            this.fontDefinition = fontDefinition;
            this.UsedCharacters = new UsedCharacterList();
        }

        public void SetDescendantFont(TrueTypeDescendantFont value)
        {
            this.descendantFont = value;
            base.ChildObjects.Add(value);
        }

        public void SetToUnicode(TrueTypeToUnicode value)
        {
            this.toUnicode = value;
            base.ChildObjects.Add(value);
        }

        public override IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory
                    .CreateByteContainer($"<<{StringConstants.NewLine}" +
                                         $"/Type /Font{StringConstants.NewLine}" +
                                         $"/Subtype /Type0{StringConstants.NewLine}" +
                                         $"/Name {base.FontName}{StringConstants.NewLine}" +
                                         $"/BaseFont /{fontDefinition.FontName}{StringConstants.NewLine}" +
                                         $"/Encoding /Identity-H{StringConstants.NewLine}" +
                                         $"/DescendantFonts [{descendantFont.Number} 0 R]{StringConstants.NewLine}" +
                                         $"/ToUnicode {toUnicode.Number} 0 R{StringConstants.NewLine}" +
                                         $">>");

                SetContent(content);

                return base.Content;
            }
        }

        public override string Map(string text)
        {
            var result = new StringBuilder(text);

            foreach (var c in text)
            {
                var metric = fontDefinition.FontMetrics[c];
                if (UsedCharacters.All(x => x.Char != c) && metric.CharacterMapping != 0)
                {
                    UsedCharacters.AddSorted(new UsedCharacter
                    {
                        Char = c,
                        Metric = metric
                    });
                }
            }

            //var temp = result.Replace(@"\", @"\\");
            //temp = temp.Replace(@"(", @"\(");
            //temp = temp.Replace(@")", @"\)");

            return result.ToString();
        }

        public override int GetWidth(char c, int size)
        {
            var usedCharacter = UsedCharacters.SingleOrDefault(x => x.Char == c);

            if (usedCharacter == null)
            {
                return c == '\t' ? 1000 * size : 0;
            }

            return usedCharacter.Metric.CharacterWidth * size;
        }

        public override byte[] Encode(string text)
        {
            var escapedChars = Encoding.Default.GetBytes("()\\");

            var result = ByteContainerFactory.CreateByteContainer();

            foreach (var c in text)
            {
                var metric = UsedCharacters.SingleOrDefault(uc => uc.Char == c)?.Metric;

                if (metric == null) continue;

                var char1 = metric.CharacterMapping >> 8;
                if (escapedChars.Contains((byte) char1))
                    result.Append('\\');
                result.Append((byte)char1);

                var char2 = metric.CharacterMapping & 0x00ff;
                if (escapedChars.Contains((byte)char2))
                    result.Append('\\');
                result.Append((byte)char2);
                //result.Append((byte)(metric.CharacterMapping & 0x00ff));
            }

            // todo: replace '(', ')' and '\' here ?

            return result.GetBytes();
        }
    }
}