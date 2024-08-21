namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core
{
    public record Partition(string Path)
    {
        public static implicit operator Partition(string path) => new(path);
        public static implicit operator string(Partition partition) => partition.Path;
    }
}
