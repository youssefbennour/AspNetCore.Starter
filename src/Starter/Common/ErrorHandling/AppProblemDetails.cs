using Microsoft.AspNetCore.Mvc;
using Starter.Common.ErrorHandling.ErrorModels;
using System.Text.Json.Serialization;

namespace Starter.Common.ErrorHandling {
    public class AppProblemDetails : ProblemDetails {
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FieldValidationError>? Errors { get; set; }
    }
}
