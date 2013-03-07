using System.Text;

namespace PdfCraft.Containers
{
    public interface IByteContainer
    {
        void Append(string text);
        void Append(byte[] bytes);
        void Append(StringBuilder text);
        void Append(IByteContainer bytes);
        string ToString();
        byte[] GetBytes();
        int Length { get; }
    }
}