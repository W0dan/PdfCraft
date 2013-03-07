using NUnit.Framework;
using PdfCraft.Fonts;

namespace Tests.Concerning_Fonts.Given_a_FontObject
{
    public class When_it_is_created_with_valid_info : BaseTest
    {
        private FontObject _sut;
        private int _objectNumber;
        private string _fontName;
        private string _baseFontname;
        private FontProperties _fontProperties;
        private Fontwidths _fontWidths;
        private FontDescriptor _fontDescriptor;

        public override void Arrange()
        {
            _objectNumber = 248;
            _fontName = "/F0";
            _baseFontname = "Helvetica";
            _fontProperties = new FontProperties { Name = _baseFontname, Size = 10 };
        }

        public override void Act()
        {
            _sut = new FontObject(_objectNumber, _fontName, _fontProperties.Name);
            _fontWidths = new Fontwidths(521, _sut);
            _sut.FontWidths = _fontWidths;
            _fontDescriptor = new FontDescriptor(621, _sut);
            _sut.FontDescriptor = _fontDescriptor;
        }

        [Test]
        public void It_should_render_a_PdfFontObject_with_the_fontName_and_the_name_of_the_baseFont()
        {
            var test = new TestExecutor(this);

            test.Assert(() =>
                {
                    const string format = "{4} 0 obj\r\n<< /Type /Font /Subtype /{0} /Name {1} /BaseFont /{2} /Encoding /{3} /Widths {5} 0 R /FirstChar 0 /LastChar 255 /FontDescriptor {6} 0 R >>\r\nendobj\r\n";
                    var expectedValue = string.Format(format, "Type1", _fontName, _fontProperties.Name, "WinAnsiEncoding", _objectNumber, _fontWidths.Number, _fontDescriptor.Number);
                    Assert.AreEqual(expectedValue, _sut.Content.ToString());
                });
        }
    }
}