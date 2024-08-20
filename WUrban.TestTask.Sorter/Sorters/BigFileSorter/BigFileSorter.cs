﻿using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Merger;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Partitioning;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Reader;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter
{
    internal class BigFileSorter : ISorter
    {
        public async Task SortAsync(string inputPath, string outputPath)
        {
            ArgumentNullException.ThrowIfNull(inputPath, nameof(inputPath));
            ArgumentNullException.ThrowIfNull(outputPath, nameof(outputPath));

            await new EntriesReader(inputPath)
            .GetEntriesAsync()
            .Partition()
            .Merge(outputPath);
        }
    }
}
