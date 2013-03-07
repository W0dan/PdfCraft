using NUnit.Framework;
using PdfCraft;

namespace Tests.Concerning_the_PagesObject.Given_a_PagesObject
{
    public class When_it_is_created : BaseTest
    {
        private int _objectNumber;
        private PagesObject _sut;

        public override void Arrange()
        {
            _objectNumber = 954;
        }

        public override void Act()
        {
            _sut = new PagesObject(_objectNumber);
        }

        [Test]
        public void It_should_contain_the_ObjectNumber()
        {
            var test = new TestExecutor(this);

            test.Assert(() => Assert.AreEqual(_objectNumber, _sut.Number));
        }
    }
}