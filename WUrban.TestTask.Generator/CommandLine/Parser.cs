using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Args;
using WUrban.TestTask.Generator.Commands;

namespace WUrban.TestTask.Generator.CommandLine
{
    internal static class Parser
    {
        public static ICommand Parse(string[] args)
        {
            return GenerateFileCommand.ParseFromArgs(args)
                ?? new HelpCommand();
        }
    }
}
