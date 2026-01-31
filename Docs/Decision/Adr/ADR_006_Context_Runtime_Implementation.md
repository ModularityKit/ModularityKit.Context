# ADR-006: Context Runtime Implementation

## Tag
#adr_006

## Status
Accepted

## Date
2025-12-19

## Scope
ModularityKit.Context.Runtime

## Context

The `ModularityKit.Context` infrastructure requires a concrete, runtime implementation for:
- storing and retrieving the current context instance in thread safe and async safe manner,
- exposing the current context to consumers via `IContextAccessor`,
- managing the activation and lifetime of a context during code execution via `IContextManager`.

A runtime layer is needed to ensure proper propagation of context in asynchronous and multithreaded environments while maintaining clean separation from domain logic.

___
## Decision

### ContextStore

**Responsibilities:**

- Store the currently active context using `AsyncLocal<TContext>`.    
- Provide access to the current context instance via `Current`.
- Allow setting a new context and restoring the previous one via a disposable scope (`SetCurrent`).
- Provide `Clear()` to remove the active context.

```csharp
public sealed class ContextStore<TContext> where TContext : class, IContext
{
    private static readonly AsyncLocal<TContext?> CurrentContext = new();

    public TContext? Current
    {
        get => CurrentContext.Value;
        private set => CurrentContext.Value = value;
    }

    public IDisposable SetCurrent(TContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var previous = Current;
        Current = context;
        return new ContextScope(this, previous);
    }

    public void Clear() => Current = null;

    private sealed class ContextScope(ContextStore<TContext> store, TContext? previousContext) : IDisposable
    {
        private int _disposed;

        public void Dispose()
        {
            if (Interlocked.Exchange(ref _disposed, 1) == 0)
                store.Current = previousContext;
        }
    }
}
```

___
### ContextAccessor

**Responsibilities:**

- Implement `IContextAccessor<TContext>` to expose the current context.
- Delegate retrieval of the current context to `ContextStore`.
- Provide `RequireCurrent()` to throw an exception if no context is active.

```csharp
public sealed class ContextAccessor<TContext>(ContextStore<TContext> store) : IContextAccessor<TContext>
    where TContext : class, IContext
{
    public TContext? Current => store.Current;

    public TContext RequireCurrent()
    {
        return Current ?? throw new InvalidOperationException(
            $"No active {typeof(TContext).Name} found. Ensure the context is set before accessing it.");
    }
}
```

___
### ContextManager

**Responsibilities:**

- Implement `IContextManager<TContext>` to execute code within a given context scope.
- Activate the provided context during execution and restore the previous context afterward.
- Support both `Task` returning actions and `Task<TResult>` functions.
- Optionally log the start and end of context execution for observability.

```csharp
public sealed class ContextManager<TContext> : IContextManager<TContext>
    where TContext : class, IContext
{
    private readonly ContextStore<TContext> _store;
    private readonly ILogger<ContextManager<TContext>>? _logger;

    public ContextManager(ContextStore<TContext> store, ILogger<ContextManager<TContext>>? logger = null)
    {
        _store = store;
        _logger = logger;
    }

    public TContext? Current => _store.Current;

    public async Task ExecuteInContext(TContext context, Func<Task> action)
    {
        Log("START", context);
        using (_store.SetCurrent(context))
            await action();
        Log("END", context);
    }

    public async Task<TResult> ExecuteInContext<TResult>(TContext context, Func<Task<TResult>> func)
    {
        Log("START", context);
        TResult result;
        using (_store.SetCurrent(context))
            result = await func();
        Log("END", context);
        return result;
    }

    private void Log(string phase, TContext context)
    {
        if (_logger is null) return;
        string summary = context.ToString() ?? typeof(TContext).Name;
        _logger.LogInformation("{Phase} Context {ContextType}: {ContextData}", phase, typeof(TContext).Name, summary);
    }
}
```

___
## Design Rationale

- `ContextStore` ensures **thread-safe and async-safe** context propagation.
- `ContextAccessor` separates access concerns from execution concerns.
- `ContextManager` provides explicit **context scoping** for safe execution.
- The design is fully **infrastructure-focused**, isolated from domain logic.
- Using `AsyncLocal` allows context to flow naturally in async operations without leaking to unrelated code.