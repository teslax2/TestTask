
namespace WUrban.TestTask.Generator.Generator
{
    internal record Entry(int Sequence, string Text)
    {
        public int Size => System.Text.Encoding.UTF8.GetByteCount($"{ToString()}");
        public override string ToString()
        {
            return $"{Sequence}. {Text}.{Environment.NewLine}";
        }
    }
}
