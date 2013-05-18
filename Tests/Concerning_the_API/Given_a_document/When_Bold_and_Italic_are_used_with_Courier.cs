using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_document
{
    [TestFixture]
    public class When_Bold_and_Italic_are_used_with_Courier : BaseTest
    {
        private Document _document;
        private TextBox _sut;
        private Page _page;

        public override void Arrange()
        {
            _document = new Document();
            _page = _document.AddPage();
            _sut = _document.CreateTextBox(new Rectangle(33, 12, 155, 255));
            var canvas = _document.CreateCanvas();
            canvas.SetStrokeColor(Color.Black);
            canvas.SetFillColor(Color.White);
            canvas.DrawBox(new Rectangle(33, 12, 155, 255));
            _page.AddCanvas(canvas);
        }

        public override void Act()
        {
            _sut.SetFont(new FontProperties("Courier", 10));
            _sut.SetAlignment(TextAlignment.Justify);

            for (var i = 0; i < 100; i++)
            {
                AddABunchOfText();
            }

            _page.AddTextBox(_sut);
        }

        private void AddABunchOfText()
        {
            _sut.AddText("non bold, non italic ");
            _sut.SetBoldOn();
            _sut.AddText("bold, non italic ");
            _sut.SetItalicOn();
            _sut.AddText("bold, italic ");
            _sut.SetItalicOff();
            _sut.AddText("bold, non italic ");
            _sut.SetBoldOff();
            _sut.AddText("non bold, non italic ");
        }

        [Test]
        public void It_should_create_the_PDF()
        {
            var generatedBytes = _document.Generate();

            //DumpToRandomFile(generatedBytes, "pdf");

            var content = new ByteArrayByteContainer(generatedBytes);
            Assert.IsNotNull(content);
        }
    }
}