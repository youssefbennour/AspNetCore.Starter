using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Starter.Common.ErrorHandling.ErrorModels
{
    internal sealed class ValidationError
    {

        internal ValidationError(string message) {
            Message = message;
        }
        internal ValidationError(string message, List<FieldValidationError> errors) {
            Message = message;
            Errors = errors;
        }

        internal string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        internal List<FieldValidationError>? Errors { get; set; }

        internal static ValidationError InternalServerError => new("Server error");
    }

    internal sealed class FieldValidationError
    {
        internal FieldValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }

        internal string Field { get; private set; }
        internal string Message { get; private set; }
    }
    
}
