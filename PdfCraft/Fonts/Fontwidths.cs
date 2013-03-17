using System.Resources;
using PdfCraft.API;
using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft.Fonts
{
    internal class Fontwidths : BasePdfObject
    {
        private readonly FontObject _font;
        private readonly int[] _widths = new int[256];

        private readonly ResourceManager _fontWidths;

        public Fontwidths(int objectNumber, FontObject font)
            : base(objectNumber)
        {
            _font = font;

            _fontWidths = new ResourceManager("PdfCraft.Fonts.fontwidths", typeof(Document).Assembly);

            var isBold = _font.Name.Contains("Bold");

            //todo: fontwidths for all type 1 fonts in this manner
            if (_font.Name.StartsWith("Helvetica"))
            {
                for (var i = 0; i < 256; i++)
                {
                    var name = "Helvetica" + "-" + i.ToString("000");
                    var dictWidth = _fontWidths.GetString(name);
                    var width = string.IsNullOrEmpty(dictWidth) ? 0 : int.Parse(dictWidth);

                    if (isBold)
                        width = (int)(width * 1.1);

                    _widths[i] = width;
                }
            }
            else if (_font.Name.StartsWith("Courier"))
            {
                for (var i = 0; i < 256; i++)
                {
                    _widths[i] = 610;
                }
            }
        }

        public int[] Widths { get { return _widths; } }

        public override IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory.CreateByteContainer("[ ");

                var column = 1;
                foreach (var width in _widths)
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