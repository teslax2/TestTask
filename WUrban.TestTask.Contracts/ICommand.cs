
namespace WUrban.TestTask.Generator.Args
{
    public interface ICommand
    {
        string CommandName { get; }
        Task ExecuteAsync();
        string Description { get; }
    }
}
