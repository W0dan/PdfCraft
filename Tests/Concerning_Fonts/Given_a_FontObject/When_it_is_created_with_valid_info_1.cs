using NUnit.Framework;
using PdfCraft.Fonts;

namespace Tests.Concerning_Fonts.Given_a_FontObject
{
    public class When_it_is_created_with_valid_info_1 : BaseTest
    {
        private FontObject _sut;
        private int _objectNumber;
        private string _fontName;
        private string _baseFontname;
        private FontProperties _fontProperties;

        public override void Arrange()
        {
            _objectNumber = 127;
            _fontName = "/F1";
            _baseFontname = "Courier";
            _fontProperties = new FontProperties { Name = _baseFontname, Size = 10 };
        }

        public override void Act()
        {
            _sut = new FontObject(_objectNumber, _fontName, _fontProperties.Name);
        }

        [Test]
        public void It_should_render_a_PdfFontObject_with_the_fontName_and_the_name_of_the_baseFont()
        {
            var test = new TestExecutor(this);

            test.Assert(() =>
                {
                    const string format = "{4} 0 obj\r\n<< /Type /Font /Subtype /{0} /Name {1} /BaseFont /{2} /Encoding /{3} >>\r\nendobj\r\n";
                    var expectedValue = string.Format(format, "Type1", _fontName, _fontProperties.Name, "WinAnsiEncoding", _objectNumber);
                    Assert.AreEqual(expectedValue, _sut.Content.ToString());
                });
        }
    }
}