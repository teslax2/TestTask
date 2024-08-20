using BenchmarkDotNet.Running;

namespace WUrban.TestTask.Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var results = BenchmarkRunner.Run<Sorters>();
        }
    }
}

