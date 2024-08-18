
namespace WUrban.TestTask.Generator.Args
{
    internal interface ICommand
    {
        string CommandName { get; }
        Task ExecuteAsync();
    }
}
