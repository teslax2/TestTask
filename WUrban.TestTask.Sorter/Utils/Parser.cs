using WUrban.TestTask.Contracts;
using WUrban.TestTask.Sorter.Commands;

namespace WUrban.TestTask.Sorter.Utils
{
    internal static class Parser
    {
        public static ICommand Parse(string[] args)
        {
            return SortCommand.ParseFromArgs(args)
                ?? throw new InvalidOperationException($"Usage: {SortCommand.Name} --path=path");
        }
    }
}
