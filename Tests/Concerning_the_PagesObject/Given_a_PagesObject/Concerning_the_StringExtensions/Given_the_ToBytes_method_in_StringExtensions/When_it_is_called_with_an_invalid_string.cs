using System;
using NUnit.Framework;
using PdfCraft.Extensions;

namespace Tests.Concerning_the_StringExtensions.Given_the_ToBytes_method_in_StringExtensions
{
    [TestFixture]
    public class When_it_is_called_with_an_invalid_string : BaseTest
    {
        private string _text;

        public override void Arrange()
        {
            _text = "é€";
        }

        public override void Act()
        {
            _text.ToBytes();
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void It_should_throw_an_ArgumentException()
        {
            var test = new TestExecutor(this);

            test.Assert(() => Assert.Fail("No exception has been thrown"));
        }
    }
}