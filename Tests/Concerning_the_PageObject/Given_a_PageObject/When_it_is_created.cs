using System.Drawing;
using NUnit.Framework;
using PdfCraft;

namespace Tests.Concerning_the_PageObject.Given_a_PageObject
{
    public class When_it_is_created : BaseTest
    {
        private int _objectNumber;
        private PageObject _sut;
        private int _width;
        private int _height;
        private ContentsObject _contents;

        public override void Arrange()
        {
            _width = 210;
            _height = 297;
            _objectNumber = 145;
            _contents = new ContentsObject(254);
        }

        public override void Act()
        {
            _sut = new PageObject(_objectNumber, new Size(_width, _height));
            _sut.AddContents(_contents);
        }

        [Test]
        public void It_should_contain_the_ObjectNumber()
        {
            var test = new TestExecutor(this);

            test.Assert(() => Assert.AreEqual(_objectNumber, _sut.Number));
        }

        [Test]
        public void It_should_be_constructed_with_its_size_in_mm()
        {
            var test = new TestExecutor(this);

            var expectedValue = string.Format("/MediaBox [0 0 {0} {1}]", (int)(_width * 2.54), (int)(_height * 2.54));

            test.Assert(() => Assert.IsTrue(_sut.Content.ToString().Contains(expectedValue)));
        }
    }
}