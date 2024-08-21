using Moq;
using WUrban.TestTask.Contracts;
using WUrban.TestTask.Sorter.Sorters.BigFileSorter.Reader;

namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Tests
{
    public class BigFileSorterTests
    {
        [Fact()]
        public async Task SortAsync_Passing()
        {
            // Arrange
            var entriesReader = new Mock<IEntriesReader>();
            async IAsyncEnumerable<Entry> GetEntries()
            {
                yield return await Task.FromResult(new Entry(1, "23"));
            }
            entriesReader.Setup(e => e.GetEntriesAsync()).Returns(GetEntries);
            var bigFileSorter = new BigFileSorter(entriesReader.Object);
            string inputPath = "inputPath";
            string outputPath = "outputPath";
            // Act
            await bigFileSorter.SortAsync(inputPath, outputPath);
            // Assert
            entriesReader.Verify(e => e.GetEntriesAsync(), Times.Once);
        }

        [Fact()]
        public async Task SortAsync_EmptyInput_ThrowsArgumentNullException()
        {
            // Arrange
            var entriesReader = new Mock<IEntriesReader>();
            var bigFileSorter = new BigFileSorter(entriesReader.Object);
            string inputPath = "";
            string outputPath = "outputPath";
            // Act
            await Assert.ThrowsAsync<ArgumentException>(async () => await bigFileSorter.SortAsync(inputPath, outputPath));
        }

        [Fact()]
        public async Task SortAsync_EmptyOutput_ThrowsArgumentNullException()
        {
            // Arrange
            var entriesReader = new Mock<IEntriesReader>();
            var bigFileSorter = new BigFileSorter(entriesReader.Object);
            string inputPath = "inputPath";
            string outputPath = "";
            // Act
            await Assert.ThrowsAsync<ArgumentException>(async () => await bigFileSorter.SortAsync(inputPath, outputPath));
        }
    }
}