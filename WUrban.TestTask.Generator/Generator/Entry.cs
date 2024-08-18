
namespace WUrban.TestTask.Generator.Generator
{
    internal record Entry(int Sequence, string Text)
    {
        public int Size => System.Text.Encoding.UTF8.GetByteCount(Text);
        public override string ToString()
        {
            return $"{Sequence}. {Text}";
        }
    }
}
