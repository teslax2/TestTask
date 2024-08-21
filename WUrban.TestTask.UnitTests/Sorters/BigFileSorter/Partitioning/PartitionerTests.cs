using Xunit;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Partitioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Core;
using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Partitioning.Tests
{
    public class PartitionerTests
    {
        [Fact()]
        public void PartitionAsync_PartitionWhenNotFull()
        {
            // Arrange
            var partitionStore = new Mock<IPartitionStore>();
            partitionStore.Setup(p => p.Save(It.IsAny<Entry[]>())).ReturnsAsync(new Partition("path"));
            var partitioner = new Partitioner(partitionStore.Object);
            async IAsyncEnumerable<Entry> GetEntries()
            {
                yield return await Task.FromResult(new Entry(1, "23"));
            }
            // Act
            var result = partitioner.PartitionAsync(GetEntries());
            var partitions = result.ToBlockingEnumerable().ToList();
            // Assert
            Assert.True(partitions.Count == 1);
        }

        [Fact()]
        public void PartitionAsync_PartitionWhenReachedMaxLimit()
        {
            // Arrange
            var partitionStore = new Mock<IPartitionStore>();
            partitionStore.Setup(p => p.Save(It.IsAny<Entry[]>())).ReturnsAsync(new Partition("path"));
            var partitioner = new Partitioner(partitionStore.Object, 10_000_000);
            async IAsyncEnumerable<Entry> GetEntries()
            {
                var entry = new Entry(1, "fasdfdsafasdgefgeawgasddgsdgadsgdsagdasgadsgdsagasdgdssggasdsdgasdgasd");
                var size = entry.Size();
                while(size < 10_100_000)
                {
                    yield return await Task.FromResult(entry);
                    size += entry.Size();
                }
            }
            // Act
            var result = partitioner.PartitionAsync(GetEntries());
            var partitions = result.ToBlockingEnumerable().ToList();
            // Assert
            Assert.True(partitions.Count == 2);
        }

        [Fact()]
        public void PartitionAsync_MaxLimitTooLow_ArgumentOutOfRangeException()
        {
            // Arrange
            var partitionStore = new Mock<IPartitionStore>();
            // Act
            Assert.Throws<ArgumentOutOfRangeException>(() => new Partitioner(partitionStore.Object, 10_000));
        }
    }
}