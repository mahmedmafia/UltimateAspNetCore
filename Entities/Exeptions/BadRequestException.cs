
namespace Entities.Exeptions
{
    public abstract class BadRequestException : Exception
    {
        public BadRequestException(string? message) : base(message)
        {
        }
    }
}
