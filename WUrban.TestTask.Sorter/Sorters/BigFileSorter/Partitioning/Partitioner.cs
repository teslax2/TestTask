using WUrban.TestTask.Contracts;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Partitioning
{
    internal class Partitioner : IPartitioner
    {
        private readonly IPartitionStore _partitionStore;
        private readonly int _maxPartitionSize;

        public Partitioner(IPartitionStore partitionStore, int maxPartitionSize = 100_000_000)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(maxPartitionSize, 10_000_000);
            _partitionStore = partitionStore;
            _maxPartitionSize = maxPartitionSize;
        }

        public async IAsyncEnumerable<Partition> PartitionAsync(IAsyncEnumerable<Entry> entries)
        {
            var queue = new Queue<Entry>();
            int size = 0;
            await foreach (var entry in entries)
            {
                queue.Enqueue(entry);
                size += entry.Size();
                if (size >= _maxPartitionSize)
                {
                    var array = queue.ToArray();
                    Array.Sort(array);
                    var file = await _partitionStore.Save(array);
                    yield return file;
                    queue.Clear();
                    size = 0;
                }
            }
            // if entries stream is over but there are still entries in the queue
            if (size > 0)
            {
                var array = queue.ToArray();
                Array.Sort(array);
                var file = await _partitionStore.Save(array);
                yield return file;
                queue.Clear();
            }
        }
    }

    internal static class PartitionerExtensions
    {
        public static IAsyncEnumerable<Partition> Partition(this IAsyncEnumerable<Entry> entries, int maxPartitionSize = 100_000_000)
        {
            return new Partitioner(new PartitionStore(), maxPartitionSize).PartitionAsync(entries);
        }
    }
}
