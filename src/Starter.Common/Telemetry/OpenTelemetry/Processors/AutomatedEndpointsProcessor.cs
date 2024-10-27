using System.Diagnostics;
using OpenTelemetry;

namespace Softylines.Contably.Common.Telemetry.OpenTelemetry.Processors;

public class AutomatedEndpointsProcessor : BaseProcessor<Activity>
{
    public AutomatedEndpointsProcessor()
    {
    }
    
    public override void OnEnd(Activity activity)
    {
        if (IsHealthOrMetricsEndpoint(activity))
        {
            activity.ActivityTraceFlags &= ~ActivityTraceFlags.Recorded;
        }
    }
    private static bool IsHealthOrMetricsEndpoint(Activity activity)
    {
        
        if (string.IsNullOrEmpty(activity.DisplayName))
        {
            return false;
        }
        return activity.GetRelativeUrlPath().StartsWith("/health") ||
               activity.GetRelativeUrlPath().StartsWith("/metrics");
    }
}