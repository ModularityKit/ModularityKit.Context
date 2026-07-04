using Microsoft.Extensions.Logging;
using ModularityKit.Context.Abstractions;
using ModularityKit.Context.Runtime.Stores;

namespace ModularityKit.Context.Runtime.Managers;

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
        ArgumentNullException.ThrowIfNull(store);
        _store = store;
        _logger = logger;
    }

    /// <inheritdoc />
    public TContext? Current => _store.Current;

    /// <inheritdoc />
    public async Task ExecuteInContext(TContext context, Func<Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);
        Log("START", context);

        try
        {
            using (_store.SetCurrent(context))
            {
                await action().ConfigureAwait(false);
            }
        }
        finally
        {
            Log("END", context);
        }
    }

    /// <inheritdoc />
    public async Task<TResult> ExecuteInContext<TResult>(TContext context, Func<Task<TResult>> func)
    {
        ArgumentNullException.ThrowIfNull(func);
        Log("START", context);

        try
        {
            using (_store.SetCurrent(context))
            {
                return await func().ConfigureAwait(false);
            }
        }
        finally
        {
            Log("END", context);
        }
    }

    private void Log(string phase, TContext context)
    {
        if (_logger is null) return;
        string contextSummary = context.ToString() ?? typeof(TContext).Name;
        _logger.LogInformation("{Phase} Context {ContextType}: {ContextData}", phase, typeof(TContext).Name, contextSummary);
    }
}
