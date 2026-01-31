# ADR-003: Context Execution Manager Model (IContextManager)

## Tag
#adr_003

## Status
Accepted

## Date
2025-12-19

## Scope
ModularityKit.Context.Abstractions

## Context

Some operations must be executed within a specific execution context to ensure proper scoping, isolation, and lifecycle handling. The mechanism used to activate and manage a context during execution should be abstracted away from consumers.

To avoid coupling application logic to a concrete context storage or propagation strategy, a dedicated abstraction is required to manage context activation for the duration of an operation.

___
## Decision

### IContextManager

**Responsibilities:**

- Manage activation of a context for the duration of an execution.
- Expose the currently active context.
- Execute asynchronous actions within a given context.
- Execute asynchronous functions within a given context and return a result.
- Ensure the provided context is active only for the execution scope.

```csharp
public interface IContextManager<TContext>
    where TContext : class, IContext
{
    TContext? Current { get; }

    Task ExecuteInContext(TContext context, Func<Task> action);

    Task<TResult> ExecuteInContext<TResult>(
        TContext context,
        Func<Task<TResult>> func);
}

```

___
## Design Rationale

- Separating context execution from context access clarifies responsibilities.
- The abstraction hides context propagation details (ambient, async-local, scoped).
- Explicit execution boundaries prevent context leakage across operations.
- Supporting both actions and functions covers common execution scenarios.
- The model remains infrastructure-focused and free of domain concerns.