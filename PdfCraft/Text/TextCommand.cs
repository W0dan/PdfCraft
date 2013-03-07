namespace PdfCraft.Text
{
    public class TextCommand
    {
        private readonly Command _command;
        private readonly object _data;

        public TextCommand(Command command, object data)
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