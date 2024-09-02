using WUrban.TestTask.Contracts;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Merger
{
    internal class PartitionMerger : IPartitionMerger
    {
        private readonly IPartitionStore _partitionStore;
        private readonly int _bufferSize;

        public PartitionMerger(IPartitionStore partitionStore, int bufferSize = 40960)
        {
            _partitionStore = partitionStore;
            _bufferSize = bufferSize;
        }
        public async Task MergePartitionsAsync(IAsyncEnumerable<Partition> partitions, string outputFileName)
        {
            using var outputStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write, FileShare.None, _bufferSize, FileOptions.Asynchronous);
            using var writer = new StreamWriter(outputStream, bufferSize: _bufferSize);
            await MergePartitionsBaseAsync(partitions, writer).ConfigureAwait(false);
        }
        // for testing purposes
        public async Task MergePartitionsAsync(IAsyncEnumerable<Partition> partitions, Stream stream)
        {
            using var writer = new StreamWriter(stream, bufferSize: _bufferSize);
            await MergePartitionsBaseAsync(partitions, writer).ConfigureAwait(false);
        }

        private async Task MergePartitionsBaseAsync(IAsyncEnumerable<Partition> partitions, StreamWriter writer)
        {
            Console.WriteLine("Merging partitions...");
            var queue = new PriorityQueue<EntryStreamReader,Entry>();
            var partitionList = new List<Partition>();
            await foreach (var partition in partitions)
            {
                var reader = _partitionStore.GetStreamReader(partition);
                partitionList.Add(partition);
                var entry = Entry.Parse(await reader.ReadLineAsync().ConfigureAwait(false));
                queue.Enqueue(new EntryStreamReader(entry, reader),entry);
            }
            while(queue.TryDequeue(out var entryReader,out _))
            {
                await writer.WriteLineAsync(entryReader.Entry.ToString()).ConfigureAwait(false);
                if(entryReader.Reader.EndOfStream) continue;
                var nextEntry = Entry.Parse(await entryReader.Reader.ReadLineAsync().ConfigureAwait(false));
                queue.Enqueue(new EntryStreamReader(nextEntry, entryReader.Reader),nextEntry);
            }
            CleanUp(partitionList);
        }

        private void CleanUp(IEnumerable<Partition> partitions)
        {
            foreach (var partition in partitions)
            {
                _partitionStore.RemoveFile(partition);
            }
        }
    }

    internal static class PartitionMergerExtensions
    {
        public static async Task Merge(this IAsyncEnumerable<Partition> partitions, string outputFileName)
        {
            await new PartitionMerger(new PartitionStore()).MergePartitionsAsync(partitions, outputFileName).ConfigureAwait(false);
        }
    }
}
