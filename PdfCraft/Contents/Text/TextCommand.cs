namespace PdfCraft.Contents.Text
{
    public class TextCommand
    {
        public TextCommand(Command command, object data)
        {
            Command = command;
            Data = data;
        }

        public Command Command { get; }
        public object Data { get; }
    }
}