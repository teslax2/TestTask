using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Reader
{
    internal interface IEntriesReader
    {
        IAsyncEnumerable<Entry> GetEntriesAsync();
    }
}