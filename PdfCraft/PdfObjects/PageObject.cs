using System.Drawing;
using PdfCraft.API;
using PdfCraft.Constants;
using PdfCraft.Containers;
using PdfCraft.Extensions;

namespace PdfCraft
{
    public class PageObject : BasePdfObject
    {
        private int _parentObjectNumber;
        private Size _size;

        public PageObject(int objectNumber, Size size)
            : base(objectNumber)
        {
            _size = size.GetSizeInMillimeters();
        }

        public void AddContents(ContentsObject contents)
        {
            Contents = contents;
        }

        public void AddTextBox(TextBox textbox)
        {
            var newY = _size.Height - textbox.Position.Y;
            textbox.Position = new Point(textbox.Position.X, newY);

            Contents.AddTextBox(textbox);
        }

        public void AddCanvas(GraphicsCanvas canvas)
        {
            canvas.Size = _size;

            Contents.AddCanvas(canvas);
        }

        public ContentsObject Contents { get; private set; }

        public void SetParentObjectNumber(int objectNumber)
        {
            _parentObjectNumber = objectNumber;
        }

        public override IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory
                    .CreateByteContainer(
                        $"<< /Type /Page\r\n/Parent {_parentObjectNumber} 0 R{StringConstants.NewLine}");

                content.Append($"/MediaBox [0 0 {_size.Width} {_size.Height}]{StringConstants.NewLine}");
                content.Append($"/Contents {Contents.Number} 0 R{StringConstants.NewLine}");

                content.Append($"/Resources <<{StringConstants.NewLine}");
                content.Append($"/ProcSet [/PDF /Text /ImageC]{StringConstants.NewLine}");

                //fonts
                content.Append("/Font << ");
                foreach (var fontname in Contents.GetFontnames())
                    content.Append(fontname);
                content.Append(">>" + StringConstants.NewLine);

                //xobjects
                content.Append("/XObject << ");
                foreach (var xObjectname in Contents.GetXObjectnames())
                    content.Append(xObjectname);
                content.Append(">>" + StringConstants.NewLine);

                content.Append(">> >>");

                SetContent(content);

                return base.Content;
            }
        }
    }
}