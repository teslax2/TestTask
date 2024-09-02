using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core
{
    public interface IPartitionStore
    {
        Task<Partition> SaveAsync(IEnumerable<Entry> entries);
        StreamReader GetStreamReader(Partition partition, int bufferSize = 81920);
        void RemoveFile(Partition partition);
    }
}