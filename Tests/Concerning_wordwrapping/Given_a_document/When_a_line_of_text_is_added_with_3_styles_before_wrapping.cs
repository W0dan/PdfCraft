using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;
using PdfCraft.Fonts;

namespace Tests.Concerning_wordwrapping.Given_a_document
{
    [TestFixture]
    public class When_a_line_of_text_is_added_with_3_styles_before_wrapping : BaseTest
    {
        private Document _sut;
        private GraphicsCanvas _canvas1;
        private TextBox _textbox1;
        private TextBox _textbox2;

        public override void Arrange()
        {
            _sut = new Document();

            _canvas1 = _sut.CreateCanvas();
            _canvas1.SetFillColor(Color.Azure);
            _canvas1.SetStrokeColor(Color.Black);

            var location = new Point(20, 45);
            var size = new Size(100, 50);
            _canvas1.DrawBox(location, size);

            for (var i = 30; i < 120; i += 10)
            {
                DrawVerticalRuler(i);
            }

            _textbox1 = CreateTextbox(location, size, 10, TextAlignment.Justify);
            location = new Point(20, 80);
            _textbox2 = CreateTextbox(location, size, 10, TextAlignment.Left);

        }

        private void DrawVerticalRuler(int xPosition)
        {
            _canvas1.DrawLine(new Point(xPosition, 30), new Point(xPosition, 200));
        }

        private TextBox CreateTextbox(Point location, Size size, int fontSize, TextAlignment textAlignment)
        {
            var textbox = _sut.CreateTextBox(new Rectangle(location, size));
            textbox.SetAlignment(textAlignment);
            textbox.SetFont(new FontProperties { Name = "Courier", Size = fontSize });
            var realisticText_pt1 = "function should ";
            textbox.AddText(realisticText_pt1);

            textbox.SetColor(Color.Green);
            textbox.AddText("technically");

            textbox.SetColor(Color.Black);
            var realisticText_pt3 = " be able to run without modification.";
            textbox.AddText(realisticText_pt3);

            return textbox;
        }

        public override void Act()
        {
            var page = _sut.AddPage();

            page.AddCanvas(_canvas1);
            page.AddTextBox(_textbox1);

            page.AddTextBox(_textbox2);
        }

        [Test]
        public void It_should_render_correctly()
        {
            var generatedBytes = _sut.Generate();

            //DumpToRandomFile(generatedBytes, "pdf");

            var content = new ByteArrayByteContainer(generatedBytes);
            Assert.IsNotNull(content);
        }
    }
}