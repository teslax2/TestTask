using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Reader
{
    internal class EntriesReader : IEntriesReader
    {
        private readonly StreamReader _streamReader;
        private readonly int _bufferSize;

        public EntriesReader(string path, int bufferSize = 81920)
        {
            _bufferSize = bufferSize;
            _streamReader = new StreamReader(File.OpenRead(path), bufferSize: _bufferSize);
        }

        public EntriesReader(Stream stream, int bufferSize = 81920)
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
                    var line = await _streamReader.ReadLineAsync();
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
