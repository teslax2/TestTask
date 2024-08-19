﻿using WUrban.TestTask.Generator.Generator;
using WUrban.TestTask.Sorter.Commands;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter;

namespace WUrban.TestTask.Sorter.Sorters
{
    internal class BigFileSorterExecutor : IExecutor<SortCommand>
    {
        public async Task ExecuteAsync(SortCommand command)
        {
            using var partitioner = new Partitioner(command.Path, 50_000_000);
            var partitions = partitioner.GetPartitions();
            var tempFiles = new List<string>();
            foreach (var partition in partitions)
            {
                var tempFile = await partition.GetEntries().OrderAndStoreExternalyAsync();
                tempFiles.Add(tempFile);
            }
            Console.WriteLine("Merging sorted files...");
            await MergeSortedFilesAsync(tempFiles, "output.txt");
            // Clean up temp files
            foreach (var tempFile in tempFiles)
            {
                File.Delete(tempFile);
            }
        }

        private async Task MergeSortedFilesAsync(List<string> sortedFiles, string outputFileName)
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
