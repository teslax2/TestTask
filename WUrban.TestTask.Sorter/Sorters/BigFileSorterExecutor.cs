using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Generator;
using WUrban.TestTask.Sorter.Commands;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter;

namespace WUrban.TestTask.Sorter.Sorters
{
    internal class BigFileSorterExecutor : IExecutor<SortCommand>
    {
        public async Task ExecuteAsync(SortCommand command)
        {
            await new EntriesReader(command.Path)
                .GetEntriesAsync()
                .Partition()
                .Merge("output.txt");
        }
    }
}
