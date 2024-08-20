using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Generator;
using WUrban.TestTask.Sorter.Commands;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter;

namespace WUrban.TestTask.Sorter.Sorters
{
    internal class BigFileSorterExecutor : IExecutor<SortCommand>
    {
        public async Task ExecuteAsync(SortCommand command)
        {
            var entriesReader = new EntriesReader(command.Path);
            var partitioner = new Partitioner(entriesReader);
            var partitions = await partitioner.Partition();

            Console.WriteLine("Merging sorted files...");
            await MergeSortedFilesAsync(partitions, "output.txt");
            // Clean up temp files
            foreach (var tempFile in partitions)
            {
                File.Delete(tempFile);
            }
        }

        private async Task MergeSortedFilesAsync(IEnumerable<string> sortedFiles, string outputFileName)
        {
            using var outputStream = File.OpenWrite(outputFileName);
            using var writer = new StreamWriter(outputStream, bufferSize: 8192);
            var readers = sortedFiles.Select(file => new StreamReader(new BufferedStream(File.OpenRead(file))));
            var dict = new Dictionary<StreamReader, Entry>();
            foreach (var reader in readers)
            {
                var line = await reader.ReadLineAsync();
                var entry = Entry.Parse(line);
                dict.Add(reader, entry);
            }

            while (dict.Count > 0)
            {
                var x = dict.MinBy(x => x.Value);
                await writer.WriteLineAsync(x.Value.ToString());
                if(x.Key.EndOfStream)
                {
                    dict.Remove(x.Key);
                    continue;
                }
                var line = await x.Key.ReadLineAsync();
                dict[x.Key] = Entry.Parse(line);
            }

            foreach (var reader in readers)
            {
                reader?.Dispose();
            }
        }
    }
}
