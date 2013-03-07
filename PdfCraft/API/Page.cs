namespace PdfCraft.API
{
    public class Page
    {
        private readonly PageObject _page;

        internal Page(PageObject page)
        {
            _page = page;
        }

        public void AddTextBox(TextBox textbox)
        {
            _page.AddTextBox(textbox);
        }

        public void AddCanvas(GraphicsCanvas canvas)
        {
            _page.AddCanvas(canvas);
        }
    }
}