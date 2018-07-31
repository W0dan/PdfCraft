using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_document
{
    [TestFixture]
    public class When_a_Page_with_text_and_graphics_is_added : BaseTest
    {
        private Document _sut;
        private TextBox _textbox;
        private GraphicsCanvas _canvas;

        public override void Arrange()
        {
            _sut = new Document();
            _textbox = _sut.CreateTextBox(new Rectangle(new Point(50, 50), new Size(50, 50)));
            _textbox.SetColor(Color.Blue);
            _textbox.SetFont(new FontProperties ("Helvetica", 10 ));
            _textbox.AddText("dit is een testje dat iets langer is.");

            _canvas = _sut.CreateCanvas();
            _canvas.SetFillColor(Color.Black);
            _canvas.SetStrokeColor(Color.Black);
            _canvas.DrawLine(new Point(0, 0), new Point(50, 50));
        }

        public override void Act()
        {
            var page = _sut.AddPage();

            page.AddTextBox(_textbox);
            page.AddCanvas(_canvas);
        }

        [Test]
        public void It_should_render_a_PDF()
        {
            var generatedBytes = _sut.Generate();

            //DumpToRandomFile(generatedBytes, "pdf");

            var content = new ByteArrayByteContainer(generatedBytes);
            Assert.IsNotNull(content);
        }
    }
}