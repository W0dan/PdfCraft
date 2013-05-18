using System.Drawing;
using NUnit.Framework;
using PdfCraft;

namespace Tests.Concerning_the_PageObject.Given_a_PageObject
{
    [TestFixture]
    public class When_a_ContentsObject_is_added : BaseTest
    {
        private int _pageObjectNumber;
        private PageObject _sut;
        private int _contentsObjectNumber;
        private ContentsObject _contents;

        public override void Arrange()
        {
            _pageObjectNumber = 12;
            _contentsObjectNumber = 25;

            _contents = new ContentsObject(_contentsObjectNumber);

            _sut = new PageObject(_pageObjectNumber, new Size(210, 297));
        }

        public override void Act()
        {
            _sut.AddContents(_contents);
        }

        [Test]
        public void It_should_render_a_PdfPageObject_with_the_objectnumber_of_the_ContentsObject()
        {
            var expectedValue = string.Format("/Contents {0} 0 R", _contentsObjectNumber);

            Assert.IsTrue(_sut.Content.ToString().Contains(expectedValue));
        }
    }
}