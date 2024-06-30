using System.Text.Json.Serialization;

namespace Starter.IntegrationTests.Localizations
{
    /// <summary>
    /// Since the base class has multiple constructors, and its source is <see cref="Microsoft.Extensions.Localization"/>.
    /// We had to create a version which specifies the constructor to be used for deserialization process.
    /// </summary>
    internal sealed class LocalizedString : Microsoft.Extensions.Localization.LocalizedString
    {

        [JsonConstructor]
        internal LocalizedString(string name, string value, bool resourceNotFound, string? searchedLocation)
            : base(name, value, resourceNotFound, searchedLocation)
        {

        }

    }
}
