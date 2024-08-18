using WUrban.TestTask.Generator.Args;

namespace WUrban.TestTask.Generator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("start");
            var command = Parser.Parse(args);
            await command.ExecuteAsync();
            Console.WriteLine("stop");
        }
    }
}
