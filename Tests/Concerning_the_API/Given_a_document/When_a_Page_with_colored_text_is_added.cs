using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_document
{
    [TestFixture]
    public class When_a_Page_with_colored_text_is_added : BaseTest
    {
        private Document _sut;
        private TextBox _textbox;

        public override void Arrange()
        {
            _sut = new Document();
            _textbox = _sut.CreateTextBox(new Rectangle(new Point(50, 50), new Size(50, 50)));
            _textbox.SetColor(Color.Black);
            _textbox.SetFont(new FontProperties("Helvetica", 10));
            _textbox.AddText("dit is een testje dat iets langer is.");
            _textbox.SetColor(Color.Red);
            _textbox.AddText(" dit is rode tekst");
            _textbox.SetColor(Color.Green);
            _textbox.AddText(" dit is groene tekst");
        }

        public override void Act()
        {
            var page = _sut.AddPage();

            page.AddTextBox(_textbox);
        }

        [Test]
        public void It_should_render_a_PDF()
        {
            var generatedBytes = _sut.Generate();

            var content = new ByteArrayByteContainer(generatedBytes);
            Assert.IsNotNull(content);
        }
    }
}