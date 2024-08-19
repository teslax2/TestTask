using WUrban.TestTask.Generator.Generator;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter
{
    internal class Partition
    {
        private readonly StreamReader _streamReader;
        private readonly long _maxSize;
        private long _initPosition;

        public Partition(StreamReader stream, long maxSize) {
            _streamReader = stream;
            _maxSize = maxSize;
        }

        public async IAsyncEnumerable<Entry> GetEntriesAsync()
        {
            _initPosition = _streamReader.BaseStream.Position;
            while (!_streamReader.EndOfStream && _streamReader.BaseStream.Position < _initPosition + _maxSize)
            {
                var line = await _streamReader.ReadLineAsync();
                yield return Entry.Parse(line);
            }
        }

        public IEnumerable<Entry> GetEntries()
        {
            _initPosition = _streamReader.BaseStream.Position;
            while (!_streamReader.EndOfStream && _streamReader.BaseStream.Position < _initPosition + _maxSize)
            {
                var line = _streamReader.ReadLine();
                yield return Entry.Parse(line);
            }
        }
    }

    internal static class PartitionExtensions
    {
        public static async Task<string> OrderAndStoreExternalyAsync(this IEnumerable<Entry> entries)
        {
            var ordered = entries.OrderBy(x => x);
            var tmpFile = Path.GetTempFileName();
            using var stream = File.OpenWrite(tmpFile);
            using var writer = new StreamWriter(stream, bufferSize: 8192);
            foreach (var entry in ordered)
            {
                await writer.WriteLineAsync(entry.ToString());
            }
            return await Task.FromResult(tmpFile);
        }
    }
}
