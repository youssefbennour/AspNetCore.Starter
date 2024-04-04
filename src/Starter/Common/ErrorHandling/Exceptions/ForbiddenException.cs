﻿using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.Validation.Requests.Exceptions;

internal class ForbiddenException : AppException
{
    internal ForbiddenException(string message) : base(message) { }

    internal ForbiddenException(IEnumerable<string> messages) : base(messages) { }
}