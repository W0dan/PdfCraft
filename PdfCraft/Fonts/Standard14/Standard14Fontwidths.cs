using System.Resources;
using PdfCraft.API;
using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft.Fonts.Standard14
{
    internal class Standard14FontWidths : BasePdfObject, IFontWidths
    {
        public Standard14FontWidths(int objectNumber, FontObject font)
            : base(objectNumber)
        {
            var fontWidths = new ResourceManager("PdfCraft.Fonts.fontwidths", typeof(Document).Assembly);

            var isBold = font.Name.Contains("Bold");

            //todo: fontwidths for all type 1 fonts in this manner
            if (font.Name.StartsWith("Helvetica"))
            {
                for (var i = 0; i < 256; i++)
                {
                    var name = "Helvetica" + "-" + i.ToString("000");
                    var dictWidth = fontWidths.GetString(name);
                    var width = string.IsNullOrEmpty(dictWidth) ? 0 : int.Parse(dictWidth);

                    if (isBold)
                        width = (int)(width * 1.1);

                    Widths[i] = width;
                }
            }
            else if (font.Name.StartsWith("Courier"))
            {
                for (var i = 0; i < 256; i++)
                {
                    Widths[i] = 610;
                }
            }
        }

        public int[] Widths { get; } = new int[256];

        public override IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory.CreateByteContainer("[ ");

                var column = 1;
                foreach (var width in Widths)
                {
                    content.Append(width.ToString("0"));

                    if (column > 15)
                    {
                        content.Append(StringConstants.NewLine);
                        column = 1;
                    }
                    else
                    {
                        content.Append(" ");
                        column++;
                    }
                }

                content.Append("]");

                SetContent(content);

                return base.Content;
            }
        }
    }
}