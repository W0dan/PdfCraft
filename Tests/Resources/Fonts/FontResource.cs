using System.Collections.Generic;

namespace Tests.Resources.Fonts
{
    public static class FontResource
    {
        private static readonly Dictionary<string, byte[]> FontBytes = new Dictionary<string, byte[]>();

        public static byte[] GetArial_Regular()
            => ReadFontbytes("Tests.Resources.Fonts.arial.ttf");

        public static byte[] GetArial_Bold()
            => ReadFontbytes("Tests.Resources.Fonts.arialbd.ttf");

        public static byte[] GetArial_BoldItalic()
            => ReadFontbytes("Tests.Resources.Fonts.arialbi.ttf");

        public static byte[] GetArial_Italic()
            => ReadFontbytes("Tests.Resources.Fonts.ariali.ttf");

        public static byte[] GetArial_Black()
            => ReadFontbytes("Tests.Resources.Fonts.ariblk.ttf");

        public static byte[] GetLinuxLibertine_Regular()
            => ReadFontbytes("Tests.Resources.Fonts.LinLibertine_R_G.ttf");

        public static byte[] GetFlandersArtSans_Regular()
            => ReadFontbytes("Tests.Resources.Fonts.FlandersArtSans-Regular.ttf");

        private static byte[] ReadFontbytes(string fontResource)
        {
            if (FontBytes.ContainsKey(fontResource))
                return FontBytes[fontResource];

            var bytes = ResourceReader.GetInstance.ReadBytes(fontResource, typeof(FontResource).Assembly);
            FontBytes.Add(fontResource, bytes);

            return bytes;
        }
    }
}