using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_document
{
    [TestFixture]
    public class When_a_Page_with_colored_text_wrapped_is_added2 : BaseTest
    {
        private Document _sut;
        private TextBox _textbox1;
        private GraphicsCanvas _canvas1;
        private TextBox _textbox2;
        private GraphicsCanvas _canvas2;
        private TextBox _textbox3;
        private GraphicsCanvas _canvas3;
        private TextBox _textbox4;
        private GraphicsCanvas _canvas4;
        private TextBox _textbox5;
        private GraphicsCanvas _canvas5;

        public override void Arrange()
        {
            _sut = new Document();
            _textbox1 = CreateTextbox(100, 25);
            _canvas1 = CreateCanvas(100, 25);
            _textbox2 = CreateTextbox(90, 60);
            _canvas2 = CreateCanvas(90, 60);
            _textbox3 = CreateTextbox(80, 95);
            _canvas3 = CreateCanvas(80, 95);
            _textbox4 = CreateTextbox(70, 130);
            _canvas4 = CreateCanvas(70, 130);
            _textbox5 = CreateTextbox(65, 165);
            _canvas5 = CreateCanvas(65, 165);
        }

        private TextBox CreateTextbox(int width, int top)
        {
            var textbox = _sut.CreateTextBox(new Rectangle(new Point(50, top), new Size(width, 30)));
            textbox.SetColor(Color.Black);
            textbox.SetFont(new FontProperties ("Courier", 10 ));
            textbox.AddText("d i t i s t e k s t d i e e x p r e s s h e e l v e e l s p a t i e s");

            return textbox;
        }

        private GraphicsCanvas CreateCanvas(int width, int top)
        {
            var canvas = _sut.CreateCanvas();
            canvas.SetStrokeColor(Color.DarkOrange);
            canvas.DrawBox(new Point(50, top), new Size(width, 30));
            return canvas;
        }

        public override void Act()
        {
            var page = _sut.AddPage();

            page.AddTextBox(_textbox1);
            page.AddTextBox(_textbox2);
            page.AddTextBox(_textbox3);
            page.AddTextBox(_textbox4);
            page.AddTextBox(_textbox5);
            page.AddCanvas(_canvas1);
            page.AddCanvas(_canvas2);
            page.AddCanvas(_canvas3);
            page.AddCanvas(_canvas4);
            page.AddCanvas(_canvas5);
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