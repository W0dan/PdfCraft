using System.Collections.Generic;
using System.Linq;
using System.Text;
using PdfCraft.API;
using PdfCraft.Constants;
using PdfCraft.Containers;
using PdfCraft.Contents;
using PdfCraft.Extensions;

namespace PdfCraft
{
    public class ContentsObject : BasePdfObject
    {
        private readonly List<ContentsPart> parts = new List<ContentsPart>();
        private IByteContainer content;

        public ContentsObject(int objectNumber)
            : base(objectNumber)
        {
        }

        public void AddTextBox(TextBox textbox)
        {
            parts.Add(textbox);
        }

        public void AddCanvas(GraphicsCanvas canvas)
        {
            parts.Add(canvas);
        }

        public IEnumerable<string> GetXObjectnames()
        {
            var result = new HashSet<string>();

            foreach (var part in parts.Where(x => !x.IsText))
            {
                var canvas = (GraphicsCanvas)part;
                var xObjectnames = canvas.GetXObjectnames();

                foreach (var xObjectname in xObjectnames
                    .Where(xObjectname => !result.Contains(xObjectname)))
                {
                    result.Add(xObjectname);
                }
            }

            return result;
        }

        public IEnumerable<string> GetFontnames()
        {
            var result = new HashSet<string>();

            foreach (var part in parts.Where(x => x.IsText))
            {
                var textBox = (TextBox)part;
                var fontnames = textBox.GetFontReferences();

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
                if (this.content != null)
                    return this.content;

                var streamObject = ByteContainerFactory.CreateByteContainer();

                //var currentPartIsText = false;
                foreach (var part in parts)
                {
                    if (part.IsText)
                    {
                        streamObject.Append($"BT{StringConstants.NewLine}");
                    }

                    //if (!currentPartIsText && part.IsText)
                    //{
                    //    currentPartIsText = true;
                    //    streamObject.Append($"BT{StringConstants.NewLine}");
                    //}
                    //else if (currentPartIsText && !part.IsText)
                    //{
                    //    currentPartIsText = false;
                    //    streamObject.Append($"ET{StringConstants.NewLine}");
                    //}

                    streamObject.Append(part.Content);

                    if (part.IsText)
                    {
                        streamObject.Append($"ET{StringConstants.NewLine}");
                    }
                }
                //if (currentPartIsText)
                //    streamObject.Append("ET");

                //var streamData = $"{streamObject.ToHexString()}>";
                var streamData = streamObject;
                var newContent = ByteContainerFactory.CreateByteContainer();
                newContent.Append($"<<{StringConstants.NewLine}" +
                                  $"/Length {streamData.Length}{StringConstants.NewLine}" +
                                  //$"/Filter [/ASCIIHexDecode]{StringConstants.NewLine}" +
                                  $">>{StringConstants.NewLine}");
                newContent.Append($"stream{StringConstants.NewLine}");
                newContent.Append(streamObject);
                newContent.Append($"{StringConstants.NewLine}endstream");

                SetContent(newContent);

                this.content = base.Content;

                return this.content;
            }
        }
    }
}