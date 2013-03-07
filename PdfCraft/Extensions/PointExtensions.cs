using System.Drawing;

namespace PdfCraft.Extensions
{
    public static class PointExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point">coördinates in inches</param>
        /// <returns></returns>
        public static Point GetPointInMillimeters(this Point point)
        {
            return new Point((int)(point.X * 2.54), (int)(point.Y * 2.54));
        }
    }
}