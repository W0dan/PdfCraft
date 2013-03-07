namespace PdfCraft.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="length">in inches</param>
        /// <returns></returns>
        public static int ToMillimeters(this int length)
        {
            return (int)(length * 2.54);
        }
    }
}