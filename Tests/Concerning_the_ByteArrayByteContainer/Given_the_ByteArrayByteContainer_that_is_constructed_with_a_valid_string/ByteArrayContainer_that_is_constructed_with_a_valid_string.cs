using PdfCraft.Containers;

namespace Tests.Concerning_the_ByteArrayByteContainer.Given_the_ByteArrayByteContainer_that_is_constructed_with_a_valid_string
{
    public abstract class ByteArrayContainer_that_is_constructed_with_a_valid_string : BaseTest
    {
        protected ByteArrayByteContainer Sut;
        protected string ValidString;

        public override void Arrange()
        {
            ValidString = "valid string";
            Sut = new ByteArrayByteContainer(ValidString);
        }
    }
}