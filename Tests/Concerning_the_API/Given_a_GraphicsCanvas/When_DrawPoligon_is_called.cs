using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;

namespace Tests.Concerning_the_API.Given_a_GraphicsCanvas
{
    [TestFixture]
    public class When_DrawPoligon_is_called : BaseTest
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

            _sut.SetStrokeColor(Color.Blue);
            _sut.SetFillColor(Color.Orange);
        }

        public override void Act()
        {
            _sut.DrawPoligon(new Point(50, 50), new Point(75, 100), new Point(100, 75));

            _sut.DrawClosedPoligon(new Point(50, 150), new Point(75, 200), new Point(100, 175));
        }

        [Test]
        public void It_should_draw_a_circle()
        {
            var generatedBytes = _document.Generate();

            //DumpToRandomFile(generatedBytes, "pdf");

            var content = new ByteArrayByteContainer(generatedBytes);
            Assert.IsNotNull(content);
        }
    }
}