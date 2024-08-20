using WUrban.TestTask.Generator.Args;
using WUrban.TestTask.Generator.Generator;
using WUrban.TestTask.Generator.Generators;

namespace WUrban.TestTask.Generator.Commands
{
    internal class GenerateFileCommandExecutor : IExecutor<GenerateFileCommand>
    {
        private readonly IGenerator _generator;

        public GenerateFileCommandExecutor(IGenerator generator)
        {
            _generator = generator;
        }

        public async Task ExecuteAsync(GenerateFileCommand command)
        {
            await _generator.GenerateAsync(command.SizeInBytes, command.Output);
        }
    }
}
