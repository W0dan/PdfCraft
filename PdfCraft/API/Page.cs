namespace PdfCraft.API
{
    public class Page
    {
        private readonly PageObject page;

        internal Page(PageObject page)
        {
            this.page = page;
        }

        public void AddTextBox(TextBox textbox)
        {
            page.AddTextBox(textbox);
        }

        public void AddCanvas(GraphicsCanvas canvas)
        {
            page.AddCanvas(canvas);
        }
    }
}