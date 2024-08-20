using System.Diagnostics;
using WUrban.TestTask.Sorter.CommandLine;

namespace WUrban.TestTask.Sorter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var command = Parser.Parse(args);
            await command.ExecuteAsync();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.ToString());
        }
    }
}
