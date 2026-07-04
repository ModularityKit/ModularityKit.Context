using ModularityKit.Context.Abstractions;

namespace ModularityKit.Context.Models;

/// <summary>
/// Lightweight context summary intended for logging and diagnostics.
/// </summary>
public sealed record ContextDescriptor(
    string ContextType,
    string Id,
    DateTimeOffset CreatedAt,
    string? Summary = null
)
{
    /// <summary>
    /// Creates a descriptor from a context instance.
    /// </summary>
    public static ContextDescriptor FromContext<TContext>(TContext context)
        where TContext : class, IContext
    {
        ArgumentNullException.ThrowIfNull(context);

        return new ContextDescriptor(
            ContextType: typeof(TContext).Name,
            Id: context.Id,
            CreatedAt: context.CreatedAt,
            Summary: context.ToString()
        );
    }
}
