using WUrban.TestTask.Contracts;
using WUrban.TestTask.Generator.CommandLine;

namespace WUrban.TestTask.Generator
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
                Console.Error.WriteLine("Usage: generate --size=1000 [--output=output_file]");
                Console.Error.WriteLine("--size in bytes");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
