using NUnit.Framework;
using PdfCraft.Extensions;

namespace Tests.Concerning_the_StringExtensions.Given_the_ToBytes_method_in_StringExtensions
{
    public class When_it_is_called_with_a_valid_string : BaseTest
    {
        private string _text;
        private byte[] _result;

        public override void Arrange()
        {
            _text = "abcdefghijklmnopqrstuvwxyz1234567890";
        }

        public override void Act()
        {
            _result = _text.ToBytes();
        }

        [Test]
        public void It_should_return_the_bytes_of_that_string()
        {
            var test = new TestExecutor(this);

            test.Assert(() =>
                {
                    for (var i = 0; i < _result.Length; i++)
                    {
                        Assert.AreEqual(_text[i], (char)_result[i]);
                    }
                });
        }
    }
}