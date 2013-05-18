using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;
using PdfCraft.Contents.Graphics;

namespace Tests.Concerning_the_API.Given_a_GraphicsCanvas
{
    [TestFixture]
    public class When_DrawImage_is_called_with_a_stream : BaseTest
    {
        private Document _document;
        private GraphicsCanvas _sut;
        private Page _page;

        public override void Arrange()
        {
            _document = new Document();
            _page = _document.AddPage();

            _sut = _document.CreateCanvas();
            _page.AddCanvas(_sut);
        }

        public override void Act()
        {
            var image = GetType().Assembly
                     .GetManifestResourceStream("Tests.Concerning_the_API.Given_a_GraphicsCanvas.image.jpg");

            _sut.DrawImage(new Point(75, 50), new Size(25, 75), ImageType.Jpg, image);
            _sut.DrawImage(new Point(50, 75), new Size(75, 25), ImageType.Jpg, image);

            var image1 = GetType().Assembly
                    .GetManifestResourceStream("Tests.Concerning_the_API.Given_a_GraphicsCanvas.image1.jpg");

            _sut.DrawImage(new Point(50, 175), new Size(75, 25), ImageType.Jpg, image1);
        }

        [Test]
        public void It_should_draw_an_image()
        {
            var generatedBytes = _document.Generate();

            //DumpToRandomFile(generatedBytes, "pdf");

            var content = new ByteArrayByteContainer(generatedBytes);
            Assert.IsNotNull(content);
        }
    }
}