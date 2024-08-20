namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core
{
    internal record Partition(string path)
    {
        public static implicit operator Partition(string path) => new(path);
        public static implicit operator string(Partition partition) => partition.path;
    }
}
