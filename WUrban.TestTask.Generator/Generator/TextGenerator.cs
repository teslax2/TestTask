
using WUrban.TestTask.Generator.Args;

namespace WUrban.TestTask.Generator.Generator
{
    internal class TextGenerator : IExecutor<GenerateFileCommand>
    {
        private int _currentSize = 0;

        public async Task ExecuteAsync(GenerateFileCommand command)
        {
            using var fileHandle = File.OpenWrite("output.txt");
            using var stringWriter = new StreamWriter(fileHandle);
            var entryGenerator = new EntryGenerator();
            while (_currentSize < command.SizeInBytes)
            {
                var entry = entryGenerator.GenerateEntry();
                _currentSize += entry.Size;
                await stringWriter.WriteLineAsync(entry.ToString());
            }
        }
    }
}
