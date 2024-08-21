using WUrban.TestTask.Generator.Generator;

namespace WUrban.TestTask.Generator.Generators
{
    internal class FileGenerator : IGenerator
    {
        private readonly int _bufferSize;

        public FileGenerator(int bufferSize = 91200)
        {
            _bufferSize = bufferSize;
        }

        public async Task GenerateAsync(int sizeInBytes, string output) 
            => await GenerateAsync(sizeInBytes, File.OpenWrite(output));

        public async Task GenerateAsync(int sizeInBytes, Stream stream)
        {
            using var streamWriter = new StreamWriter(stream, bufferSize: _bufferSize);
            int _currentSize = 0;
            while (_currentSize < sizeInBytes)
            {
                var entry = EntryGenerator.GenerateEntry();
                _currentSize += entry.Size();
                await streamWriter.WriteLineAsync(entry.ToString());
            }
        }
    }
}
