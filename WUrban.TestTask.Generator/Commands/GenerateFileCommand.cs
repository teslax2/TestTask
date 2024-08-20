

using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Generator;

namespace WUrban.TestTask.Generator.Args
{
    internal class GenerateFileCommand : ICommand
    {
        public const string Name = "GenerateFile";
        public uint SizeInBytes { get; set; }
        public string CommandName => Name;
        public IExecutor<GenerateFileCommand> FileGenerator { get; set; }
        public string Description => $"{Name} --size=4096";

        public GenerateFileCommand(uint sizeInBytes, IExecutor<GenerateFileCommand> fileGenerator)
        {
            SizeInBytes = sizeInBytes;
            FileGenerator = fileGenerator;
        }

        public static GenerateFileCommand? ParseFromArgs(string[] args)
        {
            if (args[0].Equals(Name, StringComparison.OrdinalIgnoreCase)
                && args[1].StartsWith("--size=")
                && uint.TryParse(args[1][7..], out var sizeInBytes))
            {
                return new GenerateFileCommand(sizeInBytes,new TextGenerator());
            }
            return null;
        }

        public async Task ExecuteAsync()
        {
            await FileGenerator.ExecuteAsync(this);
        }
    }
}
