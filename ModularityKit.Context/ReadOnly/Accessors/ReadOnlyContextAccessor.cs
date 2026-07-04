using ModularityKit.Context.Abstractions;
using ModularityKit.Context.ReadOnly.Snapshots;

namespace ModularityKit.Context.ReadOnly.Accessors;

/// <summary>
/// Provides a read-only accessor wrapper that exposes <see cref="IReadOnlyContext"/> 
/// while delegating the underlying context retrieval to an existing accessor.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Wraps an <see cref="IContextAccessor{TContext}"/> and converts retrieved contexts into read-only views.</item>
/// <item>Ensures consumers cannot mutate the returned context instance.</item>
/// <item>Ensures thread-safety and consistent snapshot creation.</item>
/// </list>
/// </remarks>
public sealed class ReadOnlyContextAccessor<TContext> : IContextAccessor<IReadOnlyContext>
    where TContext : class, IContext
{
    private readonly IContextAccessor<TContext> _innerAccessor;

    public ReadOnlyContextAccessor(IContextAccessor<TContext> innerAccessor)
    {
        ArgumentNullException.ThrowIfNull(innerAccessor);
        _innerAccessor = innerAccessor;
    }

    /// <inheritdoc />
    public IReadOnlyContext? Current
    {
        get
        {
            var context = _innerAccessor.Current;
            return context != null 
                ? ReadOnlyContextSnapshot.FromContext(context)
                : null;
        }
    }

    /// <inheritdoc />
    public IReadOnlyContext RequireCurrent()
    {
        var context = _innerAccessor.RequireCurrent();
        return ReadOnlyContextSnapshot.FromContext(context);
    }
}
