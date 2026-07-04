using ModularityKit.Context.Abstractions;

namespace ModularityKit.Context.Models;

/// <summary>
/// Represents the observed execution of a delegate inside an active context.
/// </summary>
/// <typeparam name="TContext">The context type used for execution.</typeparam>
public sealed record ContextExecutionTrace<TContext>(
    TContext Context,
    DateTimeOffset StartedAt,
    DateTimeOffset CompletedAt,
    ContextExecutionState State,
    Exception? Exception = null
) where TContext : class, IContext
{
    /// <summary>
    /// Gets the elapsed time for the execution.
    /// </summary>
    public TimeSpan Duration => CompletedAt - StartedAt;

    /// <summary>
    /// Gets a value indicating whether the execution completed successfully.
    /// </summary>
    public bool Succeeded => State == ContextExecutionState.Succeeded;

    /// <summary>
    /// Gets the error message when the execution faulted or was canceled.
    /// </summary>
    public string? ErrorMessage => Exception?.Message;
}
