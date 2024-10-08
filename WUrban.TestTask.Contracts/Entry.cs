﻿namespace WUrban.TestTask.Contracts
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

            var textComparisionResult = string.Compare(Text, other.Text,StringComparison.CurrentCultureIgnoreCase);

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

        public static Entry Parse(string? text)
        {       
            try
            {
                ArgumentNullException.ThrowIfNull(text, nameof(text));
                var dotIndex = text?.IndexOf('.') ?? throw new EntryException("No dot found");
                var sentenceIndex = dotIndex + 2;
                return new Entry(int.Parse(text[..dotIndex]), text[sentenceIndex..]);
            }
            catch (Exception ex)
            {
                throw new EntryException($"Invalid entry format: {text}", ex);
            }
        }
    }
}
