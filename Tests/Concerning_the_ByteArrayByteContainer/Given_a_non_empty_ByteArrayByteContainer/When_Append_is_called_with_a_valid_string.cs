using NUnit.Framework;

namespace Tests.Concerning_the_ByteArrayByteContainer.Given_a_non_empty_ByteArrayByteContainer
{
    public class When_Append_is_called_with_a_valid_string : Non_empty_ByteArrayByteContainer
    {
        protected string _appendedContent;

        public override void Act()
        {
            _appendedContent = "this is a valid string";
            Sut.Append(_appendedContent);
        }

        [Test]
        public void It_should_contain_the_original_bytes_before_the_bytes_of_the_appended_string()
        {
            var test = new TestExecutor(this);

            test.Assert(() => Assert.AreEqual(OriginalContent + _appendedContent, Sut.ToString()));
        }
    }
}