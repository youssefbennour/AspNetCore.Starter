using Starter.Passes.GetAllPasses;
using Starter.Passes.MarkPassAsExpired;

namespace Starter.Passes;

internal static class PassesEndpoints
{
    internal static void MapPasses(this IEndpointRouteBuilder app)
    {
        app.MapGetAllPasses();
        app.MapMarkPassAsExpired();
    }
}