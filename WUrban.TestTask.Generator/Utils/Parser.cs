using WUrban.TestTask.Generator.Args;

namespace WUrban.TestTask.Generator.Utils
{
    internal static class Parser
    {
        public static ICommand Parse(string[] args)
        {
            return GenerateFileCommand.ParseFromArgs(args)
                ?? throw new InvalidOperationException($"Usage: {GenerateFileCommand.Name} --size=128");
        }
    }
}
