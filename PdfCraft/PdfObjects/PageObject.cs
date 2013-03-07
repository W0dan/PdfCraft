using System.Drawing;
using PdfCraft.API;
using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft
{
    public class PageObject : BasePdfObject
    {
        private int _parentObjectNumber;
        private Size _size;
        private ContentsObject _contents;

        public PageObject(int objectNumber, Size size)
            : base(objectNumber)
        {
            _size = new Size((int)(size.Width * 2.54), (int)(size.Height * 2.54));
        }

        public void AddContents(ContentsObject contents)
        {
            _contents = contents;
        }

        public void AddTextBox(TextBox textbox)
        {
            var newY = _size.Height - textbox.Position.Y;
            textbox.Position = new Point(textbox.Position.X, newY);

            _contents.AddTextBox(textbox);
        }

        public void AddCanvas(GraphicsCanvas canvas)
        {
            canvas.Size = _size;

            _contents.AddCanvas(canvas);
        }

        public ContentsObject Contents
        {
            get { return _contents; }
        }

        public void SetParentObjectNumber(int objectNumber)
        {
            _parentObjectNumber = objectNumber;
        }

        public override IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory
                    .CreateByteContainer(string.Format("<< /Type /Page\r\n/Parent {0} 0 R", _parentObjectNumber) + StringConstants.NewLine);

                content.Append(string.Format("/MediaBox [0 0 {0} {1}]", _size.Width, _size.Height) + StringConstants.NewLine);
                content.Append(string.Format("/Contents {0} 0 R", _contents.Number) + StringConstants.NewLine);

                content.Append("/Resources << /ProcSet [/PDF /Text /ImageC] /Font << ");

                foreach (var fontname in _contents.GetFontnames())
                    content.Append(fontname);

                content.Append(">> >> >>");

                SetContent(content);

                return base.Content;
            }
        }
    }
}