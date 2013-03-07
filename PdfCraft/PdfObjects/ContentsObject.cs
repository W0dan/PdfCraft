using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PdfCraft.API;
using PdfCraft.Constants;
using PdfCraft.Containers;
using PdfCraft.Contents;

namespace PdfCraft
{
    public class ContentsObject : BasePdfObject
    {
        private readonly List<ContentsPart> _parts = new List<ContentsPart>();
        private Size _pageSize;
        private IByteContainer _content;

        public ContentsObject(int objectNumber)
            : base(objectNumber)
        {
        }

        public void AddTextBox(TextBox textbox)
        {
            _parts.Add(textbox);
        }

        public void AddCanvas(GraphicsCanvas canvas)
        {
            _parts.Add(canvas);
        }

        public IEnumerable<string> GetFontnames()
        {
            var result = new HashSet<string>();

            foreach (var part in _parts.Where(x => x.IsText))
            {
                var textBox = (TextBox)part;
                var fontnames = textBox.GetFontnames();

                foreach (var fontname in fontnames
                    .Where(fontname => !result.Contains(fontname)))
                {
                    result.Add(fontname);
                }
            }

            return result;
        }

        public override IByteContainer Content
        {
            get
            {
                if (_content != null)
                    return _content;

                var streamObject = ByteContainerFactory.CreateByteContainer();

                var currentPartIsText = false;
                foreach (var part in _parts)
                {
                    if (!currentPartIsText && part.IsText)
                    {
                        currentPartIsText = true;
                        streamObject.Append("BT" + StringConstants.NewLine);
                    }
                    else if (currentPartIsText && !part.IsText)
                    {
                        currentPartIsText = false;
                        streamObject.Append("ET" + StringConstants.NewLine);
                    }

                    streamObject.Append(part.Content);
                }
                if (currentPartIsText)
                    streamObject.Append("ET");

                var content = ByteContainerFactory.CreateByteContainer();
                content.Append(string.Format("<< /length {0} >>", streamObject.Length) + StringConstants.NewLine);
                content.Append("stream" + StringConstants.NewLine);
                content.Append(streamObject);
                content.Append(StringConstants.NewLine + "endstream");

                SetContent(content);

                _content = base.Content;

                return _content;
            }
        }
    }
}