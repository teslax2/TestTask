using BenchmarkDotNet.Attributes;
using System.Buffers;
using System.Text;

namespace WUrban.TestTask.Benchmarks
{
    [MemoryDiagnoser]
    public class FileReaders
    {
        private const string _filePath = @"C:\Users\wiurban\source\repos\TestTask\WUrban.TestTask\WUrban.TestTask.Generator\bin\Debug\net8.0\output.txt";


        //[Benchmark]
        public void ReadFileLineByLine()
        {
            var lines = ReadFileLineByLineImpl();
            foreach (var line in lines)
            {
                // do nothing
            }
        }
        public IEnumerable<string> ReadFileLineByLineImpl() 
        { 
            var stream = File.OpenRead(_filePath);
            var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    yield return line;
                }
            }
        }

        [Benchmark]
        public void ReadFileLineByLineWithBuffer()
        {
            var lines = ReadFileLineByLineWithBufferImpl();
            foreach (var line in lines)
            {
                // do nothing
            }
        }

        public IEnumerable<string> ReadFileLineByLineWithBufferImpl()
        {
            var stream = File.OpenRead(_filePath);
            var reader = new StreamReader(stream, bufferSize: 81920);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    yield return line;
                }
            }
        }

        [Benchmark]
        public void ReadFileLineByLineWithBufferBigger()
        {
            var lines = ReadFileLineByLineWithBufferBiggerImpl();
            foreach (var line in lines)
            {
                // do nothing
            }
        }

        public IEnumerable<string> ReadFileLineByLineWithBufferBiggerImpl()
        {
            var stream = File.OpenRead(_filePath);
            var reader = new StreamReader(stream, bufferSize: 819200);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    yield return line;
                }
            }
        }

        //[Benchmark]
        public void ReadBlock()
        {
            var lines = ReadBlockImpl();
            foreach (var line in lines)
            {
                // do nothing
            }
        }

        public IEnumerable<string> ReadBlockImpl()
        {
            var stream = File.OpenRead(_filePath);
            var reader = new StreamReader(stream, bufferSize: 81920);
            int _bufferSize = 8192;
            var arrayPool = ArrayPool<char>.Shared.Rent(_bufferSize);
            var sb = new StringBuilder();
            try
            {
                while (true)
                {
                    var count = reader.ReadBlock(arrayPool, 0, _bufferSize);
                    if (count == 0) break;

                    for (var i = 0; i < count; i++)
                    {
                        if (arrayPool[i] == '\n')
                        {
                            var entryString = sb.ToString();
                            if (!string.IsNullOrWhiteSpace(entryString))
                            {
                                yield return entryString;
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
                reader.Dispose();
            }
        }
    }
}
