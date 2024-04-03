namespace Starter.Common.Validation.Exceptions.Base;

using System.Net;

internal class AppException : InvalidOperationException
{
    internal AppException(string message) : base(message)
    {
        ErrorMessages = [message];
    }

    internal AppException(IEnumerable<string> errorMessages) : base(errorMessages.FirstOrDefault())
    {
        ErrorMessages = errorMessages;
    }

    internal IEnumerable<string> ErrorMessages { get; }
}
