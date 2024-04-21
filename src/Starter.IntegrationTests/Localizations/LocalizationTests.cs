using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
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
        internal async Task Given_localizaed_word_it_should_be_transalted_to_the_correct_language(
            string countryCultureInfo,
            string expected)
        {
            //Arrange
            applicationHttpClient.DefaultRequestHeaders.Add("Accept-Language", countryCultureInfo);
            var localizationOptions = 
                applicationInMemoryFactory.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;

            //Act
            var localizationResponse = await applicationHttpClient.GetAsync("/localization-tests");
            var localizedString = await localizationResponse.Content.ReadFromJsonAsync<LocalizedString>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, localizationResponse.StatusCode);
            Assert.NotNull(localizedString);
            Assert.Equal(expected, localizedString.Value);
        }
    }
}
