using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Generator;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Reader;

namespace WUrban.TestTask.Sorter.Commands
{
    internal class SortCommand : ICommand
    {
        public const string Name = "Sort";
        public string Path {  get; }
        public string Output { get; }
        private readonly IExecutor<SortCommand> _executor;

        public SortCommand(string path, string output, IExecutor<SortCommand> executor) 
        {
            ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));
            ArgumentException.ThrowIfNullOrEmpty(output, nameof(output));
            Path = path;
            Output = output;
            _executor = executor;
        }

        public static ICommand? ParseFromArgs(string[] args)
        {
            if (args.Length < 2)
            {
                return null;
            }
            if (args[0].Equals(Name, StringComparison.OrdinalIgnoreCase)
                && args[1].StartsWith("--path="))
            {
                var path = args[1][7..];
                if (args.Length == 3 && args[2].StartsWith("--output="))
                {
                    return new SortCommand(path, args[2][9..], new SortCommandExecutor(new BigFileSorter(new EntriesReader(path))));
                }
                return new SortCommand(path, "output.txt", new SortCommandExecutor(new BigFileSorter(new EntriesReader(path))));
            }
            return null;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine($"{DateTime.Now} Sorting file {Path}...");
            await _executor.ExecuteAsync(this);
            Console.WriteLine($"{DateTime.Now} File sorted.");
        }
    }
}
