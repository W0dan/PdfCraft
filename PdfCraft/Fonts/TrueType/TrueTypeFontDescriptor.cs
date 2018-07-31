using PdfCraft.Constants;
using PdfCraft.Containers;

namespace PdfCraft.Fonts.TrueType
{
    public class TrueTypeFontDescriptor : BasePdfObject
    {
        private readonly PdfFontDefinition fontDefinition;
        private TrueTypeFontFile2 fontFile2;

        public TrueTypeFontDescriptor(int objectNumber, PdfFontDefinition fontDefinition) : base(objectNumber)
        {
            this.fontDefinition = fontDefinition;
        }

        public void SetFontFile(TrueTypeFontFile2 value)
        {
            this.fontFile2 = value;
            base.ChildObjects.Add(value);
        }

        public override IByteContainer Content
        {
            get
            {
                var content = ByteContainerFactory
                    .CreateByteContainer($"<<{StringConstants.NewLine}" +
                                         $"/Type /FontDescriptor{StringConstants.NewLine}" +
                                         $"/FontName /{fontDefinition.FontName}{StringConstants.NewLine}" +
                                         $"/StemV 80{StringConstants.NewLine}" +
                                         $"/Descent {fontDefinition.Descender}{StringConstants.NewLine}" +
                                         $"/Ascent {fontDefinition.Ascender}{StringConstants.NewLine}" +
                                         $"/ItalicAngle {fontDefinition.ItalicAngle}{StringConstants.NewLine}" +
                                         $"/CapHeight {fontDefinition.CapHeight}{StringConstants.NewLine}" +
                                         $"/Flags 32{StringConstants.NewLine}" +
                                         $"/FontFile2 {fontFile2.Number} 0 R{StringConstants.NewLine}" +
                                         $"/FontBBox [{fontDefinition.FontBBox[0]} {fontDefinition.FontBBox[1]} {fontDefinition.FontBBox[2]} {fontDefinition.FontBBox[3]}]{StringConstants.NewLine}" +
                                         $">>");

                SetContent(content);

                return base.Content;
            }
        }
    }
}