
using WUrban.TestTask.Contracts;

namespace WUrban.TestTask.Generator.Generator
{
    public interface IExecutor<in TCommand> 
        where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
