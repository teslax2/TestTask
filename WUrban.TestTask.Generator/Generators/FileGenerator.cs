using WUrban.TestTask.Generator.Generator;

namespace WUrban.TestTask.Generator.Generators
{
    internal class FileGenerator : IGenerator
    {
        private readonly int _bufferSize;
        private int _progress = 0;

        public FileGenerator(int bufferSize = 91200)
        {
            _bufferSize = bufferSize;
        }

        public async Task GenerateAsync(long sizeInBytes, string output) 
            => await GenerateAsync(sizeInBytes, File.Open(output,FileMode.Truncate));

        public async Task GenerateAsync(long sizeInBytes, Stream stream)
        {
            using var streamWriter = new StreamWriter(stream, bufferSize: _bufferSize);
            long _currentSize = 0;
            while (_currentSize < sizeInBytes)
            {
                var entry = EntryGenerator.GenerateEntry();
                _currentSize += entry.Size();
                await streamWriter.WriteLineAsync(entry.ToString());
                ReportProgress(_currentSize, sizeInBytes);
            }
        }

        private void ReportProgress(long currentSize, long sizeInBytes)
        {
            var progress = (int)((double)currentSize / sizeInBytes * 100);
            if (progress >= _progress + 5)
            {
                _progress = progress;
                Console.WriteLine($"Progress: {_progress}%");
            }
        }
    }
}
