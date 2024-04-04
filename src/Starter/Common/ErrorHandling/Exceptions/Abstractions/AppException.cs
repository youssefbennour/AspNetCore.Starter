namespace Starter.Common.ErrorHandling.Exceptions.Abstractions;

internal abstract class AppException : InvalidOperationException
{
    private protected AppException(string message) : base(message)
    {
        ErrorMessages = [message];
    }

    private protected AppException(IEnumerable<string> errorMessages) : base(errorMessages.FirstOrDefault())
    {
        ErrorMessages = errorMessages;
    }

    private protected IEnumerable<string> ErrorMessages { get; }
}
