using WUrban.TestTask.Contracts;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Merger
{
    internal class PartitionMerger : IPartitionMerger
    {
        private readonly IPartitionStore _partitionStore;
        private readonly int _bufferSize;

        public PartitionMerger(IPartitionStore partitionStore, int bufferSize = 81920)
        {
            _partitionStore = partitionStore;
            _bufferSize = bufferSize;
        }
        public async Task MergePartitionsAsync(IAsyncEnumerable<Partition> partitions, string outputFileName)
        {
            using var outputStream = File.Open(outputFileName, FileMode.Create);
            using var writer = new StreamWriter(outputStream, bufferSize: _bufferSize);
            await MergePartitionsBaseAsync(partitions, writer);
        }
        // for testing purposes
        public async Task MergePartitionsAsync(IAsyncEnumerable<Partition> partitions, Stream stream)
        {
            using var writer = new StreamWriter(stream, bufferSize: _bufferSize);
            await MergePartitionsBaseAsync(partitions, writer);
        }

        private async Task MergePartitionsBaseAsync(IAsyncEnumerable<Partition> partitions, StreamWriter writer)
        {
            var queue = new PriorityQueue<EntryStreamReader,Entry>();
            await foreach (var partition in partitions)
            {
                var reader = _partitionStore.GetStreamReader(partition);
                var entry = Entry.Parse(await reader.ReadLineAsync());
                queue.Enqueue(new EntryStreamReader(entry, reader),entry);
            }
            while(queue.TryDequeue(out var entryReader,out _))
            {
                await writer.WriteLineAsync(entryReader.Entry.ToString());
                if(entryReader.Reader.EndOfStream) continue;
                var nextEntry = Entry.Parse(await entryReader.Reader.ReadLineAsync());
                queue.Enqueue(new EntryStreamReader(nextEntry, entryReader.Reader),nextEntry);
            }
        }
    }

    public record EntryStreamReader(Entry Entry, StreamReader Reader);

    internal static class PartitionMergerExtensions
    {
        public static async Task Merge(this IAsyncEnumerable<Partition> partitions, string outputFileName)
        {
            await new PartitionMerger(new PartitionStore()).MergePartitionsAsync(partitions, outputFileName);
        }
    }
}
