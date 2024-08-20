using WUrban.TestTask.Contracts;

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
        public static async Task<string> OrderAndStoreExternalyAsync(this List<Entry> entries)
        {
            var ordered = entries.Order();
            var tmpFile = Path.GetTempFileName();
            await File.WriteAllLinesAsync(tmpFile, ordered.Select(x => x.ToString()));
            return tmpFile;
        }
    }
}
