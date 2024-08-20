using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Generator;
using WUrban.TestTask.Sorter.Sorters;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter;

namespace WUrban.TestTask.Sorter.Commands
{
    internal class SortCommand : ICommand
    {
        public const string Name = "Sort";
        public string Path {  get; }
        public string Output { get; }
        public IExecutor<SortCommand> Executor { get; }

        public SortCommand(string path, string output, IExecutor<SortCommand> executor) 
        {
            Path = path;
            Output = output;
            Executor = executor;
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
                if (args.Length == 3 && args[2].StartsWith("--output="))
                {
                    return new SortCommand(args[1][7..], args[2][9..], new SortCommandExecutor(new BigFileSorter()));
                }
                return new SortCommand(args[1][7..], "output.txt", new SortCommandExecutor(new BigFileSorter()));
            }
            return null;
        }

        public async Task ExecuteAsync()
        {
            await Executor.ExecuteAsync(this);
        }
    }
}
