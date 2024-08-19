
using WUrban.TestTask.Generator.Args;

namespace WUrban.TestTask.Generator.Generator
{
    public interface IExecutor<in TCommand> 
        where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
