using ModularityKit.Context.Abstractions.Contracts;

namespace ModularityKit.Context.Abstractions.Factories;

/// <summary>
/// Creates new <typeparamref name="TContext"/> instances in consistent, centralized way.
/// </summary>
/// <typeparam name="TContext">The type of context to create, must implement <see cref="IContext"/>.</typeparam>
/// <remarks>
/// <list type="bullet">
/// <item>Centralizes context construction so application code can avoid scattered <c>new</c> calls and ad hoc factories.</item>
/// <item>Creation is kept separate from activation; factories never enter a scope or start execution.</item>
/// <item>Implementations should return immutable or effectively immutable context instances.</item>
/// <item>This interface is intentionally minimal; richer factories can be built by implementing it.</item>
/// </list>
/// </remarks>
public interface IContextFactory<out TContext> where TContext : class, IContext
{
    /// <summary>
    /// Creates a new <typeparamref name="TContext"/> instance.
    /// </summary>
    /// <returns>A new context instance.</returns>
    TContext Create();
}