using System.Drawing;
using System.IO;
using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft.Contents.Graphics
{
    public class XObject : BasePdfObject
    {
        private readonly string _xObjectname;
        private readonly byte[] _imageData;
        private readonly string _colorSpace;
        private readonly string _filter;
        private readonly int _bitsPerComponent;
        private readonly int _width;
        private readonly int _height;
        private readonly int _length;

        public XObject(int objectNumber, ImageType imageType, string filename, string xObjectname)
            : this(objectNumber, imageType)
        {
            _xObjectname = xObjectname;
            using (var fs = File.OpenRead(filename))
            {
                _length = (int)fs.Length;
                _imageData = new byte[_length];
                fs.Read(_imageData, 0, _length);
            }

            var bitmap = new Bitmap(filename);
            _width = bitmap.Width;
            _height = bitmap.Height;
            bitmap.Dispose();
        }

        public XObject(int objectNumber, ImageType imageType, Stream imageData, string xObjectname)
            : this(objectNumber, imageType)
        {
            _xObjectname = xObjectname;
            _length = (int)imageData.Length;
            _imageData = new byte[_length];
            imageData.Read(_imageData, 0, _length);

            var bitmap = new Bitmap(imageData);
            _width = bitmap.Width;
            _height = bitmap.Height;
            bitmap.Dispose();
        }

        private XObject(int objectNumber, ImageType imageType) :
            base(objectNumber)
        {
            switch (imageType)
            {
                case ImageType.Jpg:
                    _colorSpace = "DeviceRGB";
                    _filter = "DCTDecode";
                    _bitsPerComponent = 8;
                    break;
            }
        }

        public string XObjectname
        {
            get { return _xObjectname; }
        }

        public override IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory.CreateByteContainer();

                const string formatString = "<< /Type /XObject" + StringConstants.NewLine +
                                            "/Subtype /Image" + StringConstants.NewLine +
                                            "/Width {0}" + StringConstants.NewLine +
                                            "/Height {1}" + StringConstants.NewLine +
                                            "/ColorSpace /{2}" + StringConstants.NewLine +
                                            "/Filter /{3}" + StringConstants.NewLine +
                                            "/BitsPerComponent {4}" + StringConstants.NewLine +
                                            "/Length {5}" + StringConstants.NewLine +
                                            ">>";

                content.Append(string.Format(formatString, _width, _height, _colorSpace, _filter, _bitsPerComponent, _length));

                content.Append("stream" + StringConstants.NewLine);
                content.Append(_imageData);
                content.Append("endstream");

                SetContent(content);

                return base.Content;
            }
        }
    }
}