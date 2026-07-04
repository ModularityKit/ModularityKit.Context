using ModularityKit.Context.Abstractions;

namespace ModularityKit.Context.Models;

/// <summary>
/// Represents the result of a context-bound execution.
/// </summary>
/// <typeparam name="TContext">The context type used for execution.</typeparam>
/// <typeparam name="TResult">The result type.</typeparam>
public sealed record ContextExecutionResult<TContext, TResult>(
    TContext Context,
    TResult? Result,
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
