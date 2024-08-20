namespace WUrban.TestTask.Contracts
{
    public interface ICommand
    {
        string CommandName { get; }
        Task ExecuteAsync();
        string Description { get; }
    }
}
