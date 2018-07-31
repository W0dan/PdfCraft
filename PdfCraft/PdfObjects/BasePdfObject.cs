using System.Collections.Generic;
using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft
{
    public abstract class BasePdfObject
    {
        private IByteContainer objectContent;
        public readonly List<BasePdfObject> ChildObjects = new List<BasePdfObject>();
        private IByteContainer cachedContent = null;

        protected BasePdfObject(int objectNumber)
        {
            this.Number = objectNumber;
            objectContent = null;
        }

        protected void SetContent(IByteContainer newContent)
        {
            objectContent = newContent;
        }

        public int Number { get; }

        public virtual IByteContainer Content
        {
            get
            {
                if (cachedContent == null)
                {
                    cachedContent = ByteContainerFactory
                        .CreateByteContainer($"{Number} 0 obj{StringConstants.NewLine}");
                    cachedContent.Append(objectContent);
                    cachedContent.Append($"{StringConstants.NewLine}endobj{StringConstants.NewLine}");
                }
                return cachedContent;
            }
        }

        public int Length => Content.Length;
    }
}