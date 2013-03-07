using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;

namespace Tests.Concerning_the_API.Given_a_GraphicsCanvas
{
    public class When_DrawCircle_is_called : BaseTest
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
            _sut.SetFillColor(Color.White);
        }

        public override void Act()
        {
            _sut.DrawCircle(new Point(100, 100), 50);
        }

        [Test]
        public void It_should_draw_a_circle()
        {
            var test = new TestExecutor(this);

            test.Assert(() =>
            {
                var generatedBytes = _document.Generate();

                var content = new ByteArrayByteContainer(generatedBytes);
                Assert.IsNotNull(content);
            });
        }
    }
}