namespace WUrban.TestTask.Generator.Generators
{
    internal interface IGenerator
    {
        Task GenerateAsync(int sizeInBytes, string output);
    }
}
