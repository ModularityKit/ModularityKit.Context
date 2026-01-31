# ADR-005: Read-Only Context Accessor and Snapshot

## Tag
#adr_005

## Status
Accepted

## Date
2025-12-19

## Scope
ModularityKit.Context.ReadOnly

## Context

Some consumers require access to context data without the ability to mutate it. Exposing full context objects directly can lead to unintended modifications, breaking isolation and violating architectural boundaries.

To safely provide context information to untrusted code, logging, auditing, or other read only scenarios, dedicated read only accessor and snapshot mechanism is required. This ensures that consumers receive immutable views of context data while the underlying context remains fully managed by the original accessor.

___
## Decision

### ReadOnlyContextSnapshot

**Responsibilities:**

- Provide an immutable view of an `IContext` instance.
- Implement `IReadOnlyContext` to safely expose identity (`Id`) and lifecycle (`CreatedAt`).
- Support creation from any `IContext` via a factory method (`FromContext`).
- Ensure thread safety and immutability.

```csharp
public sealed record ReadOnlyContextSnapshot(
    string Id,
    DateTimeOffset CreatedAt
) : IReadOnlyContext
{
    public static ReadOnlyContextSnapshot FromContext(IContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return new ReadOnlyContextSnapshot(
            Id: context.Id,
            CreatedAt: context.CreatedAt
        );
    }
}
```

___
### ReadOnlyContextAccessor

**Responsibilities:**

- Wrap an existing `IContextAccessor<TContext>` and expose it as `IContextAccessor<IReadOnlyContext>`.
- Convert retrieved contexts into immutable snapshots (`ReadOnlyContextSnapshot`).
- Ensure `RequireCurrent()` throws if no context is present, preserving fail-fast semantics.
- Provide safe, read only API to consumers without exposing mutability.
```csharp
public sealed class ReadOnlyContextAccessor<TContext>(
    IContextAccessor<TContext> innerAccessor
) : IContextAccessor<IReadOnlyContext> where TContext : class, IContext
{
    public IReadOnlyContext? Current
    {
        get
        {
            var context = innerAccessor.Current;
            return context != null ? ReadOnlyContextSnapshot.FromContext(context) : null;
        }
    }

    public IReadOnlyContext RequireCurrent()
    {
        var context = innerAccessor.RequireCurrent();
        return ReadOnlyContextSnapshot.FromContext(context);
    }
}
```

___
## Design Rationale

- Separates read only access from full mutable context, preventing accidental mutation.
- Snapshot approach ensures thread-safe, consistent views of context data.
- Wrapping the existing accessor avoids duplication of context propagation logic.
- Supports safe context exposure in logging, auditing, or untrusted code without changing underlying infrastructure.