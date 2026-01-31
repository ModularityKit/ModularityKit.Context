using Microsoft.Extensions.Logging;
using ModularityKit.Context.Abstractions;

namespace ModularityKit.Context.Runtime;

/// <summary>
/// Manages the execution of code within a specific <typeparamref name="TContext"/> scope
/// with optional logging.
/// </summary>
/// <typeparam name="TContext">The type of context, must implement <see cref="IContext"/>.</typeparam>
public sealed class ContextManager<TContext> : IContextManager<TContext>
    where TContext : class, IContext
{
    private readonly ContextStore<TContext> _store;
    private readonly ILogger<ContextManager<TContext>>? _logger;

    /// <summary>
    /// Creates a ContextManager with optional logging.
    /// </summary>
    public ContextManager(ContextStore<TContext> store, ILogger<ContextManager<TContext>>? logger = null)
    {
        _store = store;
        _logger = logger;
    }

    /// <inheritdoc />
    public TContext? Current => _store.Current;

    /// <inheritdoc />
    public async Task ExecuteInContext(TContext context, Func<Task> action)
    {
        Log("START", context);

        using (_store.SetCurrent(context))
        {
            await action();
        }

        Log("END", context);
    }

    /// <inheritdoc />
    public async Task<TResult> ExecuteInContext<TResult>(TContext context, Func<Task<TResult>> func)
    {
        Log("START", context);

        TResult result;
        using (_store.SetCurrent(context))
        {
            result = await func();
        }

        Log("END", context);

        return result;
    }

    private void Log(string phase, TContext context)
    {
        if (_logger is null) return;
        string contextSummary = context.ToString() ?? typeof(TContext).Name;
        _logger.LogInformation("{Phase} Context {ContextType}: {ContextData}", phase, typeof(TContext).Name, contextSummary);
    }
}