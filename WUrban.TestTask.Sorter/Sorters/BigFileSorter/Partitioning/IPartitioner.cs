using WUrban.TestTask.Contracts;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Partitioning
{
    internal interface IPartitioner
    {
        IAsyncEnumerable<Partition> PartitionAsync(IAsyncEnumerable<Entry> entries);
    }
}