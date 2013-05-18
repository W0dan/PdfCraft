using NUnit.Framework;

namespace Tests.Concerning_the_ByteArrayByteContainer.Given_the_ByteArrayByteContainer_that_is_constructed_with_a_byte_array
{
    [TestFixture]
    public class When_GetBytes_is_called : ByteArrayContainer_that_is_constructed_with_a_byte_array
    {
        private byte[] _result;

        public override void Act()
        {
            _result = Sut.GetBytes();
        }

        [Test]
        public void It_should_return_those_bytes()
        {
            for (var i = 0; i < ByteArray.Length; i++)
                Assert.AreEqual(ByteArray[i], (char)_result[i]);
        }
    }
}