using System.Collections.Generic;
using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft
{
    internal class PdfGenerator
    {
        private readonly Dictionary<int, int> _xref;
        private readonly IByteContainer _content;
        private int _offset;
        private int _catalogObjectNumber;

        public PdfGenerator()
        {
            _xref = new Dictionary<int, int>();
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
            _xref.Add(obj.Number, _offset);
            _content.Append(obj.Content);
            _offset += obj.Length;
        }

        public byte[] GetBytes()
        {
            _content.Append("xref" + StringConstants.NewLine);
            _content.Append(string.Format("0 {0}", _xref.Count + 1) + StringConstants.NewLine);
            _content.Append("0000000000 65535 f" + StringConstants.NewLine);

            for (var i = 0; i < _xref.Count; i++)
            {
                var offset = _xref[i + 1];
                _content.Append(string.Format("{0:0000000000} 00000 n", offset) + StringConstants.NewLine);
            }

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