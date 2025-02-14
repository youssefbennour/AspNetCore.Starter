﻿using Starter.Common.ErrorHandling.Exceptions.Abstractions;

namespace Starter.Common.ErrorHandling.Exceptions;

public class UnauthorizedException(string message) : AppException<UnauthorizedException>(message);