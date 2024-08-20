using System.Buffers;
using System.Collections;
using System.Text;
using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter
{
    internal class EntriesReader : IEnumerable<Entry>
    {
        private readonly StreamReader _streamReader;
        private readonly int _bufferSize = 8190;

        public EntriesReader(string path)
        {
            _streamReader = new StreamReader(File.OpenRead(path), bufferSize: _bufferSize);
        }

        public IEnumerator<Entry> GetEnumerator()
        {
            var arrayPool = ArrayPool<char>.Shared.Rent(_bufferSize);
            var sb = new StringBuilder();
            try
            {
                while (true)
                {
                    var count = _streamReader.ReadBlock(arrayPool, 0, _bufferSize);
                    if (count == 0) break;

                    for (var i = 0; i < count; i++)
                    {
                        if (arrayPool[i] == '\n')
                        {
                            var entryString = sb.ToString();
                            if (!string.IsNullOrWhiteSpace(entryString))
                            {
                                yield return Entry.Parse(entryString);
                            }
                            sb.Clear();
                        }
                        else if (arrayPool[i] != '\r')
                        {
                            sb.Append(arrayPool[i]);
                        }
                    }
                }
            }
            finally
            {
                ArrayPool<char>.Shared.Return(arrayPool);
                _streamReader.Dispose();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
