using PdfCraft.Containers;

namespace PdfCraft
{
    public class CatalogObject : BasePdfObject
    {
        public CatalogObject(int objectNumber)
            : base(objectNumber)
        {
        }

        public void AddPages(PagesObject pages)
        {
            var content = ByteContainerFactory
                .CreateByteContainer($"<< /Type /Catalog /Pages {pages.Number} 0 R >>");

            SetContent(content);
        }
    }
}