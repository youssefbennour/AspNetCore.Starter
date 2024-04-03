namespace Starter.Common.Validation.Exceptions.Base;

using System.Net;

internal class AppException : InvalidOperationException
{
    internal AppException(string message, HttpStatusCode statusCode) : base(message)
    {
        ErrorMessages = [message];
        StatusCode = statusCode;
    }

    internal AppException(IEnumerable<string> errorMessages, HttpStatusCode statusCode) : base(errorMessages.FirstOrDefault())
    {
        ErrorMessages = errorMessages;
        StatusCode = statusCode;
    }

    internal IEnumerable<string> ErrorMessages { get; }
    internal HttpStatusCode StatusCode { get; }
}
