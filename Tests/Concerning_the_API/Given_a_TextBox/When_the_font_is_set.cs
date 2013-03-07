using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_TextBox
{
    public class When_the_font_is_set : BaseTest
    {
        private TextBox _sut;
        private FontProperties _properties;
        private Document _document;
        private int _fontsize;

        public override void Arrange()
        {
            _document = new Document();
            _sut = _document.CreateTextBox(new Rectangle(new Point(50, 50), new Size(50, 50)));

            _fontsize = 10;
            _properties = new FontProperties { Name = "Arial", Size = _fontsize };
        }

        public override void Act()
        {
            _sut.SetFont(_properties);
            _sut.AddText("test");
        }

        [Test]
        public void It_should_contain_the_pdf_code_to_set_the_font()
        {
            var test = new TestExecutor(this);

            test.Assert(() =>
                {
                    var font = _document.Fonts.ToDictionary()[_properties.GetHashCode()];
                    var expectedValue = string.Format("{0} {1} Tf", font.FontName, _fontsize);

                    Assert.IsTrue(_sut.Content.Contains(expectedValue), expectedValue + " not found in TextBox");
                });
        }
    }
}