using NUnit.Framework;
using PdfCraft;

namespace Tests.Concerning_the_PageObject.Given_a_ContentsObject
{
    [TestFixture]
    public class When_it_is_created : BaseTest
    {
        private int _objectNumber;
        private ContentsObject _sut;

        public override void Arrange()
        {
            _objectNumber = 245;
        }

        public override void Act()
        {
            _sut = new ContentsObject(_objectNumber);
        }

        [Test]
        public void It_should_contain_the_ObjectNumber()
        {
            Assert.AreEqual(_objectNumber, _sut.Number);
        }
    }
}