using NUnit.Framework;
using PdfCraft;

namespace Tests.Concerning_the_CatalogObject.Given_a_CatalogObject
{
    [TestFixture]
    public class When_a_PagesObject_is_added : BaseTest
    {
        private int _catalogObjectNumber;
        private CatalogObject _sut;
        private int _pagesObjectNumber;
        private PagesObject _pages;

        public override void Arrange()
        {
            _catalogObjectNumber = 12;
            _pagesObjectNumber = 25;

            _pages = new PagesObject(_pagesObjectNumber);

            _sut = new CatalogObject(_catalogObjectNumber);
        }

        public override void Act()
        {
            _sut.AddPages(_pages);
        }

        [Test]
        public void It_should_render_a_PdfCatalogObject_with_the_objectnumber_of_the_PagesObject()
        {
            var expectedValue = string.Format("{0} 0 obj\r\n<< /Type /Catalog /Pages {1} 0 R >>\r\nendobj\r\n",
                _catalogObjectNumber, _pagesObjectNumber);
            Assert.AreEqual(expectedValue, _sut.Content.ToString());
        }
    }
}