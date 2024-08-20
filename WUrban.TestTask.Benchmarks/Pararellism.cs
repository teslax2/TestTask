using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Benchmarks
{
    [MemoryDiagnoser]
    public class Pararellism
    {
        private const string _filePath = @"C:\Users\wiurban\source\repos\TestTask\WUrban.TestTask\WUrban.TestTask.Generator\bin\Debug\net8.0\output.txt";

        [Benchmark(Baseline = true)]
        [IterationCount(3)]
        [WarmupCount(1)]
        public void QueueSort()
        {
            var lines = ReadFileLineByLineImpl();
            var queue = new Queue<Entry>();
            foreach (var line in lines)
            {
                queue.Enqueue(line);
            }
            var array = queue.ToArray();
            Array.Sort(array);
        }

        public IEnumerable<Entry> ReadFileLineByLineImpl()
        {
            var stream = File.OpenRead(_filePath);
            var reader = new StreamReader(stream, bufferSize: 81920);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    yield return Entry.Parse(line);
                }
            }
        }
    }
}
