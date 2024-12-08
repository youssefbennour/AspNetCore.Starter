using System.Diagnostics;
using OpenTelemetry;

namespace Starter.Common.Telemetry.OpenTelemetry.Processors;

public class AutomatedEndpointsProcessor : BaseProcessor<Activity>
{
    public override void OnEnd(Activity activity)
    {
        if (!IsActivityToBeRecorded(activity))
        {
            activity.ActivityTraceFlags &= ActivityTraceFlags.None;
            activity.IsAllDataRequested = false;
        }
    }
    private static bool IsActivityToBeRecorded(Activity activity)
    {
        var rootActivity = activity;
        while (rootActivity.Parent != null)
        {
            rootActivity = rootActivity.Parent;
        }

        return rootActivity.Kind == ActivityKind.Server;
    }
}