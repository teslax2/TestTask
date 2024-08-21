using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Merger;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Partitioning;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Reader;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter
{
    internal class BigFileSorter : ISorter
    {
        private readonly IEntriesReader _entriesReader;

        public BigFileSorter(IEntriesReader entriesReader)
        {
            _entriesReader = entriesReader;
        }

        public async Task SortAsync(string inputPath, string outputPath)
        {
            ArgumentException.ThrowIfNullOrEmpty(inputPath, nameof(inputPath));
            ArgumentException.ThrowIfNullOrEmpty(outputPath, nameof(outputPath));

            await _entriesReader
            .GetEntriesAsync()
            .Partition()
            .Merge(outputPath);
        }
    }
}
