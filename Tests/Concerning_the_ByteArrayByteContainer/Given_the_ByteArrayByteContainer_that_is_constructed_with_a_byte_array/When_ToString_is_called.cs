using NUnit.Framework;

namespace Tests.Concerning_the_ByteArrayByteContainer.Given_the_ByteArrayByteContainer_that_is_constructed_with_a_byte_array
{
    [TestFixture]
    public class When_ToString_is_called : ByteArrayContainer_that_is_constructed_with_a_byte_array
    {
        private string _result;

        public override void Act()
        {
            _result = Sut.ToString();
        }

        [Test]
        public void It_should_return_that_byteArray_as_a_string()
        {
            for (var i = 0; i < ByteArray.Length; i++)
                Assert.AreEqual((char)ByteArray[i], _result[i]);
        }
    }
}