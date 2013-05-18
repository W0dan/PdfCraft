using NUnit.Framework;

namespace Tests.Concerning_the_ByteArrayByteContainer.Given_the_ByteArrayByteContainer_that_is_constructed_with_a_valid_string
{
    [TestFixture]
    public class When_GetBytes_is_called : ByteArrayContainer_that_is_constructed_with_a_valid_string
    {
        private byte[] _result;

        public override void Act()
        {
            _result = Sut.GetBytes();
        }

        [Test]
        public void It_should_return_the_bytes_of_that_string()
        {
            for (var i = 0; i < ValidString.Length; i++)
                Assert.AreEqual(ValidString[i], (char)_result[i]);
        }
    }
}