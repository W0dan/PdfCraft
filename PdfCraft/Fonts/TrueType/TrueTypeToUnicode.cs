using System.Text;
using PdfCraft.Constants;
using PdfCraft.Containers;
using PdfCraft.Extensions;

namespace PdfCraft.Fonts.TrueType
{
    public class TrueTypeToUnicode : BasePdfObject
    {
        private readonly TrueTypeFontObject parent;

        public TrueTypeToUnicode(int objectNumber, TrueTypeFontObject parent) : base(objectNumber)
        {
            this.parent = parent;
        }

        public override IByteContainer Content
        {
            get
            {
                var usedCharacters = parent.UsedCharacters;

                var hexContent = new StringBuilder();

                hexContent.Append($"/CIDInit /ProcSet findresource begin{StringConstants.NewLine}");
                hexContent.Append($"12 dict begin{StringConstants.NewLine}");
                hexContent.Append($"begincmap{StringConstants.NewLine}");
                hexContent.Append($"/CIDSystemInfo{StringConstants.NewLine}");
                hexContent.Append($"<< /Registry (Adobe){StringConstants.NewLine}");
                hexContent.Append($"/Ordering (UCS){StringConstants.NewLine}");
                hexContent.Append($"/Supplement 0{StringConstants.NewLine}");
                hexContent.Append($">> def{StringConstants.NewLine}");
                hexContent.Append($"/CMapName /Adobe-Identity-UCS def{StringConstants.NewLine}");
                hexContent.Append($"/CMapType 2 def{StringConstants.NewLine}");
                hexContent.Append($"1 begincodespacerange{StringConstants.NewLine}");
                if (usedCharacters.Count > 1)
                {
                    hexContent.Append($"<{usedCharacters[0].Metric.CharacterMapping.ToString("X4").ToLower()}>" +
                                      $"<{usedCharacters[usedCharacters.Count - 1].Metric.CharacterMapping.ToString("X4").ToLower()}>{StringConstants.NewLine}");
                }
                else
                {
                    hexContent.Append($"<>{StringConstants.NewLine}");
                }
                hexContent.Append($"endcodespacerange{StringConstants.NewLine}");
                hexContent.Append($"{usedCharacters.Count} beginbfrange{StringConstants.NewLine}");
                if (usedCharacters.Count > 0)
                {
                    foreach (var usedChar in usedCharacters)
                    {
                        hexContent.Append($"<{usedChar.Metric.CharacterMapping.ToString("X4").ToLower()}>" +
                                          $"<{usedChar.Metric.CharacterMapping.ToString("X4").ToLower()}>" +
                                          $"<{usedChar.Char.ToString("X4").ToLower()}>{StringConstants.NewLine}");
                    }
                }
                hexContent.Append($"endbfrange{StringConstants.NewLine}");
                hexContent.Append($"endcmap{StringConstants.NewLine}");
                hexContent.Append($"CMapName currentdict /CMap defineresource pop{StringConstants.NewLine}");
                hexContent.Append("end end");

                var content = ByteContainerFactory
                    .CreateByteContainer($"<<{StringConstants.NewLine}" +
                                         $"/Filter /ASCIIHexDecode{StringConstants.NewLine}" +
                                         $"/Length {hexContent.Length}{StringConstants.NewLine}" +
                                         $">>{StringConstants.NewLine}" +
                                         $"stream{StringConstants.NewLine}" +
                                         $"{hexContent.ToString().ToHex()}>{StringConstants.NewLine}" +
                                         $"endstream");

                SetContent(content);

                return base.Content;
            }
        }
    }
}