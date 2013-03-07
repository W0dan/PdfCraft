using System.Text;
using PdfCraft.Extensions;

namespace PdfCraft.Containers
{
    public class StringBuilderByteContainer : IByteContainer
    {
        private readonly StringBuilder _data;

        public StringBuilderByteContainer()
        {
            _data = new StringBuilder();
        }

        public StringBuilderByteContainer(string text)
        {
            _data = new StringBuilder(text);
        }

        public void Append(string text)
        {
            _data.Append(text);
        }

        public void Append(byte[] bytes)
        {
            _data.Append(bytes);
        }

        public void Append(StringBuilder text)
        {
            _data.Append(text);
        }

        public void Append(IByteContainer bytes)
        {
            _data.Append(bytes);
        }

        public override string ToString()
        {
            return _data.ToString();
        }

        public byte[] GetBytes()
        {
            return ToString().ToBytes();
        }

        public int Length { get { return _data.ToString().Length; } }
    }
}