using NUnit.Framework;
using PdfCraft.Fonts;
using PdfCraft.Fonts.Standard14;

namespace Tests.Concerning_Fonts.Given_a_FontObject
{
    [TestFixture]
    public class When_it_is_created_with_valid_info : BaseTest
    {
        private Standard14FontObject sut;
        private int objectNumber;
        private string fontName;
        private string baseFontname;
        private FontProperties fontProperties;
        private Standard14FontWidths fontWidths;
        private Standard14FontDescriptor fontDescriptor;

        public override void Arrange()
        {
            objectNumber = 248;
            fontName = "/F0";
            baseFontname = "Helvetica";
            fontProperties = new FontProperties(baseFontname, size: 10);
        }

        public override void Act()
        {
            sut = new Standard14FontObject(objectNumber, fontName, fontProperties.Name);
            fontWidths = new Standard14FontWidths(521, sut);
            sut.SetFontWidths(fontWidths);
            fontDescriptor = new Standard14FontDescriptor(621, sut);
            sut.SetFontDescriptor(fontDescriptor);
        }

        [Test]
        public void It_should_render_a_PdfFontObject_with_the_fontName_and_the_name_of_the_baseFont()
        {
            const string format = "{4} 0 obj\r\n<< /Type /Font /Subtype /{0} /Name {1} /BaseFont /{2} /Encoding /{3} /Widths {5} 0 R /FirstChar 0 /LastChar 255 /FontDescriptor {6} 0 R >>\r\nendobj\r\n";
            var expectedValue = string.Format(format, "Type1", fontName, fontProperties.Name, "WinAnsiEncoding", objectNumber, fontWidths.Number, fontDescriptor.Number);
            Assert.AreEqual(expectedValue, sut.Content.ToString());
        }
    }
}