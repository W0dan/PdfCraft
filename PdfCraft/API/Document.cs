using System.Collections.Generic;
using System.Drawing;
using PdfCraft.Constants;
using PdfCraft.Containers;
using PdfCraft.Fonts;

namespace PdfCraft.API
{
    public class Document
    {
        private int _nextObjectNumber = 1;
        private readonly Fonts.Fonts _fonts;
        private readonly CatalogObject _catalog;
        private readonly PagesObject _pages;
        private readonly List<PageObject> _pageObjects = new List<PageObject>();

        public Document()
        {
            _catalog = new CatalogObject(GetNextObjectNumber());
            _pages = new PagesObject(GetNextObjectNumber());
            _catalog.AddPages(_pages);
            _fonts = new Fonts.Fonts();
        }

        public TextBox CreateTextBox(Rectangle sizeAndPosition)
        {
            return new TextBox(sizeAndPosition, this);
        }

        public GraphicsCanvas CreateCanvas()
        {
            return new GraphicsCanvas(this);
        }

        internal int GetNextObjectNumber()
        {
            return _nextObjectNumber++;
        }

        internal FontObject AddFont(string properties)
        {
            return _fonts.AddFont(properties, GetNextObjectNumber);
        }

        internal Fonts.Fonts Fonts
        {
            get { return _fonts; }
        }

        public Page AddPage()
        {
            var contents = new ContentsObject(GetNextObjectNumber());
            var page = new PageObject(GetNextObjectNumber(), new Size(210, 297));

            page.AddContents(contents);
            _pages.AddPage(page);
            _pageObjects.Add(page);

            return new Page(page);
        }

        public byte[] Generate()
        {
            var xref = new List<string>();

            xref.Add("0000000000 65535 f" + StringConstants.NewLine);
            var content = ByteContainerFactory.CreateByteContainer("%PDF-1.7" + StringConstants.NewLine);
            var offset = 9;

            xref.Add(string.Format("{0:0000000000} 00000 n", offset) + StringConstants.NewLine);
            content.Append(_catalog.Content);
            offset += _catalog.Length;

            xref.Add(string.Format("{0:0000000000} 00000 n", offset) + StringConstants.NewLine);
            content.Append(_pages.Content);
            offset += _pages.Length;

            foreach (var fontObject in _fonts.ToDictionary())
            {
                xref.Add(string.Format("{0:0000000000} 00000 n", offset) + StringConstants.NewLine);
                content.Append(fontObject.Value.Content);
                offset += fontObject.Value.Length;

                xref.Add(string.Format("{0:0000000000} 00000 n", offset) + StringConstants.NewLine);
                content.Append(fontObject.Value.FontWidths.Content);
                offset += fontObject.Value.FontWidths.Length;

                xref.Add(string.Format("{0:0000000000} 00000 n", offset) + StringConstants.NewLine);
                content.Append(fontObject.Value.FontDescriptor.Content);
                offset += fontObject.Value.FontDescriptor.Length;
            }
            foreach (var pageObject in _pageObjects)
            {
                xref.Add(string.Format("{0:0000000000} 00000 n", offset) + StringConstants.NewLine);
                content.Append(pageObject.Contents.Content);
                offset += pageObject.Contents.Length;

                xref.Add(string.Format("{0:0000000000} 00000 n", offset) + StringConstants.NewLine);
                content.Append(pageObject.Content);
                offset += pageObject.Length;
            }

            content.Append("xref" + StringConstants.NewLine);
            content.Append(string.Format("0 {0}", xref.Count) + StringConstants.NewLine);
            foreach (var xrefEntry in xref)
                content.Append(xrefEntry);

            content.Append("trailer" + StringConstants.NewLine);
            content.Append(string.Format("<< /Size {0}", xref.Count) + StringConstants.NewLine);
            content.Append(string.Format("/Root {0} 0 R", _catalog.Number) + StringConstants.NewLine);
            content.Append(">>" + StringConstants.NewLine);
            content.Append("startxref" + StringConstants.NewLine);
            content.Append(string.Format("{0}", offset) + StringConstants.NewLine);
            content.Append("%%EOF");

            return content.GetBytes();
        }
    }
}