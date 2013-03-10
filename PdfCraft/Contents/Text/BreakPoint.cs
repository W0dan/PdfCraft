namespace PdfCraft.Contents.Text
{
    internal class BreakPoint
    {
        private readonly bool _hasToBreak;
        private readonly int _lengthInPoints;
        private readonly string _text;

        public BreakPoint(bool hasToBreak, int lengthInPoints, string text)
        {
            _hasToBreak = hasToBreak;
            _lengthInPoints = lengthInPoints;
            _text = text;
        }

        public bool HasToBreak
        {
            get { return _hasToBreak; }
        }

        public string Text
        {
            get { return _text; }
        }

        public int LengthInPoints
        {
            get { return _lengthInPoints; }
        }
    }
}