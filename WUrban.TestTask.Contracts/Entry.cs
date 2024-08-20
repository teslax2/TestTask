namespace WUrban.TestTask.Contracts
{
    public record Entry(int Sequence, string Text) : IComparable<Entry>
    {
        public int Size() => System.Text.Encoding.UTF8.GetByteCount($"{ToString()}");

        public int CompareTo(Entry? other)
        {
            if (other == null)
            {
                return 1;
            }

            var textComparisionResult = string.CompareOrdinal(Text, other.Text);

            if (textComparisionResult == 0)
            {
                return Sequence.CompareTo(other.Sequence);
            }

            return textComparisionResult;
        }

        public override string ToString()
        {
            return $"{Sequence}. {Text}";
        }

        public static Entry Parse(string text)
        {
            var dotIndex = text.IndexOf('.');
            if (dotIndex < 0)
            {
                throw new EntryException($"Invalid entry format: {text}");
            }

            try
            {
                return new Entry(int.Parse(text[..dotIndex]), text[++dotIndex..]);
            }
            catch (Exception)
            {
                return new Entry(0, string.Empty);
            }
        }
    }
}
