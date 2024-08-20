using WUrban.TestTask.Generator.Generator;
using WUrban.TestTask.Sorter.Commands;

namespace WUrban.TestTask.Sorter.Sorters
{
    internal class SortCommandExecutor : IExecutor<SortCommand>
    {
        private readonly ISorter _sorter;

        public SortCommandExecutor(ISorter sorter)
        {
            _sorter = sorter;
        }

        public async Task ExecuteAsync(SortCommand command)
        {
            await _sorter.SortAsync(command.Path, command.Output);
        }
    }
}
