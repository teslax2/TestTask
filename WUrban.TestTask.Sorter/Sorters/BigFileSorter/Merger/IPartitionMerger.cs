using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Merger
{
    internal interface IPartitionMerger
    {
        Task MergePartitionsAsync(IAsyncEnumerable<Partition> partitions, string outputFileName);
    }
}