using Microsoft.AspNetCore.Mvc;
using Starter.Common.ErrorHandling.ErrorModels;
using System.Text.Json.Serialization;

namespace Starter.Common.ErrorHandling {
    public class AppProblemDetails
    {
        private const string DefaultMessage = "Une ou plusieurs erreurs se sont produites";
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FieldValidationError>? Errors { get; set; }

        public AppProblemDetails()
        {
            this.Message = DefaultMessage;
        }

        public AppProblemDetails(List<FieldValidationError> errors)
        {
            this.Message = DefaultMessage;
            this.Errors = errors;
        }
        
        public AppProblemDetails(string message, List<FieldValidationError>? errors = null)
        {
            this.Message = message;
            this.Errors = errors;
        }
    }
}
