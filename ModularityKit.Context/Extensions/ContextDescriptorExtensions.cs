using ModularityKit.Context.Abstractions;
using ModularityKit.Context.Models;

namespace ModularityKit.Context.Extensions;

/// <summary>
/// Descriptor helpers for contexts.
/// </summary>
public static class ContextDescriptorExtensions
{
    /// <summary>
    /// Creates a lightweight descriptor suitable for logs and diagnostics.
    /// </summary>
    public static ContextDescriptor ToDescriptor<TContext>(this TContext context)
        where TContext : class, IContext
        => ContextDescriptor.FromContext(context);
}
