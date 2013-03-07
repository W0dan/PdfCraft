using System.Drawing;
using System.Text;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_document
{
    public class Testpage_Helvetica_Fontwidths : BaseTest
    {
        private Document _sut;
        private TextBox _textbox;
        private GraphicsCanvas _canvas;
        private TextBox _textbox2;

        public override void Arrange()
        {
            _sut = new Document();
            var sizeAndPosition = new Rectangle(10, 10, 190, 277);

            _textbox = _sut.CreateTextBox(sizeAndPosition);
            _textbox.SetColor(Color.Black);
            _textbox.SetAlignment(TextAlignment.Right);
            _textbox.SetFont(new FontProperties("Helvetica", 10));


            var sb = new StringBuilder();
            for (var i = 0; i < 1; i++)
            {
                const string format = "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
                                      "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
                                      "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
                                      "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
                                      "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
                                      "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0}\n";
                var textToAppend = string.Format(format, (char)(i + 65));
                sb.Append(textToAppend);
            }
            sb.Append("\n\n");
            _textbox.AddText(sb.ToString());


            _textbox.SetFont(new FontProperties("Courier", 10));
            sb = new StringBuilder();
            for (var i = 0; i < 1; i++)
            {
                const string format = "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
                                      "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
                                      "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
                                      "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
                                      "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
                                      "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0}\n";
                var textToAppend = string.Format(format, (char)(i + 65));
                sb.Append(textToAppend);
            }
            _textbox.AddText(sb.ToString());


            _textbox2 = _sut.CreateTextBox(sizeAndPosition);
            _textbox2.SetColor(Color.Black);
            _textbox2.SetAlignment(TextAlignment.Right);
            _textbox2.SetFont(new FontProperties("Helvetica", 10));


            //sb = new StringBuilder();
            //for (var i = 0; i < 26; i++)
            //{
            //    const string format = "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
            //                          "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
            //                          "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0}\n";
            //    var textToAppend = string.Format(format, (char)(i + 97));
            //    sb.Append(textToAppend);
            //}
            //sb.Append("\n\n");
            //for (var i = 0; i < 10; i++)
            //{
            //    const string format = "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +
            //                          "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} " +                  
            //                          "{0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0} {0}{0}{0}\n";
            //    var textToAppend = string.Format(format, (char)(i + 48));
            //    sb.Append(textToAppend);
            //}
            //_textbox2.AddText(sb.ToString());


            _canvas = _sut.CreateCanvas();
            _canvas.DrawBox(new Point(10, 10), new Size(190, 277));
        }

        public override void Act()
        {
            var page = _sut.AddPage();
            page.AddCanvas(_canvas);
            page.AddTextBox(_textbox);

            //var page2 = _sut.AddPage();
            //page2.AddCanvas(_canvas);
            //page2.AddTextBox(_textbox2);
        }

        [Test]
        public void Render()
        {
            var test = new TestExecutor(this);

            test.Assert(() =>
            {
                var generatedBytes = _sut.Generate();

                //DumpToRandomFile(generatedBytes, "pdf");

                var content = new ByteArrayByteContainer(generatedBytes);
                Assert.IsNotNull(content);
            });
        }
    }
}