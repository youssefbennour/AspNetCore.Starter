using System.Diagnostics;

namespace Softylines.Contably.Common.Telemetry.OpenTelemetry;

public static class OpenTelemetryHelper
{
    public static string GetRelativeUrlPath(this Activity activity){
        string? urlPath = activity.TagObjects
            .FirstOrDefault(m => m.Key == "url.path")
            .Value?.ToString();
        
        return urlPath ?? activity.DisplayName;
    }
    
}