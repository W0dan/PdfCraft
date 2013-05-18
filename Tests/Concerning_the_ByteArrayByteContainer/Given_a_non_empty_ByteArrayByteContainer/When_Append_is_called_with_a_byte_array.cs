using NUnit.Framework;

namespace Tests.Concerning_the_ByteArrayByteContainer.Given_a_non_empty_ByteArrayByteContainer
{
    [TestFixture]
    public class When_Append_is_called_with_a_byte_array : Non_empty_ByteArrayByteContainer
    {
        protected byte[] _appendedContent;

        public override void Act()
        {
            _appendedContent = new byte[] { 65, 66, 67, 68, 69, 70, 255, 254, 253, 252, 251, 250 };
            Sut.Append(_appendedContent);
        }

        [Test]
        public void It_should_contain_the_original_bytes_before_the_bytes_of_the_appended_string()
        {
            var content = Sut.GetBytes();
            for (var i = 0; i < content.Length; i++)
            {
                if (i < OriginalContent.Length)
                    Assert.AreEqual(OriginalContent[i], (char)content[i]);
                else
                    Assert.AreEqual(_appendedContent[i - OriginalContent.Length], content[i]);
            }
        }
    }
}