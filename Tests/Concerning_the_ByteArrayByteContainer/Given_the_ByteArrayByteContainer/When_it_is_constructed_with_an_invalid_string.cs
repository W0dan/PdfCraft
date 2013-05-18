using System;
using NUnit.Framework;
using PdfCraft.Containers;

namespace Tests.Concerning_the_ByteArrayByteContainer.Given_the_ByteArrayByteContainer
{
    [TestFixture]
    public class When_it_is_constructed_with_an_invalid_string : BaseTest
    {
        protected ByteArrayByteContainer Sut;
        protected string InvalidString;

        public override void Arrange()
        {
            InvalidString = "invalid string é µ €";
        }

        public override void Act()
        {
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void It_should_throw_an_Exception()
        {
            Sut = new ByteArrayByteContainer(InvalidString);
        }
    }
}