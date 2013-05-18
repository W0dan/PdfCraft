using System.Drawing;
using NUnit.Framework;
using PdfCraft;

namespace Tests.Concerning_the_PagesObject.Given_a_PagesObject
{
    [TestFixture]
    public class When_a_PageObject_is_added : BaseTest
    {
        private int _pageObjectNumber;
        private PageObject _page;
        private int _pagesObjectNumber;
        private PagesObject _sut;

        public override void Arrange()
        {
            _pagesObjectNumber = 9;
            _pageObjectNumber = 51;

            _page = new PageObject(_pageObjectNumber, new Size(210, 297));
            var contents = new ContentsObject(295);
            _page.AddContents(contents);

            _sut = new PagesObject(_pagesObjectNumber);
        }

        public override void Act()
        {
            _sut.AddPage(_page);
        }

        [Test]
        public void It_should_render_a_PdfPagesObject_with_the_objectnumber_of_the_PageObject()
        {
            var expectedValue = string.Format("{0} 0 obj\r\n" +
                                "<< /Type /Pages\r\n/Kids [{1} 0 R ]\r\n/Count 1\r\n>>\r\n" +
                                "endobj\r\n", _pagesObjectNumber, _pageObjectNumber);

            Assert.AreEqual(expectedValue, _sut.Content.ToString());
        }

        [Test]
        public void It_should_set_the_ParentObjectNumber_on_the_Page()
        {
            var expectedValue = string.Format("/Parent {0} 0 R", _pagesObjectNumber);

            Assert.IsTrue(_page.Content.ToString().Contains(expectedValue));
        }
    }
}