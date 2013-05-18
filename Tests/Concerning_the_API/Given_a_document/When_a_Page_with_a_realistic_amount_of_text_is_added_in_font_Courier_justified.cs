using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_document
{
    [TestFixture]
    public class When_a_Page_with_a_realistic_amount_of_text_is_added_in_font_Courier_justified : BaseTest
    {
        private Document _sut;
        private TextBox _title;
        private TextBox _textbox1;
        private TextBox _textbox2;
        private GraphicsCanvas _canvas1;
        private GraphicsCanvas _canvas2;

        public override void Arrange()
        {
            _sut = new Document();
            _title = _sut.CreateTextBox(new Rectangle(new Point(20, 20), new Size(170, 10)));
            _title.SetColor(Color.Black);
            _title.SetFont(new FontProperties { Name = "Courier", Size = 20 });
            _title.AddText("The select Model");

            _canvas1 = _sut.CreateCanvas();
            _canvas1.SetFillColor(Color.Azure);
            _canvas1.SetStrokeColor(Color.Black);

            var location = new Point(20, 45);
            var size = new Size(170, 110);
            _canvas1.DrawBox(location, size);
            _textbox1 = CreateTextbox(location, size, 10);

            _canvas2 = _sut.CreateCanvas();
            _canvas2.SetFillColor(Color.Azure);
            _canvas2.SetStrokeColor(Color.Black);

            _canvas2.DrawBox(new Point(20, 20), new Size(170, 115));
            _textbox2 = CreateTextbox(new Point(20, 20), new Size(170, 115), 20);
        }

        private TextBox CreateTextbox(Point location, Size size, int fontSize)
        {
            var textbox = _sut.CreateTextBox(new Rectangle(location, size));
            textbox.SetAlignment(TextAlignment.Justify);
            textbox.SetFont(new FontProperties { Name = "Courier", Size = fontSize });
            var realisticText_pt1 = "The select Model is the most widely available I/O model in Winsock. " +
                                    "We call it the select model because it centers on using the select function to " +
                                    "manage I/O. The design of this model originated on Unix-based computers featuring " +
                                    "Berkeley socket implementations. The select model was incorporated into Winsock 1.1 " +
                                    "to allow applications that want to avoid blocking on socket calls the ability to manage " +
                                    "multiple sockets in an organized manner. Because Winsock 1.1 is backward-compatible " +
                                    "with Berkeley socket implementations, a Berkeley socket application that uses the select " +
                                    "function should ";
            textbox.AddText(realisticText_pt1);

            textbox.SetColor(Color.Green);
            textbox.AddText("technically");

            textbox.SetColor(Color.Black);
            var realisticText_pt3 = " be able to run without modification.\n" +
                                    "The select function can be used to determine whether there is data on a socket and " +
                                    "whether a socket can be written to. The whole reason for having this function is to " +
                                    "prevent your application from blocking on an I/O bound call such as send or recv when " +
                                    "a socket is in a blocking mode and to prevent the ";
            textbox.AddText(realisticText_pt3);

            textbox.SetFont(new FontProperties { Name = "Courier", Size = 15 });
            textbox.AddText("WSAEWOULDBLOCK");

            textbox.SetFont(new FontProperties { Name = "Courier", Size = fontSize });
            var realisticText_pt4 = " error when a socket " +
                                    "is in nonblocking mode. The select function blocks for I/O opperations until the " +
                                    "conditions specified as parameters are met. The function prototype for select is " +
                                    "as follows:\n" +
                                    "...\n";
            textbox.AddText(realisticText_pt4);

            return textbox;
        }

        public override void Act()
        {
            var page = _sut.AddPage();

            page.AddTextBox(_title);
            page.AddCanvas(_canvas1);
            page.AddTextBox(_textbox1);

            var page2 = _sut.AddPage();
            page2.AddCanvas(_canvas2);
            page2.AddTextBox(_textbox2);
        }

        [Test]
        public void It_should_render_a_PDF()
        {
            var generatedBytes = _sut.Generate();

            //DumpToRandomFile(generatedBytes, "pdf");

            var content = new ByteArrayByteContainer(generatedBytes);
            Assert.IsNotNull(content);
        }
    }
}