using System.Drawing;

namespace PdfCraft.Graphics
{
    public class BoxDefinition
    {
        private readonly Point _topLeft;
        private readonly Size _size;

        public BoxDefinition(Point topLeft, Size size)
        {
            _topLeft = topLeft;
            _size = size;
        }

        public Point TopLeft
        {
            get { return _topLeft; }
        }

        public Size Size
        {
            get { return _size; }
        }
    }
}