using System.Collections.Generic;
using System.Drawing;
using System.IO;
using PdfCraft.Contents.Graphics;
using PdfCraft.Fonts;

namespace PdfCraft.API
{
    public class Document
    {
        private int nextObjectNumber = 1;
        internal readonly FontFactory FontFactory;
        private readonly CatalogObject catalog;
        private readonly PagesObject pages;
        private readonly List<PageObject> pageObjects = new List<PageObject>();
        private readonly XObjects xObjects;

        public Document()
        {
            catalog = new CatalogObject(GetNextObjectNumber());
            pages = new PagesObject(GetNextObjectNumber());
            catalog.AddPages(pages);
            FontFactory = new FontFactory();
            xObjects = new XObjects();
        }

        public TextBox CreateTextBox(Rectangle sizeAndPosition)
        {
            return new TextBox(sizeAndPosition, this);
        }

        public GraphicsCanvas CreateCanvas()
        {
            return new GraphicsCanvas(this);
        }

        private int GetNextObjectNumber()
        {
            return nextObjectNumber++;
        }

        internal FontObject AddFont(string name, HashSet<FontStyle> fontStyles, FontProperties properties)
        {
            return FontFactory.AddFont(name, GetNextObjectNumber, fontStyles, properties);
        }

        internal XObject AddXObject(ImageType imageType, string sourceFile)
        {
            return xObjects.AddXObject(imageType, sourceFile, GetNextObjectNumber);
        }

        internal XObject AddXObject(ImageType imageType, Stream sourceStream)
        {
            return xObjects.AddXObject(imageType, sourceStream, GetNextObjectNumber);
        }

        public Page AddPage(int width = 210, int height = 297)
        {
            var contents = new ContentsObject(GetNextObjectNumber());
            var page = new PageObject(GetNextObjectNumber(), new Size(width, height));

            page.AddContents(contents);
            pages.AddPage(page);
            pageObjects.Add(page);

            return new Page(page);
        }

        public byte[] Generate()
        {
            var generator = new PdfGenerator();

            generator.AddCatalog(catalog);
            generator.AddObject(pages);

            foreach (var xObject in xObjects.ToDictionary())
            {
                generator.AddObject(xObject.Value);
            }

            foreach (var pageObject in pageObjects)
            {
                generator.AddObject(pageObject.Contents);
                generator.AddObject(pageObject);
            }

            foreach (var fontObject in FontFactory.ToDictionary())
            {
                generator.AddObject(fontObject.Value);
            }

            return generator.GetBytes();
        }
    }
}