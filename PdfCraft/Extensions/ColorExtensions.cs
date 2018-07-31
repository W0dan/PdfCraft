using System.Drawing;

namespace PdfCraft.Extensions
{
    public static class ColorExtensions
    {
        public static string ToPdfColor(this Color color)
        {
            var pdfColor = $"{(double)color.R / 255:0.###} {(double)color.G / 255:0.###} {(double)color.B / 255:0.###}";

            return pdfColor.Replace(',', '.');
        }
    }
}