using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Generator;
using WUrban.TestTask.Sorter.Sorters;

namespace WUrban.TestTask.Sorter.Commands
{
    internal class SortCommand : ICommand
    {
        public const string Name = "Sort";
        public string CommandName => Name;

        public string Description => "";

        public string Path {  get; set; }
        public IExecutor<SortCommand> Executor { get; }

        public SortCommand(string path, IExecutor<SortCommand> executor) 
        {
            Path = path;
            Executor = executor;
        }

        public static SortCommand? ParseFromArgs(string[] args)
        {
            if (args[0].Equals(Name, StringComparison.OrdinalIgnoreCase)
                && args[1].StartsWith("--path="))
            {
                return new SortCommand(args[1][7..], new BigFileSorterExecutor());
            }
            return null;
        }

        public async Task ExecuteAsync()
        {
            await Executor.ExecuteAsync(this);
        }
    }
}
