using Moq;
using System.Text;
using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Generator;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Merger.Tests
{
    public class PartitionMergerTests
    {

        [Fact()]
        public async Task MergePartitionsAsync_SortsAndMerges()
        {
            //Arrange
            var partitionStore = new Mock<IPartitionStore>();
            partitionStore.Setup(p => p.GetStreamReader(It.Is<Partition>(x => x.Path == "path1"),81920))
                .Returns(GetStreamReader());
            partitionStore.Setup(p => p.GetStreamReader(It.Is<Partition>(x => x.Path == "path2"), 81920))
                .Returns(GetStreamReader());
            partitionStore.Setup(p => p.GetStreamReader(It.Is<Partition>(x => x.Path == "path3"), 81920))
                .Returns(GetStreamReader());
            var partitionMerger = new PartitionMerger(partitionStore.Object);
            async IAsyncEnumerable<Partition> GetPartitions()
            {
                yield return await Task.FromResult(new Partition("path1"));
                yield return await Task.FromResult(new Partition("path2"));
                yield return await Task.FromResult(new Partition("path3"));
            }
            var bytes = new byte[100000];
            var outputStream = new MemoryStream(bytes);
            //Act
            await partitionMerger.MergePartitionsAsync(GetPartitions(), outputStream);
            //Assert
            var result = Encoding.UTF8.GetString(bytes);
            var lines = result.Split(Environment.NewLine)[..^1];
            var entries = lines.Select(x => Entry.Parse(x)).ToArray();
            var ordered = entries.Order().ToArray();
            Assert.True(entries.Length == 30);
            Assert.True(entries.SequenceEqual(ordered));
        }

        private StreamReader GetStreamReader()
        {
            var entries = new Entry[10];
            for (var i = 0; i < 10; i++)
            {
                entries[i] = EntryGenerator.GenerateEntry();
            }
            Array.Sort(entries);
            var bytes = Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, entries.Select(x => x.ToString())));
            var stream = new MemoryStream(bytes);
            return new StreamReader(stream);
        }
    }
}