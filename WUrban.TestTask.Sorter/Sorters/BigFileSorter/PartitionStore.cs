using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter
{
    internal class PartitionStore : IPartitionStore
    {
        public async Task<Partition> Save(Entry[] entries)
        {
            var fileName = Path.GetTempFileName();
            await File.WriteAllLinesAsync(fileName, entries.Select(x => x.ToString()));
            return fileName;
        }
    }
}
