using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter
{
    internal interface IPartitionStore
    {
        Task<Partition> Save(Entry[] entries);
    }
}