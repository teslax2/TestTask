using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Generator.Commands
{
    internal class HelpCommand : ICommand
    {
        public string CommandName => "Help";

        public string Description => "Displays help";

        public Task ExecuteAsync()
        {
            this.GetType().Assembly.GetTypes()
                .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface)
                .Select(t => (ICommand)Activator.CreateInstance(t))
                .ToList()
                .ForEach(c => Console.WriteLine($"{c.CommandName} - {c.Description}"));
            return Task.CompletedTask;
        }
    }
}
