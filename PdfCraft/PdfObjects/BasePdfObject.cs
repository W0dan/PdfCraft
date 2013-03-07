using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft
{
    public abstract class BasePdfObject
    {
        private readonly int _objectNumber;
        private IByteContainer _objectContent;

        protected BasePdfObject(int objectNumber)
        {
            _objectNumber = objectNumber;
            _objectContent = null;
        }

        protected BasePdfObject(int objectNumber, IByteContainer objectContent)
        {
            _objectNumber = objectNumber;
            _objectContent = objectContent;
        }

        protected void SetContent(string newContent)
        {
            _objectContent = ByteContainerFactory.CreateByteContainer(newContent);
        }

        protected void SetContent(byte[] newContent)
        {
            _objectContent = ByteContainerFactory.CreateByteContainer(newContent);
        }

        protected void SetContent(IByteContainer newContent)
        {
            _objectContent = newContent;
        }

        public int Number
        {
            get { return _objectNumber; }
        }

        public virtual IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory
                    .CreateByteContainer(_objectNumber + " 0 obj" + StringConstants.NewLine);
                content.Append(_objectContent);
                content.Append(StringConstants.NewLine + "endobj" + StringConstants.NewLine);

                return content;
            }
        }

        public int Length
        {
            get
            {
                return Content.Length;
            }
        }

    }
}