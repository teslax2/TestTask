using System.Diagnostics;
using WUrban.TestTask.Contracts;
using WUrban.TestTask.Sorter.CommandLine;

namespace WUrban.TestTask.Sorter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var command = Parser.Parse(args);
                await command.ExecuteAsync();
            }
            catch (InvalidCommandException ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine("Usage: sort --path=input_file [--output=output_file]");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
