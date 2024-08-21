
using Moq;
using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Generator;

namespace WUrban.TestTask.Generator.Args.Tests
{
    public class GenerateFileCommandTests
    {
        [Fact()]
        public void GenerateFileCommand_Passing()
        {
            // Arrange
            var executor = new Mock<IExecutor<ICommand>>();
            // Act
            var generateFileCommand = new GenerateFileCommand(1000, "output.txt", executor.Object);
            // Assert
            Assert.NotNull(generateFileCommand);
        }

        [Fact()]
        public void GenerateFileCommand_IncorrectSize_ThrowsArgumentOutOfRange()
        {
            // Arrange
            var executor = new Mock<IExecutor<ICommand>>();
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new GenerateFileCommand(-1, "output.txt", executor.Object));
        }

        [Fact()]
        public void GenerateFileCommand_OutputNull_ThrowsArgumentException()
        {
            // Arrange
            var executor = new Mock<IExecutor<ICommand>>();
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new GenerateFileCommand(1000, "", executor.Object));
        }

        [Fact()]
        public void ParseFromArgs_Passing()
        {
            // Arrange
            var args = new string[] { "Generate", "--size=1000", "--output=output.txt" };
            // Act
            var result = GenerateFileCommand.ParseFromArgs(args);
            // Assert
            Assert.NotNull(result);
        }

        [Fact()]
        public void ParseFromArgs_NotEnoughArgs_ReturnsNull()
        {
            // Arrange
            var args = new string[] { "Generate" };
            // Act
            var result = GenerateFileCommand.ParseFromArgs(args);
            // Assert
            Assert.Null(result);
        }

        [Fact()]
        public void ParseFromArgs_IncorrectSize_ReturnsNull()
        {
            // Arrange
            var args = new string[] { "Generate", "--size= " };
            // Act
            var result = GenerateFileCommand.ParseFromArgs(args);
            // Assert
            Assert.Null(result);
        }

        [Fact()]
        public async Task ExecuteAsyncTest()
        {
            // Arrange
            var executor = new Mock<IExecutor<ICommand>>();
            executor.Setup(x => x.ExecuteAsync(It.IsAny<ICommand>())).Returns(Task.CompletedTask);
            var generateFileCommand = new GenerateFileCommand(1000, "output.txt", executor.Object);
            // Act
            await generateFileCommand.ExecuteAsync();
        }
    }
}