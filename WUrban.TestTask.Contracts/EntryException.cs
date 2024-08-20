
namespace WUrban.TestTask.Contracts
{
    internal class EntryException : Exception
    {
        public EntryException()
        {
        }

        public EntryException(string? message) : base(message)
        {
        }

        public EntryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
