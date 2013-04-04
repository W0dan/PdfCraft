using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_document
{
    public class When_Superscript_is_used : BaseTest
    {
        private TextBox _sut;
        private Document _document;
        private Page _page;

        public override void Arrange()
        {
            _document = new Document();
            _page = _document.AddPage();
            _sut = _document.CreateTextBox(new Rectangle(33, 12, 155, 255));

            _sut.SetFont(new FontProperties("Helvetica", 10));

            _page.AddTextBox(_sut);
        }

        public override void Act()
        {
            _sut.AddText("normal text");
            _sut.SetSuperscriptOn();
            _sut.AddText("superscripted text");
            _sut.SetSuperscriptOff();
            _sut.AddText(" normal text again");
        }

        [Test]
        public void It_should_render_a_pdf()
        {
            var test = new TestExecutor(this);

            test.Assert(() =>
            {
                var generatedBytes = _document.Generate();

                //DumpToRandomFile(generatedBytes, "pdf");

                var content = new ByteArrayByteContainer(generatedBytes);
                Assert.IsNotNull(content);
            });
        }
    }
}