using ModularityKit.Context.Abstractions.Contracts;

namespace ModularityKit.Context.Abstractions.Access;

/// <summary>
/// Manages the execution of operations within specific <typeparamref name="TContext"/>.
/// </summary>
/// <typeparam name="TContext">The type of context being managed, must implement <see cref="IContext"/>.</typeparam>
/// <remarks>
/// <list type="bullet">
/// <item>Provides access to the current context via <see cref="Current"/>.</item>
/// <item>Allows executing actions or functions scoped to given context.</item>
/// <item>Ensures that the provided context is active for the duration of the execution.</item>
/// <item>Supports both void-returning actions and result-returning functions.</item>
/// </list>
/// </remarks>
public interface IContextManager<TContext> where TContext : class, IContext
{
    /// <summary>
    /// Begins a context scope that will restore the previous context on disposal.
    /// </summary>
    IContextScope<TContext> BeginContext(TContext context);

    /// <summary>
    /// Gets the currently active context, or null if no context is active.
    /// </summary>
    TContext? Current { get; }
    
    /// <summary>
    /// Executes an asynchronous action within the specified <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The context to activate for the duration of the action.</param>
    /// <param name="action">The asynchronous action to execute.</param>
    Task ExecuteInContext(TContext context, Func<Task> action);

    /// <summary>
    /// Executes an asynchronous action within the specified <paramref name="context"/>.
    /// </summary>
    /// <param name="context">The context to activate for the duration of the action.</param>
    /// <param name="action">The asynchronous action to execute.</param>
    /// <param name="cancellationToken">The cancellation token for the action.</param>
    Task ExecuteInContext(TContext context, Func<CancellationToken, Task> action, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Executes an asynchronous function within the specified <paramref name="context"/> and returns a result.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="context">The context to activate for the duration of the function.</param>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <returns>The result of the executed function.</returns>
    Task<TResult> ExecuteInContext<TResult>(TContext context, Func<Task<TResult>> func);

    /// <summary>
    /// Executes an asynchronous function within the specified <paramref name="context"/> and returns a result.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="context">The context to activate for the duration of the function.</param>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="cancellationToken">The cancellation token for the function.</param>
    /// <returns>The result of the executed function.</returns>
    Task<TResult> ExecuteInContext<TResult>(TContext context, Func<CancellationToken, Task<TResult>> func, CancellationToken cancellationToken = default);
}
