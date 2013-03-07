namespace PdfCraft.Contents
{
    public abstract class ContentsPart
    {
        internal abstract string Content { get; }

        internal abstract bool IsText { get; }
    }
}