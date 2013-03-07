using System.Collections.Generic;
using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft
{
    internal class PdfGenerator
    {
        private readonly List<string> _xref;
        private readonly IByteContainer _content;
        private int _offset;
        private int _catalogObjectNumber;

        public PdfGenerator()
        {
            _xref = new List<string> { "0000000000 65535 f" + StringConstants.NewLine };
            _content = ByteContainerFactory.CreateByteContainer("%PDF-1.7" + StringConstants.NewLine);
            _offset = 9;
        }

        public void AddCatalog(CatalogObject catalog)
        {
            AddObject(catalog);
            _catalogObjectNumber = catalog.Number;
        }

        public void AddObject(BasePdfObject obj)
        {
            _xref.Add(string.Format("{0:0000000000} 00000 n", _offset) + StringConstants.NewLine);
            _content.Append(obj.Content);
            _offset += obj.Length;
        }

        public byte[] GetBytes()
        {
            _content.Append("xref" + StringConstants.NewLine);
            _content.Append(string.Format("0 {0}", _xref.Count) + StringConstants.NewLine);
            foreach (var xrefEntry in _xref)
                _content.Append(xrefEntry);

            _content.Append("trailer" + StringConstants.NewLine);
            _content.Append(string.Format("<< /Size {0}", _xref.Count) + StringConstants.NewLine);
            _content.Append(string.Format("/Root {0} 0 R", _catalogObjectNumber) + StringConstants.NewLine);
            _content.Append(">>" + StringConstants.NewLine);
            _content.Append("startxref" + StringConstants.NewLine);
            _content.Append(string.Format("{0}", _offset) + StringConstants.NewLine);
            _content.Append("%%EOF");

            return _content.GetBytes();
        }
    }
}