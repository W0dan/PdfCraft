using System;
using System.Collections.Generic;
using System.IO;

namespace PdfCraft.Contents.Graphics
{
    internal class XObjects
    {
        private int _nextXObjectNumber;
        private readonly Dictionary<int, XObject> _xObjects = new Dictionary<int, XObject>();

        public Dictionary<int, XObject> ToDictionary()
        {
            return _xObjects;
        }

        public XObject AddXObject(ImageType imageType, Stream sourceStream, Func<int> getNextObjectNumber)
        {
            var hash = sourceStream.GetHashCode();

            XObject xObject;
            if (!_xObjects.ContainsKey(hash))
            {
                xObject = XObjectFactory.CreateXObject(getNextObjectNumber(), _nextXObjectNumber++, imageType, sourceStream);

                _xObjects.Add(hash, xObject);
            }
            else
                xObject = _xObjects[hash];

            return xObject;
        }

        public XObject AddXObject(ImageType imageType, string fileName, Func<int> getNextObjectNumber)
        {
            var hash = fileName.GetHashCode();

            XObject xObject;
            if (!_xObjects.ContainsKey(hash))
            {
                xObject = XObjectFactory.CreateXObject(getNextObjectNumber(), _nextXObjectNumber++, imageType, fileName);

                _xObjects.Add(hash, xObject);
            }
            else
                xObject = _xObjects[hash];

            return xObject;
        }

    }
}