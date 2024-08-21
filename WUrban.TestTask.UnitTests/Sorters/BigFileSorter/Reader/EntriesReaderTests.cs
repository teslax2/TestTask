using System.Text;


namespace WUrban.TestTask.Sorter.Sorters.BigFileSorter.Reader.Tests
{
    public class EntriesReaderTests
    {
        [Fact()]
        public void GetEntriesAsync_ReturnsEntries()
        {
            // Arrange
            var lines = new List<string>
            {
                "1. sdfasdf",
                "3. sdf",
                "5. sdff"
            };
            var bytes = Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, lines));
            var stream = new MemoryStream(bytes);
            var entriesReader = new EntriesReader(stream);
            // Act
            var result = entriesReader.GetEntriesAsync();
            var entries = result.ToBlockingEnumerable().ToList();
            // Assert
            Assert.True(entries.Count == lines.Count);
        }

        [Fact()]
        public void GetEntriesAsync_ReturnsEmpty()
        {
            // Arrange
            var stream = new MemoryStream();
            var entriesReader = new EntriesReader(stream);
            // Act
            var result = entriesReader.GetEntriesAsync();
            var entries = result.ToBlockingEnumerable().ToList();
            // Assert
            Assert.True(entries.Count == 0);
        }
    }
}