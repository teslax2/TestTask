using System.Buffers;
using System.Net.NetworkInformation;
using WUrban.TestTask.Contracts;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Partitioning
{
    internal class Partitioner : IPartitioner
    {
        private readonly IPartitionStore _partitionStore;
        private readonly int _maxPartitionSize;
        private int _partitionNumber = 0;
        private readonly SemaphoreSlim _semaphore = new(2, 2);

        public Partitioner(IPartitionStore partitionStore, int maxPartitionSize = 500_000_000)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(maxPartitionSize, 10_000_000);
            _partitionStore = partitionStore;
            _maxPartitionSize = maxPartitionSize;
        }

        public async IAsyncEnumerable<Partition> PartitionAsync(IAsyncEnumerable<Entry> entries)
        {
            var queue = new Queue<Entry>(1_000_000);
            var saveTasks = new List<Task<Partition>>();
            double size = 0;
            await foreach (var entry in entries)
            {
                queue.Enqueue(entry);
                size += entry.Size();
                if (size >= _maxPartitionSize)
                {
                    var array = await CopyToArray(queue).ConfigureAwait(false);
                    var partition = SortAndSavePartition(array, size, _partitionNumber);
                    saveTasks.Add(partition);
                    size = 0;
                }
            }
            // if entries stream is over but there are still entries in the queue
            if (size > 0)
            {
                var array = await CopyToArray(queue).ConfigureAwait(false);
                var partition = SortAndSavePartition(array, size, _partitionNumber);
                saveTasks.Add(partition);
            }
            foreach (var task in saveTasks)
            {
                yield return await task;
            }
        }

        private async Task<Entry[]> CopyToArray(Queue<Entry> queue)
        {
            await _semaphore.WaitAsync().ConfigureAwait(false);
            var array = queue.ToArray();
            queue.Clear();
            _partitionNumber++;
            return array;
        }

        private async Task<Partition> SortAndSavePartition(Entry[] array, double size, int partitionNumber)
        {
            try
            {
                await Task.Run(() => Array.Sort(array)).ConfigureAwait(false);
                return await _partitionStore.SaveAsync(array).ConfigureAwait(false);
            }
            finally
            {
                Console.WriteLine($"Sorted partition: {partitionNumber} of size: {(size / 1_000_000_000.0)} GB");
                _semaphore.Release();
            }
        }
    }

    internal static class PartitionerExtensions
    {
        public static IAsyncEnumerable<Partition> Partition(this IAsyncEnumerable<Entry> entries)
        {
            return new Partitioner(new PartitionStore()).PartitionAsync(entries);
        }
    }
}
