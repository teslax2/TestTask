

using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Commands;
using WUrban.TestTask.Generator.Generator;
using WUrban.TestTask.Generator.Generators;

namespace WUrban.TestTask.Generator.Args
{
    internal class GenerateFileCommand : ICommand
    {
        public const string Name = "Generate";
        public int SizeInBytes { get; }
        public string Output { get; }
        private readonly IExecutor<GenerateFileCommand> _executor;

        public GenerateFileCommand(int sizeInBytes, string output, IExecutor<GenerateFileCommand> excutor)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(sizeInBytes, 0, nameof(sizeInBytes));
            ArgumentException.ThrowIfNullOrWhiteSpace(output, nameof(output));
            SizeInBytes = sizeInBytes;
            Output = output;
            _executor = excutor;
        }

        public static ICommand? ParseFromArgs(string[] args)
        {
            if (args.Length >=2
                && args[0].Equals(Name, StringComparison.OrdinalIgnoreCase)
                && args[1].StartsWith("--size=")
                && int.TryParse(args[1][7..], out var sizeInBytes))
            {
                if (args.Length >= 3 && args[2].StartsWith("--output="))
                {
                    return new GenerateFileCommand(sizeInBytes, args[2][9..], new GenerateFileCommandExecutor(new FileGenerator()));
                }
                return new GenerateFileCommand(sizeInBytes, "output.txt" , new GenerateFileCommandExecutor(new FileGenerator()));
            }
            return null;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine($"{DateTime.Now} Generating file with size {SizeInBytes} bytes...");
            await _executor.ExecuteAsync(this);
            Console.WriteLine($"{DateTime.Now} File generated.");
        }
    }
}
