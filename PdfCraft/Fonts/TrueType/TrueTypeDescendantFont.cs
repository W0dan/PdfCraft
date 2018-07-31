using System.Text;
using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft.Fonts.TrueType
{
    public class TrueTypeDescendantFont : BasePdfObject
    {
        private readonly TrueTypeFontObject parent;
        private readonly PdfFontDefinition fontDefinition;
        private TrueTypeFontDescriptor fontDescriptor;

        public TrueTypeDescendantFont(int objectNumber, TrueTypeFontObject parent, PdfFontDefinition fontDefinition) : base(objectNumber)
        {
            this.parent = parent;
            this.fontDefinition = fontDefinition;
        }

        public void SetFontDescriptor(TrueTypeFontDescriptor value)
        {
            fontDescriptor = value;
            ChildObjects.Add(value);
        }

        public override IByteContainer Content
        {
            get
            {
                var usedCharacters = parent.UsedCharacters;

                var metrics = ByteContainerFactory.CreateByteContainer();

                if (usedCharacters.Count > 0)
                {
                    metrics.Append("/W [");
                    var previousMetric = usedCharacters[0].Metric;
                    metrics.Append($"{previousMetric.CharacterMapping}[{previousMetric.CharacterWidth}");
                    for (var i = 1; i < usedCharacters.Count; i++)
                    {
                        var metric = usedCharacters[i].Metric;

                        // opeenvolgende characters samen in 1 subset
                        if (metric.CharacterMapping == previousMetric.CharacterMapping + 1)
                        {
                            metrics.Append($" {metric.CharacterWidth}");
                        }
                        else
                        {
                            metrics.Append($"]{metric.CharacterMapping}[{metric.CharacterWidth}");
                        }
                        previousMetric = metric;
                    }
                    metrics.Append($"]]");
                }

                var content = ByteContainerFactory
                    .CreateByteContainer($"<<{StringConstants.NewLine}" +
                                         $"/Type /Font{StringConstants.NewLine}" +
                                         $"/BaseFont /{fontDefinition.FontName}{StringConstants.NewLine}" +
                                         $"/CIDSystemInfo <<{StringConstants.NewLine}" +
                                         $"/Ordering (Identity){StringConstants.NewLine}" +
                                         $"/Registry (Adobe){StringConstants.NewLine}" +
                                         $"/Supplement 0{StringConstants.NewLine}" +
                                         $">>{StringConstants.NewLine}" +
                                         $"/CIDToGIDMap /Identity{StringConstants.NewLine}" +
                                         $"/FontDescriptor {fontDescriptor.Number} 0 R{StringConstants.NewLine}" +
                                         $"/Subtype /CIDFontType2{StringConstants.NewLine}" +
                                         $"/DW 1000{StringConstants.NewLine}" +
                                         $"{metrics}{StringConstants.NewLine}" +
                                         $">>");

                SetContent(content);

                return base.Content;
            }
        }
    }
}