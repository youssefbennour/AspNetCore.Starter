using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Starter.IntegrationTests.Localizations
{
    public sealed class LocalizationTests(
        WebApplicationFactory<Program> applicationInMemoryFactory)
        : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient applicationHttpClient = applicationInMemoryFactory
            .CreateClient();

        [Theory]
        [InlineData("es-ES", "Hola")]
        [InlineData("fr-FR", "Bonjour")]
        [InlineData("en-US", "Hello")]
        internal async Task Given_request_accept_language_header_Then_response_should_be_transalted_according_to_that_language(
            string countryCultureInfo,
            string expected)
        {
            //Arrange
            applicationHttpClient.DefaultRequestHeaders.Add("Accept-Language", countryCultureInfo);

            //Act
            var localizationResponse = await applicationHttpClient.GetAsync("/localization-tests");
            var localizedString = await localizationResponse.Content.ReadFromJsonAsync<LocalizedString>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, localizationResponse.StatusCode);
            Assert.NotNull(localizedString);
            Assert.Equal(expected, localizedString.Value);
        }

        [Fact]
        internal async Task Given_no_concrete_accept_language_header_Then_response_should_be_translated_to_default_culture()
        {
            //Arrange

            //act
            var localizationResponse = await applicationHttpClient.GetAsync("/localization-tests");
            var localizedString = await localizationResponse.Content.ReadFromJsonAsync<LocalizedString>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, localizationResponse.StatusCode);
            Assert.NotNull(localizedString);
            Assert.Equal("Hello", localizedString.Value);
        }
    }
}
