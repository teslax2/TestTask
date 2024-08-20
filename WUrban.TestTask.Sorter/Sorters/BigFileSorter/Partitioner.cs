using System.Buffers;
using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter
{
    internal class Partitioner
    {
        private readonly EntriesReader _entriesReader;
        private readonly long _maxPartitionSize;

        public Partitioner(EntriesReader entriesReader,long maxPartitionSize = 50_000_000)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(maxPartitionSize, 10_000_000);
            _entriesReader = entriesReader;
            _maxPartitionSize = maxPartitionSize;
        }

        public async Task<IEnumerable<string>> Partition()
        {
            using var enumerator = _entriesReader.GetEnumerator();
            var list = new List<Entry>(2560000);
            var partitions = new List<string>();
            int size = 0;
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Current);
                size += enumerator.Current.Size();
                if(size >= _maxPartitionSize)
                {
                    var file = await list.OrderAndStoreExternalyAsync();
                    partitions.Add(file);
                    list.Clear();
                    size = 0;
                }
            }
            return partitions;
        }
    }
}
