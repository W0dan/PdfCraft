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
                .CreateByteContainer(string.Format("<< /Type /Catalog /Pages {0} 0 R >>", pages.Number));

            SetContent(content);
        }
    }
}