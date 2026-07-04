using ModularityKit.Context.Abstractions;
using ModularityKit.Context.Models;

namespace ModularityKit.Context.Extensions;

/// <summary>
/// Convenience overloads and reporting helpers for context execution.
/// </summary>
public static class ContextManagerExtensions
{
    /// <summary>
    /// Executes a synchronous action within a context scope.
    /// </summary>
    public static async Task ExecuteInContext<TContext>(
        this IContextManager<TContext> manager,
        TContext context,
        Action action)
        where TContext : class, IContext
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(action);

        await manager.ExecuteInContext(context, () =>
        {
            action();
            return Task.CompletedTask;
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes a synchronous function within a context scope and returns its result.
    /// </summary>
    public static async Task<TResult> ExecuteInContext<TContext, TResult>(
        this IContextManager<TContext> manager,
        TContext context,
        Func<TResult> func)
        where TContext : class, IContext
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(func);

        return await manager.ExecuteInContext(context, () =>
        {
            var result = func();
            return Task.FromResult(result);
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes an action and returns a trace describing the outcome.
    /// </summary>
    public static async Task<ContextExecutionTrace<TContext>> TraceExecution<TContext>(
        this IContextManager<TContext> manager,
        TContext context,
        Func<Task> action)
        where TContext : class, IContext
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(action);

        var startedAt = DateTimeOffset.UtcNow;

        try
        {
            await manager.ExecuteInContext(context, action).ConfigureAwait(false);
            var completedAt = DateTimeOffset.UtcNow;
            return new ContextExecutionTrace<TContext>(
                Context: context,
                StartedAt: startedAt,
                CompletedAt: completedAt,
                State: ContextExecutionState.Succeeded);
        }
        catch (OperationCanceledException oce)
        {
            var completedAt = DateTimeOffset.UtcNow;
            return new ContextExecutionTrace<TContext>(
                Context: context,
                StartedAt: startedAt,
                CompletedAt: completedAt,
                State: ContextExecutionState.Canceled,
                Exception: oce);
        }
        catch (Exception ex)
        {
            var completedAt = DateTimeOffset.UtcNow;
            return new ContextExecutionTrace<TContext>(
                Context: context,
                StartedAt: startedAt,
                CompletedAt: completedAt,
                State: ContextExecutionState.Faulted,
                Exception: ex);
        }
    }

    /// <summary>
    /// Executes a function and returns a result model describing the outcome.
    /// </summary>
    public static async Task<ContextExecutionResult<TContext, TResult>> TraceExecution<TContext, TResult>(
        this IContextManager<TContext> manager,
        TContext context,
        Func<Task<TResult>> func)
        where TContext : class, IContext
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(func);

        var startedAt = DateTimeOffset.UtcNow;

        try
        {
            var result = await manager.ExecuteInContext(context, func).ConfigureAwait(false);
            var completedAt = DateTimeOffset.UtcNow;
            return new ContextExecutionResult<TContext, TResult>(
                Context: context,
                Result: result,
                StartedAt: startedAt,
                CompletedAt: completedAt,
                State: ContextExecutionState.Succeeded);
        }
        catch (OperationCanceledException oce)
        {
            var completedAt = DateTimeOffset.UtcNow;
            return new ContextExecutionResult<TContext, TResult>(
                Context: context,
                Result: default,
                StartedAt: startedAt,
                CompletedAt: completedAt,
                State: ContextExecutionState.Canceled,
                Exception: oce);
        }
        catch (Exception ex)
        {
            var completedAt = DateTimeOffset.UtcNow;
            return new ContextExecutionResult<TContext, TResult>(
                Context: context,
                Result: default,
                StartedAt: startedAt,
                CompletedAt: completedAt,
                State: ContextExecutionState.Faulted,
                Exception: ex);
        }
    }
}
