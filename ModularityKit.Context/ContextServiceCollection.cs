using Microsoft.Extensions.DependencyInjection;
using ModularityKit.Context.Abstractions;
using ModularityKit.Context.Extensions;

namespace ModularityKit.Context;

/// <summary>
/// Backward-compatible entry point for service registration.
/// </summary>
[Obsolete("Use ModularityKit.Context.Extensions.ContextServiceCollectionExtensions instead.")]
public static class ContextServiceCollection
{
    /// <inheritdoc cref="ContextServiceCollectionExtensions.AddContext{TContext}(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>
    public static IServiceCollection AddContext<TContext>(IServiceCollection services)
        where TContext : class, IContext
        => ContextServiceCollectionExtensions.AddContext<TContext>(services);

    /// <inheritdoc cref="ContextServiceCollectionExtensions.AddReadOnlyContextAccessor{TContext}(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>
    public static IServiceCollection AddReadOnlyContextAccessor<TContext>(IServiceCollection services)
        where TContext : class, IContext
        => ContextServiceCollectionExtensions.AddReadOnlyContextAccessor<TContext>(services);
}
