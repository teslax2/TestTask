
namespace WUrban.TestTask.Generator.Generator
{
    public record Entry(int Sequence, string Text) : IComparable<Entry>
    {
        public int Size => System.Text.Encoding.UTF8.GetByteCount($"{ToString()}");

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
            return $"{Sequence}. {Text}.{Environment.NewLine}";
        }
    }
}
