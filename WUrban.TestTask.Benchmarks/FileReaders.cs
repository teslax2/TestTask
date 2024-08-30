using BenchmarkDotNet.Attributes;
using System.Buffers;
using System.Text;

namespace WUrban.TestTask.Benchmarks
{
    [MemoryDiagnoser]
    public class FileReaders
    {
        private const string _filePath = @"C:\temp\test.txt";


        //[Benchmark]
        //public void ReadFileLineByLine()
        //{
        //    var lines = ReadFileLineByLineImpl();
        //    foreach (var line in lines)
        //    {
        //        // do nothing
        //    }
        //}
        //public IEnumerable<string> ReadFileLineByLineImpl() 
        //{ 
        //    var stream = File.OpenRead(_filePath);
        //    var reader = new StreamReader(stream);
        //    while (!reader.EndOfStream)
        //    {
        //        var line = reader.ReadLine();
        //        if (line != null)
        //        {
        //            yield return line;
        //        }
        //    }
        //}

        //[Benchmark]
        //public void ReadFileLineByLineWithBuffer()
        //{
        //    var lines = ReadFileLineByLineWithBufferImpl();
        //    foreach (var line in lines)
        //    {
        //        // do nothing
        //    }
        //}

        //public IEnumerable<string> ReadFileLineByLineWithBufferImpl()
        //{
        //    var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 40960);
        //    var reader = new StreamReader(stream);
        //    while (!reader.EndOfStream)
        //    {
        //        var line = reader.ReadLine();
        //        if (line != null)
        //        {
        //            yield return line;
        //        }
        //    }
        //}

        //[Benchmark]
        //public void ReadFileLineByBufferOnReader()
        //{
        //    var lines = ReadFileLineByBufferOnReaderImpl();
        //    foreach (var line in lines)
        //    {
        //        // do nothing
        //    }
        //}

        //public IEnumerable<string> ReadFileLineByBufferOnReaderImpl()
        //{
        //    var stream = File.OpenRead(_filePath);
        //    var reader = new StreamReader(stream, bufferSize: 40960);
        //    while (!reader.EndOfStream)
        //    {
        //        var line = reader.ReadLine();
        //        if (line != null)
        //        {
        //            yield return line;
        //        }
        //    }
        //}

        [Benchmark]
        public async Task ReadFileAsync()
        {
            var lines = ReadFileAsyncImpl();
            await foreach (var line in lines)
            {
                // do nothing
            }
        }

        public async IAsyncEnumerable<string> ReadFileAsyncImpl()
        {
            var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 40960);
            var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (line != null)
                {
                    yield return line;
                }
            }
        }

    }
}
