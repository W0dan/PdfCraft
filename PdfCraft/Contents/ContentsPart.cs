using PdfCraft.Containers;

namespace PdfCraft.Contents
{
    public abstract class ContentsPart
    {
        internal abstract IByteContainer Content { get; }

        internal abstract bool IsText { get; }
    }
}