using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_document
{
    [TestFixture]
    public class When_a_Page_with_colored_text_wrapped_is_added : BaseTest
    {
        private Document _sut;
        private TextBox _textbox1;
        private GraphicsCanvas _canvas1;
        private TextBox _textbox2;
        private GraphicsCanvas _canvas2;

        public override void Arrange()
        {
            _sut = new Document();

            var location1 = new Point(50, 25);
            var size1 = new Size(100, 50);
            _textbox1 = CreateTextbox(location1, size1);
            _canvas1 = CreateCanvas(location1, size1);

            var location2 = new Point(50, 100);
            var size2 = new Size(75, 50);
            _textbox2 = CreateTextbox(location2, size2);
            _canvas2 = CreateCanvas(location2, size2);
        }

        private TextBox CreateTextbox(Point location, Size size)
        {
            var textbox = _sut.CreateTextBox(new Rectangle(location, size));
            textbox.SetColor(Color.Black);
            textbox.SetFont(new FontProperties { Name = "Courier", Size = 10 });
            textbox.AddText("dit is een testje dat iets langer is.");
            textbox.SetColor(Color.Red);
            textbox.AddText(" dit is rode tekst");
            textbox.SetColor(Color.Green);
            textbox.AddText(" dit is groene tekst");
            textbox.SetColor(Color.DarkMagenta);
            textbox.AddText(
                " d i t i s t e k s t d i e e x p r e s s h e e l v e e l s p a t i e s b e v a t . . . d i t i s t e k s t d i e e x p r e s s h e e l v e e l s p a t i e s b e v a t . . . d i t i s t e k s t d i e e x p r e s s h e e l v e e l s p a t i e s b e v a t . . . d i t i s t e k s t d i e e x p r e s s h e e l v e e l s p a t i e s b e v a t . . .");

            return textbox;
        }

        private GraphicsCanvas CreateCanvas(Point topLeft, Size size)
        {
            var canvas = _sut.CreateCanvas();
            canvas.SetStrokeColor(Color.DarkOrange);
            canvas.DrawBox(topLeft, size);
            return canvas;
        }

        public override void Act()
        {
            var page = _sut.AddPage();

            page.AddTextBox(_textbox1);
            page.AddCanvas(_canvas1);
            page.AddTextBox(_textbox2);
            page.AddCanvas(_canvas2);
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