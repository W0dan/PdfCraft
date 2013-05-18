using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;

namespace Tests.Concerning_the_API.Given_a_document
{
    [TestFixture]
    public class When_CreateTextBox_is_called : BaseTest
    {
        private Document _sut;
        private TextBox _result;

        public override void Arrange()
        {
            _sut = new Document();
        }

        public override void Act()
        {
            _result = _sut.CreateTextBox(new Rectangle(new Point(50, 50), new Size(50, 50)));
        }

        [Test]
        public void It_should_create_an_instance_of_TextBox()
        {
            Assert.IsNotNull(_result);
        }

        [Test]
        public void It_should_bind_itself_to_the_TextBox()
        {
            Assert.AreSame(_sut, _result.Owner);
        }
    }
}