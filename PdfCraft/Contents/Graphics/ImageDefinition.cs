using System.Drawing;
using System.IO;

namespace PdfCraft.Contents.Graphics
{
    public class ImageDefinition
    {
        private readonly Point _topLeft;
        private readonly Size _size;
        private readonly ImageType _imageType;
        private readonly Stream _sourceStream;
        private readonly string _sourceFile;
        private readonly XObject _xObject;

        public ImageDefinition(Point topLeft, Size size, ImageType imageType, string sourceFile, XObject xObject)
            : this(topLeft, size, imageType)
        {
            _sourceFile = sourceFile;
            _xObject = xObject;
        }

        public ImageDefinition(Point topLeft, Size size, ImageType imageType, Stream sourceStream, XObject xObject)
            : this(topLeft, size, imageType)
        {
            _sourceStream = sourceStream;
            _xObject = xObject;
        }

        public ImageDefinition(Point topLeft, Size size, ImageType imageType)
        {
            _topLeft = topLeft;
            _size = size;
            _imageType = imageType;
        }

        public Point TopLeft
        {
            get { return _topLeft; }
        }

        public Size Size
        {
            get { return _size; }
        }

        public ImageType ImageType
        {
            get { return _imageType; }
        }

        public Stream SourceStream
        {
            get { return _sourceStream; }
        }

        public string SourceFile
        {
            get { return _sourceFile; }
        }

        public XObject XObject
        {
            get { return _xObject; }
        }
    }
}