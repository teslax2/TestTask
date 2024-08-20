using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core
{
    internal class PartitionStore : IPartitionStore
    {
        public async Task<Partition> Save(Entry[] entries)
        {
            var fileName = Path.GetTempFileName();
            await File.WriteAllLinesAsync(fileName, entries.Select(x => x.ToString()));
            return fileName;
        }

        public StreamReader GetStreamReader(Partition partition, int bufferSize = 81920)
        {
            return new StreamReader(File.OpenRead(partition), bufferSize: bufferSize);
        }
    }
}
