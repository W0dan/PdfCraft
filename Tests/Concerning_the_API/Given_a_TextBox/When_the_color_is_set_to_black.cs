using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_TextBox
{
    public class When_the_color_is_set_to_black : BaseTest
    {
        private TextBox _sut;
        private Document _document;
        private Color _color;

        public override void Arrange()
        {
            _document = new Document();
            _sut = _document.CreateTextBox(new Rectangle(new Point(50, 50), new Size(50, 50)));
        }

        public override void Act()
        {
            _color = Color.Black;
            _sut.SetColor(_color);
            _sut.SetFont(new FontProperties { Name = "Helvetica", Size = 10 });
            _sut.AddText("test");
        }

        [Test]
        public void It_should_contain_the_pdf_code_to_set_the_color()
        {
            var test = new TestExecutor(this);

            test.Assert(() =>
            {
                var expectedValue = string.Format("{0} {1} {2} rg", _color.R, _color.G, _color.B);

                Assert.IsTrue(_sut.Content.Contains(expectedValue), expectedValue + " not found in TextBox");
            });
        }
    }
}