using System.Net.Http.Json;
using Starter.IntegrationTests.Common.TestEngine.Configuration;

namespace Starter.IntegrationTests.Localizations
{
    public sealed class LocalizationTests(
        WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer databaseContainer)
        : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
    {

        private readonly HttpClient applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(databaseContainer.ConnectionString!)
            .CreateClient();

        [Theory]
        [InlineData("es-ES", "Hola")]
        [InlineData("fr-FR", "Bonjour")]
        [InlineData("en-US", "Hello")]
        internal async Task Given_request_accept_language_header_Then_response_should_be_translated_according_to_that_language(
            string countryCultureInfo,
            string expected)
        {
            //Arrange
            applicationHttpClient.DefaultRequestHeaders.Add("Accept-Language", countryCultureInfo);

            //Act
            var localizationResponse = await applicationHttpClient.GetAsync("/localization-tests");
            var localizedString = await localizationResponse.Content.ReadFromJsonAsync<LocalizedString>();

            //Assert
            localizationResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            localizedString.Should().NotBeNull();
            localizedString?.Value.Should().Be(expected);
        }

        [Fact]
        internal async Task Given_no_concrete_accept_language_header_Then_response_should_be_translated_to_default_culture()
        {
            //Arrange
            const string defaultCultureResponse = "Hello";

            //act
            var localizationResponse = await applicationHttpClient.GetAsync("/localization-tests");
            var localizedString = await localizationResponse.Content.ReadFromJsonAsync<LocalizedString>();

            //Assert
            localizationResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            localizedString.Should().NotBeNull();
            localizedString?.Value.Should().Be(defaultCultureResponse);
        }
    }
}
