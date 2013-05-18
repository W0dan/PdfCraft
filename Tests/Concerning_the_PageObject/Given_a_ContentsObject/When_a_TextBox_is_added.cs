using System.Drawing;
using NUnit.Framework;
using PdfCraft;
using PdfCraft.API;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_PageObject.Given_a_ContentsObject
{
    [TestFixture]
    public class When_a_TextBox_is_added : BaseTest
    {
        private ContentsObject _sut;
        private TextBox _textbox;
        private Document _document;
        private FontProperties _fontProperties;

        public override void Arrange()
        {
            _document = new Document();
            _textbox = _document.CreateTextBox(new Rectangle(new Point(50, 50), new Size(50, 50)));

            _fontProperties = new FontProperties { Name = "Arial" };
            _textbox.SetFont(_fontProperties);

            _sut = new ContentsObject(246);
        }

        public override void Act()
        {
            _sut.AddTextBox(_textbox);
        }

        [Test]
        public void It_should_add_the__fonts_used__to_the_document()
        {
            Assert.IsTrue(_document.Fonts.ToDictionary().ContainsKey(_fontProperties.GetHashCode()));
        }
    }
}