

using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Generator;

namespace WUrban.TestTask.Generator.Args
{
    internal class GenerateFileCommand : ICommand
    {
        public const string Name = "Generate";
        public uint SizeInBytes { get; set; }
        private IExecutor<GenerateFileCommand> _executor;

        public GenerateFileCommand(uint sizeInBytes, IExecutor<GenerateFileCommand> excutor)
        {
            SizeInBytes = sizeInBytes;
            _executor = excutor;
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
            await _executor.ExecuteAsync(this);
            Console.WriteLine($"{DateTime.Now} File generated.");
        }
    }
}
