using System.IO;

namespace PdfCraft.Contents.Graphics
{
    internal class XObjectFactory
    {
        public static XObject CreateXObject(int objectNumber, int xObjectNumber, ImageType imageType, Stream sourceStream)
        {
            var xObjectName = "/X" + xObjectNumber;
            return new XObject(objectNumber, imageType, sourceStream, xObjectName);
        }

        public static XObject CreateXObject(int objectNumber, int xObjectNumber, ImageType imageType, string fileName)
        {
            var xObjectName = "/X" + xObjectNumber;
            return new XObject(objectNumber, imageType, fileName, xObjectName);
        }
    }
}