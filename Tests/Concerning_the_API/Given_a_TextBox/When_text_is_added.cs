using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_TextBox
{
    [TestFixture]
    public class When_text_is_added : BaseTest
    {
        private TextBox _sut;
        private string _addedText;

        public override void Arrange()
        {
            var document = new Document();
            _sut = document.CreateTextBox(new Rectangle(new Point(50, 50), new Size(50, 50)));
            _sut.SetFont(new FontProperties { Name = "Courier", Size = 10 });
            _addedText = "Added/text";
        }

        public override void Act()
        {
            _sut.AddText(_addedText);
        }

        [Test]
        public void It_should_contain_that_text()
        {
            Assert.IsTrue(_sut.Content.ToString().Contains(_addedText), _addedText + " not found in textbox");
        }
    }
}