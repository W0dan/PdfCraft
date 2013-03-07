using System.Collections.Generic;
using System.Text;
using PdfCraft.Extensions;

namespace PdfCraft.Containers
{
    public class ByteArrayByteContainer : IByteContainer
    {
        private const int InitialSize = 100;
        private const int IncrementSize = 100;
        private int _allocatedSize;
        private int _currentSize;
        private byte[] _data;

        public ByteArrayByteContainer()
        {
            _data = new byte[InitialSize];
            _allocatedSize = InitialSize;
        }

        public ByteArrayByteContainer(string text)
            : this(text.ToBytes())
        {
        }

        public ByteArrayByteContainer(byte[] bytes)
        {
            Allocate(bytes.Length);
            AppendData(bytes);

            _currentSize = bytes.Length;
        }

        private void Allocate(int length)
        {
            var numberOfIncrements = length / IncrementSize;

            _allocatedSize = (numberOfIncrements + 1) * IncrementSize;
            _data = new byte[_allocatedSize];
        }

        private void AppendData(IList<byte> bytes)
        {
            for (var i = 0; i < bytes.Count; i++)
                _data[i + _currentSize] = bytes[i];

            _currentSize += bytes.Count;
        }

        public void Append(byte[] bytes)
        {
            if (_allocatedSize < _currentSize + bytes.Length)
                ReAllocate(bytes.Length);

            AppendData(bytes);
        }

        private void ReAllocate(int addedLength)
        {
            var numberOfIncrements = addedLength / IncrementSize;

            var tmp = _data;
            _allocatedSize = tmp.Length + ((numberOfIncrements + 1) * IncrementSize);

            _data = new byte[_allocatedSize];
            for (var i = 0; i < _currentSize; i++)
                _data[i] = tmp[i];
        }

        public void Append(string text)
        {
            Append(text.ToBytes());
        }

        public void Append(StringBuilder text)
        {
            Append(text.ToString());
        }

        public void Append(IByteContainer bytes)
        {
            Append(bytes.GetBytes());
        }

        public override string ToString()
        {
            return Encoding.Default.GetString(GetBytes());
        }

        public byte[] GetBytes()
        {
            var tmp = new byte[_currentSize];
            for (var i = 0; i < tmp.Length; i++)
            {
                tmp[i] = _data[i];
            }
            return tmp;
        }

        public int Length { get { return _currentSize; } }
    }
}