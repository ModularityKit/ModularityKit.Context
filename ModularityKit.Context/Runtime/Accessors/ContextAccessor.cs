using ModularityKit.Context.Abstractions;
using ModularityKit.Context.Runtime.Stores;

namespace ModularityKit.Context.Runtime.Accessors;

/// <summary>
/// Provides access to the current <typeparamref name="TContext"/> instance.
/// </summary>
/// <typeparam name="TContext">The type of context, must implement <see cref="IContext"/>.</typeparam>
/// <remarks>
/// <list type="bullet">
/// <item>Implements <see cref="IContextAccessor{TContext}"/> to expose the current context.</item>
/// <item>Delegates storage and retrieval of the context to <see cref="ContextStore{TContext}"/>.</item>
/// <item>Throws an exception if <see cref="RequireCurrent"/> is called when no context is active.</item>
/// </list>
/// </remarks>
public sealed class ContextAccessor<TContext> : IContextAccessor<TContext>
    where TContext : class, IContext
{
    private readonly ContextStore<TContext> _store;

    public ContextAccessor(ContextStore<TContext> store)
    {
        ArgumentNullException.ThrowIfNull(store);
        _store = store;
    }

    /// <inheritdoc />
    public TContext? Current => _store.Current;
    
    /// <inheritdoc />
    public TContext RequireCurrent()
    {
        return Current ?? throw new InvalidOperationException(
            $"No active {typeof(TContext).Name} found. " +
            $"Ensure the context is set before accessing it.");
    }
}
