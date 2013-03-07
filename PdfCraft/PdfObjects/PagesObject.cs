using System.Collections.Generic;
using System.Drawing;
using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft
{
    public class PagesObject : BasePdfObject
    {
        private readonly List<int> _kids = new List<int>();

        public PagesObject(int objectNumber)
            : base(objectNumber)
        {
        }

        public void AddPage(PageObject page)
        {
            page.SetParentObjectNumber(Number);
            _kids.Add(page.Number);
        }

        public override IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory
                    .CreateByteContainer("<< /Type /Pages" + StringConstants.NewLine);
                content.Append("/Kids [");

                foreach (var pageObjectNumber in _kids)
                    content.Append(string.Format("{0} 0 R ", pageObjectNumber));

                content.Append("]" + StringConstants.NewLine);
                content.Append(string.Format("/Count {0}", _kids.Count)+StringConstants.NewLine);
                content.Append(">>");

                SetContent(content);

                return base.Content;
            }
        }
    }
}