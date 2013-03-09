using System.Drawing;

namespace PdfCraft.Contents.Graphics
{
    internal class PoligonDefinition
    {
        private readonly Point[] _points;
        private readonly bool _isClosed;

        public PoligonDefinition(Point[] points, bool isClosed)
        {
            _points = points;
            _isClosed = isClosed;
        }

        public Point[] Points
        {
            get { return _points; }
        }

        public bool IsClosed
        {
            get { return _isClosed; }
        }
    }
}