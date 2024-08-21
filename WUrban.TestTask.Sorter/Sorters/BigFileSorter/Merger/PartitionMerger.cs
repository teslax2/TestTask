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
            using var outputStream = File.Open(outputFileName,FileMode.Truncate);
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
            var queue = new Dictionary<StreamReader, Entry>();
            try
            {
                queue = await InitializeQueue(partitions, _bufferSize);
                await SortAndWriteAsync(writer, queue);
            }
            finally
            {
                CleanUp(queue);
            }
        }

        private static void CleanUp(Dictionary<StreamReader, Entry> queue)
        {
            foreach (var reader in queue.Keys)
            {
                reader?.Dispose();
            }
        }

        private async Task<Dictionary<StreamReader, Entry>> InitializeQueue(IAsyncEnumerable<Partition> partitions, int bufferSize)
        {
            var readers = new List<StreamReader>();
            await foreach (var partition in partitions)
            {
                readers.Add(_partitionStore.GetStreamReader(partition));
            }

            var queue = new Dictionary<StreamReader, Entry>();
            foreach (var reader in readers)
            {
                var line = await reader.ReadLineAsync();
                var entry = Entry.Parse(line);
                queue.Add(reader, entry);
            }

            return queue;
        }
        private static async Task SortAndWriteAsync(StreamWriter writer, Dictionary<StreamReader, Entry> queue)
        {
            Console.WriteLine("Merging partitions...");
            while (queue.Count > 0)
            {
                var x = queue.MinBy(x => x.Value);
                await writer.WriteLineAsync(x.Value.ToString());
                if (x.Key.EndOfStream)
                {
                    queue.Remove(x.Key);
                    continue;
                }
                var line = await x.Key.ReadLineAsync();
                queue[x.Key] = Entry.Parse(line);
            }
        }
    }

    internal static class PartitionMergerExtensions
    {
        public static async Task Merge(this IAsyncEnumerable<Partition> partitions, string outputFileName)
        {
            await new PartitionMerger(new PartitionStore()).MergePartitionsAsync(partitions, outputFileName);
        }
    }
}
