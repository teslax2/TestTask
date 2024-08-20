using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Args;

namespace WUrban.TestTask.Generator.CommandLine
{
    internal static class Parser
    {
        public static ICommand Parse(string[] args)
        {
            return GenerateFileCommand.ParseFromArgs(args)
                ?? throw new InvalidCommandException("No command or command not supported");
        }
    }
}
