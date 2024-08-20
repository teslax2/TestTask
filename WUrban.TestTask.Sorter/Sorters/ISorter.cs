
namespace WUrban.TestTask.Sorter.Sorters
{
    internal interface ISorter
    {
        Task SortAsync(string inputPath, string outputPath);
    }
}
