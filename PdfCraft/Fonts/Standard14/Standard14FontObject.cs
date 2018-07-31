using System.Text;
using PdfCraft.Containers;

namespace PdfCraft.Fonts.Standard14
{
    public class Standard14FontObject : FontObject
    {
        private IFontWidths FontWidths { get; set; }

        private Standard14FontWidths fontWidths;
        private Standard14FontDescriptor fontDescriptor;

        public Standard14FontObject(int objectNumber, string fontName, string name)
            : base(objectNumber, fontName, name)
        {
        }

        internal void SetFontWidths(Standard14FontWidths value)
        {
            this.fontWidths = value;
            this.FontWidths = value;
            ChildObjects.Add(value);
        }

        internal void SetFontDescriptor(Standard14FontDescriptor value)
        {
            this.fontDescriptor = value;
            ChildObjects.Add(value);
        }

        public override IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory
                    .CreateByteContainer($"<< " +
                                         $"/Type /Font " +
                                         $"/Subtype /Type1 " +
                                         $"/Name {FontName} " +
                                         $"/BaseFont /{Name} " +
                                         $"/Encoding /WinAnsiEncoding " +
                                         $"/Widths {fontWidths.Number} 0 R " +
                                         $"/FirstChar 0 /LastChar 255 " +
                                         $"/FontDescriptor {fontDescriptor.Number} 0 R " +
                                         $">>");

                SetContent(content);

                return base.Content;
            }
        }

        public override int GetWidth(char c, int size)
        {
            return FontWidths.Widths[(byte)c] * size;
        }

        public override byte[] Encode(string text)
        {
            return Encoding.Default.GetBytes(text);
        }

        public override string Map(string text)
        {
            return EscapeText(text);
        }

        private static string EscapeText(string text)
        {
            text = text.Replace(@"\", @"\\");
            text = text.Replace(@"(", @"\(");
            text = text.Replace(@")", @"\)");

            text = text.Replace("€", @"\200"); //128

            text = text.Replace("‚", @"\202"); //130
            text = text.Replace("ƒ", @"\203");
            text = text.Replace("„", @"\204");
            text = text.Replace("…", @"\205");
            text = text.Replace("†", @"\206");
            text = text.Replace("‡", @"\207");

            text = text.Replace("ˆ", @"\210"); //136
            text = text.Replace("‰", @"\211");
            text = text.Replace("Š", @"\212");
            text = text.Replace("‹", @"\213");
            text = text.Replace("Œ", @"\214");
            text = text.Replace("Ž", @"\216");

            text = text.Replace("‘", @"\221"); //145
            text = text.Replace("’", @"\222");
            text = text.Replace("“", @"\223");
            text = text.Replace("”", @"\224");
            text = text.Replace("•", @"\225");
            text = text.Replace("–", @"\226");
            text = text.Replace("—", @"\227");

            text = text.Replace("˜", @"\230"); //152
            text = text.Replace("™", @"\231");
            text = text.Replace("š", @"\232");
            text = text.Replace("›", @"\233");
            text = text.Replace("œ", @"\234");
            text = text.Replace("ž", @"\236");
            text = text.Replace("Ÿ", @"\237");

            text = text.Replace(" ", @"\240"); //160
            text = text.Replace("¡", @"\241");
            text = text.Replace("¢", @"\242");
            text = text.Replace("£", @"\243");
            text = text.Replace("¤", @"\244");
            text = text.Replace("¥", @"\245");
            text = text.Replace("¦", @"\246");
            text = text.Replace("§", @"\247");

            text = text.Replace("¨", @"\250"); //168
            text = text.Replace("©", @"\251");
            text = text.Replace("ª", @"\252");
            text = text.Replace("«", @"\253");
            text = text.Replace("¬", @"\254");
            text = text.Replace("®", @"\256");
            text = text.Replace("¯", @"\257");

            text = text.Replace("°", @"\260"); //176
            text = text.Replace("±", @"\261");
            text = text.Replace("²", @"\262");
            text = text.Replace("³", @"\263");
            text = text.Replace("´", @"\264");
            text = text.Replace("µ", @"\265");
            text = text.Replace("¶", @"\266");
            text = text.Replace("·", @"\267");

            text = text.Replace("¸", @"\270"); //184
            text = text.Replace("¹", @"\271");
            text = text.Replace("º", @"\272");
            text = text.Replace("»", @"\273");
            text = text.Replace("¼", @"\274");
            text = text.Replace("½", @"\275");
            text = text.Replace("¾", @"\276");
            text = text.Replace("¿", @"\277");

            text = text.Replace("À", @"\300"); //192
            text = text.Replace("Á", @"\301");
            text = text.Replace("Â", @"\302");
            text = text.Replace("Ã", @"\303");
            text = text.Replace("Ä", @"\304");
            text = text.Replace("Å", @"\305");
            text = text.Replace("Æ", @"\306");
            text = text.Replace("Ç", @"\307");

            text = text.Replace("È", @"\310"); //200
            text = text.Replace("É", @"\311");
            text = text.Replace("Ê", @"\312");
            text = text.Replace("Ë", @"\313");
            text = text.Replace("Ì", @"\314");
            text = text.Replace("Í", @"\315");
            text = text.Replace("Î", @"\316");
            text = text.Replace("Ï", @"\317");

            text = text.Replace("Ð", @"\320"); //208
            text = text.Replace("Ñ", @"\321");
            text = text.Replace("Ò", @"\322");
            text = text.Replace("Ó", @"\323");
            text = text.Replace("Ô", @"\324");
            text = text.Replace("Õ", @"\325");
            text = text.Replace("Ö", @"\326");
            text = text.Replace("×", @"\327");

            text = text.Replace("Ø", @"\330"); //216
            text = text.Replace("Ù", @"\331");
            text = text.Replace("Ú", @"\332");
            text = text.Replace("Û", @"\333");
            text = text.Replace("Ü", @"\334");
            text = text.Replace("Ý", @"\335");
            text = text.Replace("Þ", @"\336");
            text = text.Replace("ß", @"\337");

            text = text.Replace("à", @"\340"); //224
            text = text.Replace("á", @"\341");
            text = text.Replace("â", @"\342");
            text = text.Replace("ã", @"\343");
            text = text.Replace("ä", @"\344");
            text = text.Replace("å", @"\345");
            text = text.Replace("æ", @"\346");
            text = text.Replace("ç", @"\347");

            text = text.Replace("è", @"\350"); //232
            text = text.Replace("é", @"\351");
            text = text.Replace("ê", @"\352");
            text = text.Replace("ë", @"\353");
            text = text.Replace("ì", @"\354");
            text = text.Replace("í", @"\355");
            text = text.Replace("î", @"\356");
            text = text.Replace("ï", @"\357");

            text = text.Replace("ð", @"\360"); //240
            text = text.Replace("ñ", @"\361");
            text = text.Replace("ò", @"\362");
            text = text.Replace("ó", @"\363");
            text = text.Replace("ô", @"\364");
            text = text.Replace("õ", @"\365");
            text = text.Replace("ö", @"\366");
            text = text.Replace("÷", @"\367");

            text = text.Replace("ø", @"\370"); //248
            text = text.Replace("ù", @"\371");
            text = text.Replace("ú", @"\372");
            text = text.Replace("û", @"\373");
            text = text.Replace("ü", @"\374");
            text = text.Replace("ý", @"\375");
            text = text.Replace("þ", @"\376");
            text = text.Replace("ÿ", @"\377");
            return text;
        }
    }
}