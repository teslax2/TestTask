
namespace WUrban.TestTask.Generator.Args
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
