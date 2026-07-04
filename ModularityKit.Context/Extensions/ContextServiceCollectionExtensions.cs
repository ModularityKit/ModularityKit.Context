using Microsoft.Extensions.DependencyInjection;
using ModularityKit.Context.Abstractions;
using ModularityKit.Context.ReadOnly.Accessors;
using ModularityKit.Context.Runtime.Accessors;
using ModularityKit.Context.Runtime.Managers;
using ModularityKit.Context.Runtime.Stores;

namespace ModularityKit.Context.Extensions;

/// <summary>
/// Dependency injection registration for context services.
/// </summary>
public static class ContextServiceCollectionExtensions
{
    /// <summary>
    /// Registers the core context services for <typeparamref name="TContext"/>.
    /// </summary>
    public static IServiceCollection AddContext<TContext>(this IServiceCollection services)
        where TContext : class, IContext
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<ContextStore<TContext>>();
        services.AddSingleton<IContextAccessor<TContext>, ContextAccessor<TContext>>();
        services.AddSingleton<IContextManager<TContext>, ContextManager<TContext>>();

        return services;
    }

    /// <summary>
    /// Registers a read-only accessor that exposes <see cref="IReadOnlyContext"/> to untrusted consumers.
    /// </summary>
    public static IServiceCollection AddReadOnlyContextAccessor<TContext>(this IServiceCollection services)
        where TContext : class, IContext
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IContextAccessor<IReadOnlyContext>>(sp =>
        {
            var innerAccessor = sp.GetRequiredService<IContextAccessor<TContext>>();
            return new ReadOnlyContextAccessor<TContext>(innerAccessor);
        });

        return services;
    }
}
