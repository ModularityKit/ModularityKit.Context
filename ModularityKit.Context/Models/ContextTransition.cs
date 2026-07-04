using ModularityKit.Context.Abstractions;

namespace ModularityKit.Context.Models;

/// <summary>
/// Represents a single context transition in the execution flow.
/// </summary>
/// <typeparam name="TContext">The context type involved in the transition.</typeparam>
public sealed record ContextTransition<TContext>(
    ContextTransitionKind Kind,
    TContext? PreviousContext,
    TContext CurrentContext,
    DateTimeOffset StartedAt,
    DateTimeOffset CompletedAt
) where TContext : class, IContext
{
    /// <summary>
    /// Gets the elapsed time for the transition.
    /// </summary>
    public TimeSpan Duration => CompletedAt - StartedAt;
}
