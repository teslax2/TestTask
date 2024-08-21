namespace WUrban.TestTask.Generator.Generators
{
    internal interface IGenerator
    {
        Task GenerateAsync(long sizeInBytes, string output);
    }
}
