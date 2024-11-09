using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Starter.Common.ErrorHandling;
using Starter.Common.ErrorHandling.Exceptions;

namespace Starter.UnitTests;


public sealed class GlobalExceptionHandlerTests {
   private readonly HttpContext _context = GetHttpContext();
    private readonly IWebHostEnvironment _environment;

    public GlobalExceptionHandlerTests()
    {
        _environment = Substitute.For<IWebHostEnvironment>();
        _environment.EnvironmentName.Returns(nameof(Environments.Production));
    }

    [Fact]
    internal async Task Given_business_rule_validation_exception_Then_returns_Unprocessable_Content() {
        // Arrange
        const string exceptionMessage = "Business rule not met";
        var exceptionHandler =
            new GlobalExceptionHandler(_environment);

        // Act
        await exceptionHandler.TryHandleAsync(_context, new BusinessRuleValidationException(exceptionMessage), default);

        // Assert
        _context.Response.StatusCode.Should().Be((int)HttpStatusCode.UnprocessableContent);

        var responseMessage = await GetExceptionResponseMessage();
        responseMessage.Message.Should().Be(exceptionMessage);
    }

    [Fact]
    internal async Task Given_business_rule_validation_exception_Then_should_contain_field_errors() {
        // Arrange
        string field = "Id";
        string message = "Id should not be null";

        const string exceptionMessage = "Business rule not met";
        var businessRuleValidationException = new BusinessRuleValidationException(exceptionMessage);
        businessRuleValidationException.UpsertToException(field, message);

        var exceptionHandler =
            new GlobalExceptionHandler(_environment);

        // Act
        await exceptionHandler.TryHandleAsync(_context, businessRuleValidationException, default);

        // Assert
        var responseMessage = await GetExceptionResponseMessage();
        responseMessage.Errors.Should().NotBeNullOrEmpty();
        responseMessage.Errors!.Count.Should().Be(1);
        responseMessage.Errors![0].Field.Should().Be(field);
        responseMessage.Errors![0].Message.Should().Be(message);
    }

    [Fact]
    internal async Task Given_other_than_business_rule_validation_exception_Then_returns_internal_server_error() {
        // Arrange
        const string exceptionMessage = "Server Error";
        var exceptionHandler =
            new GlobalExceptionHandler(_environment);

        // Act
        await exceptionHandler.TryHandleAsync(_context, new InvalidCastException("test"), CancellationToken.None);

        // Assert
        _context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

        var responseMessage = await GetExceptionResponseMessage();
        responseMessage.Message.Should().Be(exceptionMessage);
    }

    private static DefaultHttpContext GetHttpContext() =>
        new() {
            Response =
            {
                Body = new MemoryStream()
            }
        };

    private async Task<AppProblemDetails> GetExceptionResponseMessage() {
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var streamReader = new StreamReader(_context.Response.Body);
        var responseBody = await streamReader.ReadToEndAsync();
        var problemDetails = JsonConvert.DeserializeObject<AppProblemDetails>(responseBody);
        return problemDetails!;
    }
}
