
using Moq;
using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.Generator;
using WUrban.TestTask.Sorter.Commands;

namespace WUrban.TestTask.Generator.Args.Tests
{
    public class SortCommandTests
    {
        [Fact()]
        public void SortCommand_Passing()
        {
            // Arrange
            var executor = new Mock<IExecutor<ICommand>>();
            // Act
            var sortCommand = new SortCommand("path", "output.txt", executor.Object);
            // Assert
            Assert.NotNull(sortCommand);
        }

        [Fact()]
        public void SortCommand_PathEmpty_ThrowsArgumentException()
        {
            // Arrange
            var executor = new Mock<IExecutor<ICommand>>();
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new SortCommand("", "output.txt", executor.Object));
        }

        [Fact()]
        public void SortCommand_OutputEmpty_ThrowsArgumentException()
        {
            // Arrange
            var executor = new Mock<IExecutor<ICommand>>();
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new SortCommand("path", "", executor.Object));
        }

        [Fact()]
        public void ParseFromArgs_NotEnoughArgs_ReturnsNull()
        {
            // Arrange
            var args = new string[] { "Sort" };
            // Act
            var result = SortCommand.ParseFromArgs(args);
            // Assert
            Assert.Null(result);
        }



        [Fact()]
        public async Task ExecuteAsyncTest()
        {
            // Arrange
            var executor = new Mock<IExecutor<ICommand>>();
            executor.Setup(x => x.ExecuteAsync(It.IsAny<ICommand>())).Returns(Task.CompletedTask);
            var SortCommand = new SortCommand("path", "output.txt", executor.Object);
            // Act
            await SortCommand.ExecuteAsync();
        }
    }
}