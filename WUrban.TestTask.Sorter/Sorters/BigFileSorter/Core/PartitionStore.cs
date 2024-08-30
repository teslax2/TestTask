using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core
{
    internal class PartitionStore : IPartitionStore
    {
        public async Task<Partition> Save(IEnumerable<Entry> entries)
        {
            var fileName = Path.GetTempFileName();
            var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 40960);
            await using var writer = new StreamWriter(stream);
            foreach (var entry in entries)
            {
                await writer.WriteLineAsync(entry.ToString());
            }
            return fileName;
        }

        public StreamReader GetStreamReader(Partition partition, int bufferSize = 40960)
        {
            var stream = new FileStream(partition, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize);
            return new StreamReader(stream);
        }
    }
}
