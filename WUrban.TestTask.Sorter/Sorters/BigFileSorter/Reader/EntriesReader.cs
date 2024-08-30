using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Reader
{
    internal class EntriesReader : IEntriesReader
    {
        private readonly StreamReader _streamReader;
        private readonly int _bufferSize;

        public EntriesReader(string path, int bufferSize = 40960)
        {
            _bufferSize = bufferSize;
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: _bufferSize);
            _streamReader = new StreamReader(stream, bufferSize: _bufferSize);
        }
        // for testing purposes
        public EntriesReader(Stream stream, int bufferSize = 40960)
        {
            _bufferSize = bufferSize;
            _streamReader = new StreamReader(stream, bufferSize: _bufferSize);
        }

        public async IAsyncEnumerable<Entry> GetEntriesAsync()
        {
            try
            {
                while (!_streamReader.EndOfStream)
                {
                    var line = await _streamReader.ReadLineAsync().ConfigureAwait(false);
                    if (line != null)
                    {
                        yield return Entry.Parse(line);
                    }
                }
            }
            finally
            {
                _streamReader.Close();
            }
        }
    }
}
