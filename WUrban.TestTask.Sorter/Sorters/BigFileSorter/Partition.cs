
namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter
{
    internal record Partition(string path)
    {
        public static implicit operator Partition(string path) => new (path);
        public static implicit operator string(Partition partition) => partition.path;
    }

    internal static class PartitionExtensions
    {
        public static StreamReader GetStreamReader(this Partition partition, int bufferSize = 81920)
        {
            return new StreamReader(File.OpenRead(partition), bufferSize: bufferSize);
        }
    }
}
