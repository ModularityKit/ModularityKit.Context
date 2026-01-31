# ADR-002: Context Accessor Model (IContextAccessor)

## Tag
#adr_002

## Status
Accepted

## Date
2025-12-19

## Scope
ModularityKit.Context.Abstractions

## Context

Applications using execution contexts often require a safe and consistent way to access the current context instance. Depending on the runtime model, the context may be scoped, ambient, or resolved through dependency injection.

Direct access to context implementations would introduce coupling and reduce flexibility. To maintain a clean abstraction boundary, a generic accessor is needed to expose the current context instance without leaking implementation details or enforcing a specific storage mechanism.

___
## Decision

### IContextAccessor

**Responsibilities:**

- Provide access to the current context instance of a given type.
- Allow safe, nullable access via `Current`.
- Enforce presence of a context when required via `RequireCurrent`.
- Support dependency injection and generic context resolution.
- Remain agnostic to how the context is stored or propagated.

```csharp
public interface IContextAccessor<out TContext>
    where TContext : class, IContext
{
    TContext? Current { get; }
    TContext RequireCurrent();
}
```

___
## Design Rationale

- Separating context access from context representation avoids tight coupling.
- A generic accessor enables multiple context types without additional APIs.
- Nullable access (`Current`) supports optional context scenarios.
- Explicit failure via `RequireCurrent` makes context assumptions clear and fail-fast.
- The abstraction supports both scoped and ambient context patterns without imposing either.