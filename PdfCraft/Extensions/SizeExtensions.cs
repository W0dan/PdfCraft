using System.Drawing;

namespace PdfCraft.Extensions
{
    public static class SizeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">dimensions in inches</param>
        /// <returns></returns>
        public static Size GetSizeInMillimeters(this Size size)
        {
            return new Size((int)(size.Width * 2.54), (int)(size.Height * 2.54));
        }
    }
}