using System.Text;
using WUrban.TestTask.Generator.Args;

namespace WUrban.TestTask.Generator.Generator
{
    internal class FileGenerator : IExecutor<GenerateFileCommand>
    {
        private readonly int _bufferSize;

        public FileGenerator(int bufferSize = 91200)
        {
            _bufferSize = bufferSize;
        }

        public async Task ExecuteAsync(GenerateFileCommand command)
        {
            using var fileHandle = File.OpenWrite("output.txt");
            using var streamWriter = new StreamWriter(fileHandle, Encoding.UTF8, bufferSize: _bufferSize);
            int _currentSize = 0;
            while (_currentSize < command.SizeInBytes)
            {
                var entry = EntryGenerator.GenerateEntry();
                _currentSize += entry.Size();
                await streamWriter.WriteLineAsync(entry.ToString());
            }
        }
    }
}
