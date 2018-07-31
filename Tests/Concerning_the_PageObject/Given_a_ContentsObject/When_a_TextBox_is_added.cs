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
        private ContentsObject sut;
        private TextBox textbox;
        private Document document;
        private FontProperties fontProperties;

        public override void Arrange()
        {
            document = new Document();
            textbox = document.CreateTextBox(new Rectangle(new Point(50, 50), new Size(50, 50)));

            fontProperties = new FontProperties("Arial", 10);
            textbox.SetFont(fontProperties);

            sut = new ContentsObject(246);
        }

        public override void Act()
        {
            sut.AddTextBox(textbox);
        }

        [Test]
        public void It_should_add_the__fonts_used__to_the_document()
        {
            Assert.IsTrue(document.FontFactory.ToDictionary().ContainsKey(fontProperties.GetHashCode()));
        }
    }
}