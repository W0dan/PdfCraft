using System.Drawing;

namespace PdfCraft.Graphics
{
    public class CircleDefinition
    {
        private readonly Point _center;
        private readonly double _radius;

        public CircleDefinition(Point center, double radius)
        {
            _center = center;
            _radius = radius;
        }

        public Point Center
        {
            get { return _center; }
        }

        public double Radius
        {
            get { return _radius; }
        }
    }
}