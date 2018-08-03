using System;
using System.Drawing;
using NUnit.Framework;
using PdfCraft.API;
using PdfCraft.Fonts;
using Tests.Resources.Fonts;

namespace Tests.Concerning_TrueType_Fonts
{
    [TestFixture]
    public class When_a_Textbox_is_created_using_a_truetype_font : BaseTest
    {
        private Document document;
        private Page page;
        private FontProperties arialRegularfontProperties;
        private FontProperties arialBoldfontProperties;
        private FontProperties arialBoldItalicfontProperties;
        private FontProperties arialItalicfontProperties;
        private FontProperties arialBlackfontProperties;
        private FontProperties linuxLibertineRegularfontProperties;
        private FontProperties flandersArtSansRegularfontProperties;

        public override void Arrange()
        {
            document = new Document();
            page = document.AddPage();

            arialRegularfontProperties = new FontProperties("Arial", 10, FontResource.GetArial_Regular());
            arialBoldfontProperties = new FontProperties("ArialBold", 10, FontResource.GetArial_Bold());
            arialBoldItalicfontProperties = new FontProperties("ArialBoldItalic", 10, FontResource.GetArial_BoldItalic());
            arialItalicfontProperties = new FontProperties("ArialItalic", 10, FontResource.GetArial_Italic());
            arialBlackfontProperties = new FontProperties("ArialBlack", 10, FontResource.GetArial_Black());
            linuxLibertineRegularfontProperties = new FontProperties("LinuxLibertineRegular", 10, FontResource.GetLinuxLibertine_Regular());
            flandersArtSansRegularfontProperties = new FontProperties("FlandersArtSansRegular", 10, FontResource.GetFlandersArtSans_Regular());
        }

        public override void Act()
        {
            var textBox = document.CreateTextBox(new Rectangle(20, 20, 100, 1));
            textBox.SetColor(Color.Black);

            // Arial gebruikt kennelijk een afwijkend Ttf-formaat... todo: te onderzoeken !
            textBox.SetFont(arialRegularfontProperties);
            textBox.AddText("Dit is tekst in een truetype font (Arial regular)");
            //textBox.SetFont(arialBoldfontProperties);
            //textBox.AddText("Dit is tekst in een truetype font (Arial bold)");
            //textBox.SetFont(arialBoldItalicfontProperties);
            //textBox.AddText("Dit is tekst in een truetype font (Arial bold italic)");
            //textBox.SetFont(arialItalicfontProperties);
            //textBox.AddText("Dit is tekst in een truetype font (Arial italic)");
            //textBox.SetFont(arialBlackfontProperties);
            //textBox.AddText("Dit is tekst in een truetype font (Arial black)");

            // deze geeft geen fout, maar lijkt ook niet te werken ...
            textBox.SetFont(linuxLibertineRegularfontProperties);
            textBox.AddText("Dit is tekst in een truetype font (Linux libertine Regular)");

            textBox.SetFont(flandersArtSansRegularfontProperties);
            textBox.AddText("Dit is tekst in een truetype font (Flanders Art Sans Regular)");

            textBox.Calculate();

            page.AddTextBox(textBox);
        }

        [Test]
        public void Then_it_should_use_the_truetype_font_in_the_document()
        {
            var result = document.Generate();

            DumpToFile(result, $"c:\\temp\\TEST_{Guid.NewGuid()}.PDF");
        }
    }
}