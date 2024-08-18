using System.Text;
using WUrban.TestTask.Generator.Args;

namespace WUrban.TestTask.Generator.Generator
{
    internal class TextGenerator : IExecutor<GenerateFileCommand>
    {
        private int _currentSize = 0;

        public async Task ExecuteAsync(GenerateFileCommand command)
        {
            using var fileHandle = File.OpenWrite("output.txt");
            using var streamWriter = new StreamWriter(fileHandle, Encoding.UTF8, bufferSize: 10_000_000);
            while (_currentSize < command.SizeInBytes)
            {
                var entry = EntryGenerator.GenerateEntry();
                _currentSize += entry.Size;
                await streamWriter.WriteAsync(entry.ToString());
            }
        }
    }
}
