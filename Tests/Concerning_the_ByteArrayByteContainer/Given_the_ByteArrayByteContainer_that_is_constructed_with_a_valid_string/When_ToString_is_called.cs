using NUnit.Framework;

namespace Tests.Concerning_the_ByteArrayByteContainer.Given_the_ByteArrayByteContainer_that_is_constructed_with_a_valid_string
{
    [TestFixture]
    public class When_ToString_is_called : ByteArrayContainer_that_is_constructed_with_a_valid_string
    {
        private string _result;

        public override void Act()
        {
            _result = Sut.ToString();
        }

        [Test]
        public void It_should_return_that_string()
        {
            Assert.AreEqual(ValidString, _result);
        }
    }
}