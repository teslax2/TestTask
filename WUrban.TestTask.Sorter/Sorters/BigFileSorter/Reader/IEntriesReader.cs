using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Reader
{
    public interface IEntriesReader
    {
        IAsyncEnumerable<Entry> GetEntriesAsync();
    }
}