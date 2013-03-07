namespace PdfCraft.Graphics
{
    internal class GraphicsCommand
    {
        private readonly Command _command;
        private readonly object _data;

        public GraphicsCommand(Command command, object data)
        {
            _command = command;
            _data = data;
        }

        public Command Command
        {
            get { return _command; }
        }

        public object Data
        {
            get { return _data; }
        }
    }
}