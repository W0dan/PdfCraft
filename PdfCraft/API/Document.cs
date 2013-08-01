using System.Collections.Generic;
using System.Drawing;
using System.IO;
using PdfCraft.Contents.Graphics;
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
        private readonly XObjects _xObjects;

        public Document()
        {
            _catalog = new CatalogObject(GetNextObjectNumber());
            _pages = new PagesObject(GetNextObjectNumber());
            _catalog.AddPages(_pages);
            _fonts = new Fonts.Fonts();
            _xObjects = new XObjects();
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

        internal FontObject AddFont(string name)
        {
            return _fonts.AddFont(name, GetNextObjectNumber);
        }

        internal Fonts.Fonts Fonts
        {
            get { return _fonts; }
        }

        internal XObject AddXObject(ImageType imageType, string sourceFile)
        {
            return _xObjects.AddXObject(imageType, sourceFile, GetNextObjectNumber);
        }

        internal XObject AddXObject(ImageType imageType, Stream sourceStream)
        {
            return _xObjects.AddXObject(imageType, sourceStream, GetNextObjectNumber);
        }

        public Page AddPage(int width = 210, int height = 297)
        {
            var contents = new ContentsObject(GetNextObjectNumber());
            var page = new PageObject(GetNextObjectNumber(), new Size(width, height));

            page.AddContents(contents);
            _pages.AddPage(page);
            _pageObjects.Add(page);

            return new Page(page);
        }

        public byte[] Generate()
        {
            var generator = new PdfGenerator();

            generator.AddCatalog(_catalog);
            generator.AddObject(_pages);

            foreach (var fontObject in _fonts.ToDictionary())
            {
                generator.AddObject(fontObject.Value);
                generator.AddObject(fontObject.Value.FontDescriptor);
                generator.AddObject(fontObject.Value.FontWidths);
            }

            foreach (var xObject in _xObjects.ToDictionary())
            {
                generator.AddObject(xObject.Value);
            }

            foreach (var pageObject in _pageObjects)
            {
                generator.AddObject(pageObject.Contents);
                generator.AddObject(pageObject);
            }

            return generator.GetBytes();
        }
    }
}