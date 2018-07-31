using System.Collections.Generic;

namespace PdfCraft.Fonts.TrueType
{
    public class UsedCharacterList : List<UsedCharacter>
    {
        public void AddSorted(UsedCharacter item)
        {
            if (Count == 0)
            {
                Add(item);
                return;
            }
            if (this[Count - 1].CompareTo(item) <= 0)
            {
                Add(item);
                return;
            }
            if (this[0].CompareTo(item) >= 0)
            {
                Insert(0, item);
                return;
            }
            var index = BinarySearch(item);
            if (index < 0)
                index = ~index;
            Insert(index, item);
        }
    }
}