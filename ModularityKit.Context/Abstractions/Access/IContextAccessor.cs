using ModularityKit.Context.Abstractions.Contracts;

namespace ModularityKit.Context.Abstractions.Access;

/// <summary>
/// Provides access to the current context instance of type <typeparamref name="TContext"/>.
/// </summary>
/// <typeparam name="TContext">The type of context being accessed. Must implement <see cref="IContext"/>.</typeparam>
/// <remarks>
/// <list type="bullet">
/// <item>Exposes the current context via the <see cref="Current"/> property.</item>
/// <item>Allows retrieving the current context safely using <see cref="RequireCurrent"/>, which throws if no context is available.</item>
/// <item>Supports dependency injection and generic context retrieval in applications that use scoped or ambient context patterns.</item>
/// </list>
/// </remarks>
public interface IContextAccessor<TContext> where TContext : class, IContext
{
    /// <summary>
    /// Gets the current context instance, or <c>null</c> if no context is available.
    /// </summary>
    TContext? Current { get; }

    /// <summary>
    /// Attempts to retrieve the active context without throwing.
    /// </summary>
    /// <param name="context">When this method returns <c>true</c>, contains the active context; otherwise <c>null</c>.</param>
    /// <returns><c>true</c> when an active context exists; otherwise <c>false</c>.</returns>
    bool TryGetCurrent(out TContext context);

    /// <summary>
    /// Gets the current context instance, throwing an exception if none is available.
    /// </summary>
    /// <returns>The current <typeparamref name="TContext"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="Current"/> is <c>null</c>.</exception>
    TContext RequireCurrent();
}