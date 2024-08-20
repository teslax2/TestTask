using WUrban.TestTask.Generator.CommandLine;

namespace WUrban.TestTask.Generator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var command = Parser.Parse(args);
            await command.ExecuteAsync();
        }
    }
}
