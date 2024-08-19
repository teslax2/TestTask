using WUrban.TestTask.Generator.Utils;

namespace WUrban.TestTask.Generator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Started at {DateTime.Now}");
            var command = Parser.Parse(args);
            await command.ExecuteAsync();
            Console.WriteLine($"Completed at {DateTime.Now}");
        }
    }
}
