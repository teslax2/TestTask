

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
        public string Description => $"Usage {Name} --size=4096";

        public GenerateFileCommand()
        {
            SizeInBytes = 0;
            FileGenerator = new FileGenerator();
        }

        public GenerateFileCommand(uint sizeInBytes, IExecutor<GenerateFileCommand> fileGenerator)
        {
            SizeInBytes = sizeInBytes;
            FileGenerator = fileGenerator;
        }

        public static ICommand? ParseFromArgs(string[] args)
        {
            if (args.Length == 2
                && args[0].Equals(Name, StringComparison.OrdinalIgnoreCase)
                && args[1].StartsWith("--size=")
                && uint.TryParse(args[1][7..], out var sizeInBytes))
            {
                return new GenerateFileCommand(sizeInBytes,new FileGenerator());
            }
            return null;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine($"{DateTime.Now} Generating file with size {SizeInBytes} bytes...");
            await FileGenerator.ExecuteAsync(this);
            Console.WriteLine($"{DateTime.Now} File generated.");
        }
    }
}
