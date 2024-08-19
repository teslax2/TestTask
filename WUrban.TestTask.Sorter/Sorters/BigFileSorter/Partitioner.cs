using WUrban.TestTask.Sorter.Commands;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter
{
    internal class Partitioner : IDisposable
    {
        public string Path { get; }
        public long MaxPartitionSize { get; }
        private Stream _stream;
        private StreamReader _reader;

        public Partitioner(string path, long maxPartitionSize = 100_000_000)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(maxPartitionSize, 10_000_000);
            Path = path;
            MaxPartitionSize = maxPartitionSize;
            _stream = File.OpenRead(Path);
            _reader = new StreamReader(_stream, bufferSize: 10_000_000);
        }

        public IEnumerable<Partition> GetPartitions()
        {
            var count = GetPartitionsCount(Path);
            for (var i = 0; i < count; i++) {
                yield return new Partition(_reader, MaxPartitionSize);
            }
        }

        private long GetPartitionsCount(string path)
        {
            var fileInfo = new FileInfo(path);
            return fileInfo.Length / MaxPartitionSize;
        }

        public void Dispose()
        {
            _reader?.Dispose();
            _stream?.Dispose();
        }
    }
}
