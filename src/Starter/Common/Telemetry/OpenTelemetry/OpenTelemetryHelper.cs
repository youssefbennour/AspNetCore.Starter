using System.Diagnostics;

namespace Starter.Common.Telemetry.OpenTelemetry;

internal static class OpenTelemetryHelper
{
    internal static string GetRelativeUrlPath(this Activity activity){
        string? urlPath = activity.TagObjects
            .FirstOrDefault(m => m.Key == "url.path")
            .Value?.ToString();
        
        return urlPath ?? activity.DisplayName;
    }
    
}