using System.Drawing;

namespace PdfCraft.Extensions
{
    public static class ColorExtensions
    {
        public static string ToPdfColor(this Color color)
        {
            var pdfColor = string.Format("{0:0.###} {1:0.###} {2:0.###}", (double)color.R / 255, (double)color.G / 255, (double)color.B / 255);

            return pdfColor.Replace(',', '.');
        }
    }
}