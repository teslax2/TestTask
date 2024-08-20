using WUrban.TestTask.Contracts;
using WUrban.TestTask.Sorter.Commands;

namespace WUrban.TestTask.Sorter.CommandLine
{
    internal static class Parser
    {
        public static ICommand Parse(string[] args)
        {
            return SortCommand.ParseFromArgs(args)
                ?? throw new InvalidCommandException("No command or command not supported");
        }
    }
}
