using PdfCraft.Containers;

namespace Tests.Concerning_the_ByteArrayByteContainer.Given_the_ByteArrayByteContainer_that_is_constructed_with_a_byte_array
{
    public abstract class ByteArrayContainer_that_is_constructed_with_a_byte_array : BaseTest
    {
        protected ByteArrayByteContainer Sut;
        protected byte[] ByteArray;

        public override void Arrange()
        {
            ByteArray = new byte[] { 65, 66, 67, 68, 69, 70, 255, 254, 253, 252, 251, 250 };
            Sut = new ByteArrayByteContainer(ByteArray);
        }
    }
}