using System.Collections.Generic;
using System.Text;
using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft
{
    internal class PdfGenerator
    {
        private readonly Dictionary<int, int> xref;
        private readonly IByteContainer content;
        private int offset;
        private int catalogObjectNumber;

        public PdfGenerator()
        {
            xref = new Dictionary<int, int>();
            var bytes = new byte[] { 129, 130, 131, 132 };
            var binaryData = Encoding.GetEncoding("437").GetString(bytes);
            var pdfHeader = $"%PDF-1.7{StringConstants.NewLine}" +
                            $"%{binaryData}{StringConstants.NewLine}";
            content = ByteContainerFactory.CreateByteContainer(Encoding.GetEncoding("437").GetBytes(pdfHeader));
            offset = 9 + 7;
        }

        public void AddCatalog(CatalogObject catalog)
        {
            AddObject(catalog);
            catalogObjectNumber = catalog.Number;
        }

        private void AddObjectInternal(BasePdfObject obj)
        {
            xref.Add(obj.Number, offset);
            var tempContent = obj.Content;
            content.Append(tempContent);
            offset += obj.Length;
        }

        /// <summary>
        /// Adds the object and its childobjects recursively
        /// </summary>
        /// <param name="obj"></param>
        public void AddObject(BasePdfObject obj)
        {
            AddObjectInternal(obj);
            foreach (var o in obj.ChildObjects)
            {
                AddObject(o);
            }
        }

        public byte[] GetBytes()
        {
            content.Append($"xref{StringConstants.NewLine}");
            content.Append($"0 {xref.Count + 1}{StringConstants.NewLine}");
            content.Append($"0000000000 65535 f{StringConstants.NewLine}");

            for (var i = 0; i < xref.Count; i++)
            {
                var xrefOffset = xref[i + 1];
                content.Append($"{xrefOffset:0000000000} 00000 n{StringConstants.NewLine}");
            }

            content.Append($"trailer{StringConstants.NewLine}");
            content.Append($"<< /Size {xref.Count + 1}{StringConstants.NewLine}");
            content.Append($"/Root {catalogObjectNumber} 0 R{StringConstants.NewLine}");
            content.Append($">>{StringConstants.NewLine}");
            content.Append($"startxref{StringConstants.NewLine}");
            content.Append($"{offset}{StringConstants.NewLine}");
            content.Append("%%EOF");

            return content.GetBytes();
        }
    }
}