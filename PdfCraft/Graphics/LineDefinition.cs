using System.Drawing;

namespace PdfCraft.Graphics
{
    internal class LineDefinition
    {
        private readonly Point _start;
        private readonly Point _end;

        public LineDefinition(Point start, Point end)
        {
            _start = start;
            _end = end;
        }

        public Point Start
        {
            get { return _start; }
        }

        public Point End
        {
            get { return _end; }
        }
    }
}