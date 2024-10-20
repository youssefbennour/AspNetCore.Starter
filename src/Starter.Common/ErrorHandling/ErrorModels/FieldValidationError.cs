using System.Text.Json.Serialization;

namespace Starter.Common.ErrorHandling.ErrorModels {
    public sealed class FieldValidationError {

        public FieldValidationError(string field, string message) {
            Field = field;
            Message = message;
        }

        [JsonPropertyName("field")]
        public string Field { get; private set; }
        [JsonPropertyName("message")]
        public string Message { get; private set; }
    }

}
