using System.Drawing;
using NUnit.Framework;
using PdfCraft;

namespace Tests.Concerning_the_PagesObject.Given_a_PagesObject
{
    [TestFixture]
    public class When_2_PageObjects_are_added : BaseTest
    {
        private int _page1ObjectNumber;
        private PageObject _page1;
        private int _page2ObjectNumber;
        private PageObject _page2;
        private int _pagesObjectNumber;
        private PagesObject _sut;

        public override void Arrange()
        {
            _pagesObjectNumber = 9;
            _page1ObjectNumber = 51;
            _page2ObjectNumber = 75;

            _page1 = new PageObject(_page1ObjectNumber, new Size(210, 297));
            _page2 = new PageObject(_page2ObjectNumber, new Size(210, 297));

            _sut = new PagesObject(_pagesObjectNumber);
        }

        public override void Act()
        {
            _sut.AddPage(_page1);
            _sut.AddPage(_page2);
        }

        [Test]
        public void It_should_render_a_PdfPagesObject_with_the_objectnumber_of_the_PageObject()
        {
            var expectedValue = string.Format("{0} 0 obj\r\n" +
                                "<< /Type /Pages\r\n/Kids [{1} 0 R {2} 0 R ]\r\n/Count 2\r\n>>\r\n" +
                                "endobj\r\n", _pagesObjectNumber, _page1ObjectNumber, _page2ObjectNumber);

            Assert.AreEqual(expectedValue, _sut.Content.ToString());
        }
    }
}