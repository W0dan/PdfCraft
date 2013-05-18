using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Containers;
using PdfCraft.Fonts;

namespace Tests.Concerning_the_API.Given_a_document
{
    [TestFixture]
    public class When_a_Page_with_a_realistic_amount_of_text_is_added_in_font_courier_right_aligned : BaseTest
    {
        private Document _sut;
        private TextBox _title;
        private TextBox _textbox;

        public override void Arrange()
        {
            _sut = new Document();
            _title = _sut.CreateTextBox(new Rectangle(new Point(20, 20), new Size(170, 10)));
            _title.SetColor(Color.Black);
            _title.SetFont(new FontProperties { Name = "Courier", Size = 20 });
            _title.AddText("The select Model");

            var location = new Point(20, 45);
            var size = new Size(170, 235);
            _textbox = _sut.CreateTextBox(new Rectangle(location, size));
            _textbox.SetAlignment(TextAlignment.Right);
            _textbox.SetFont(new FontProperties { Name = "Courier", Size = 10 });
            var realisticText = "The select Model is the most widely available I/O model in Winsock. " +
                                "We call it the select model because it centers on using the select function to " +
                                "manage I/O. The design of this model originated on Unix-based computers featuring " +
                                "Berkeley socket implementations. The select model was incorporated into Winsock 1.1 " +
                                "to allow applications that want to avoid blocking on socket calls the ability to manage " +
                                "multiple sockets in an organized manner. Because Winsock 1.1 is backward-compatible " +
                                "with Berkeley socket implementations, a Berkeley socket application that uses the select " +
                                "function should technically be able to run without modification.\n" +
                                "The select function can be used to determine whether there is data on a socket and " +
                                "whether a socket can be written to. The whole reason for having this function is to " +
                                "prevent your application from blocking on an I/O bound call such as send or recv when " +
                                "a socket is in a blocking mode and to prevent the WSAEWOULDBLOCK error when a socket " +
                                "is in nonblocking mode. The select function blocks for I/O opperations until the " +
                                "conditions specified as parameters are met. The function prototype for select is " +
                                "as follows:\n" +
                                "...";
            _textbox.AddText(realisticText);
        }

        public override void Act()
        {
            var page = _sut.AddPage();

            page.AddTextBox(_title);
            page.AddTextBox(_textbox);
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