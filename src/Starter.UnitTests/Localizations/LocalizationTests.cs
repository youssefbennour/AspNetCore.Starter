using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Starter.UnitTests.Localizations
{
    public sealed class LocalizationTests(
        WebApplicationFactory<Program> applicationInMemoryFactory)
        : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient applicationHttpClient = applicationInMemoryFactory
            .CreateClient();

        [Theory]
        [InlineData("fr-FR", "Bonjour")]
        internal async Task Given_localizaed_word_it_should_be_transalted_to_the_correct_language(
            string countryCultureInfo, 
            string expected)
        {
            //Arrange
            this.applicationHttpClient.DefaultRequestHeaders.Add("Accept-Language", countryCultureInfo);

            //Act
            var localizationResponse = await this.applicationHttpClient.GetAsync("/localization-tests");
            var localizedString = await localizationResponse.Content.ReadFromJsonAsync<LocalizedString>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, localizationResponse.StatusCode);
            Assert.NotNull(localizedString);
            Assert.False(localizedString.ResourceNotFound);
            Assert.Equal(expected, localizedString.Value);
        }
    }
}
