using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core
{
    internal interface IPartitionStore
    {
        Task<Partition> Save(Entry[] entries);
        StreamReader GetStreamReader(Partition partition, int bufferSize = 81920);
    }
}