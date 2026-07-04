using ModularityKit.Context.Abstractions;

namespace ModularityKit.Context.Extensions;

/// <summary>
/// Convenience helpers for context accessors.
/// </summary>
public static class ContextAccessorExtensions
{
    /// <summary>
    /// Returns the active context, or <c>null</c> if none is available.
    /// </summary>
    public static TContext? GetCurrent<TContext>(this IContextAccessor<TContext> accessor)
        where TContext : class, IContext
    {
        ArgumentNullException.ThrowIfNull(accessor);
        return accessor.Current;
    }

    /// <summary>
    /// Returns the active context, or <c>null</c> if none is available.
    /// </summary>
    public static TContext? TryGetCurrent<TContext>(this IContextAccessor<TContext> accessor)
        where TContext : class, IContext
    {
        ArgumentNullException.ThrowIfNull(accessor);
        return accessor.Current;
    }

    /// <summary>
    /// Returns <c>true</c> when a current context exists.
    /// </summary>
    public static bool HasContext<TContext>(this IContextAccessor<TContext> accessor)
        where TContext : class, IContext
    {
        ArgumentNullException.ThrowIfNull(accessor);
        return accessor.Current is not null;
    }
}
