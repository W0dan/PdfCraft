using NUnit.Framework;
using PdfCraft;

namespace Tests.Concerning_the_CatalogObject.Given_a_CatalogObject
{
    public class When_it_is_created : BaseTest
    {
        private int _objectNumber;
        private CatalogObject _sut;

        public override void Arrange()
        {
            _objectNumber = 159;
        }

        public override void Act()
        {
            _sut = new CatalogObject(_objectNumber);
        }

        [Test]
        public void It_should_contain_the_ObjectNumber()
        {
            var test = new TestExecutor(this);

            test.Assert(() => Assert.AreEqual(_objectNumber, _sut.Number));
        }
    }
}