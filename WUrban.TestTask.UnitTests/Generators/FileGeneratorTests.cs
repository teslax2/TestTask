using Moq;

namespace WUrban.TestTask.Generator.Generators.Tests
{
    public class FileGeneratorTests
    {
        [Fact()]
        public async Task GenerateAsync_ProducesEntries()
        {
            // arrange
            var generator = new FileGenerator();
            var bytes = new byte[2000];
            using var stream = new MemoryStream(bytes);
            // act
            await generator.GenerateAsync(1000, stream);
            // assert
            var entries = System.Text.Encoding.UTF8.GetString(bytes);
            var lines = entries.Split(Environment.NewLine);
            Assert.True(lines.Length > 0);
        }
    }
}