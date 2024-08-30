using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core
{
    public interface IPartitionStore
    {
        Task<Partition> Save(IEnumerable<Entry> entries);
        StreamReader GetStreamReader(Partition partition, int bufferSize = 81920);
    }
}