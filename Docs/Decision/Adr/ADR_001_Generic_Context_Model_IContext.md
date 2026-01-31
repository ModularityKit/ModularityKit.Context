# ADR-001: Generic Context Model (IContext)

## Tag
#adr_001

## Status
Accepted

## Date
2025-12-19

## Scope
ModularityKit.Context.Abstractions

## Context

The system requires minimal and consistent way to represent an execution context instance. This context is intended solely for infrastructure-level concerns such as identification, lifecycle tracking, and correlation.

The context must **not** introduce any domain semantics and must remain fully isolated from mutation logic, domain models, and feature code. To ensure consistency and extensibility, a simple abstraction is required that can be implemented by different context providers without leaking into other modules.

___
## Decision

### IContext

**Responsibilities:**

- Represent single execution context instance.
- Provide unique identifier for correlation purposes.
- Track the creation time of the context.
- Serve as base abstraction for infrastructure-level context handling.
```csharp
public interface IContext
{
    string Id { get; }
    DateTimeOffset CreatedAt { get; }
}
```

___
## Design Rationale

- A minimal abstraction avoids coupling context handling with domain or mutation logic.
- Keeping `IContext` isolated within the Context module prevents context leakage into other layers.
- A string-based identifier allows flexibility in identifier formats (GUID, ULID, trace-id).
- The model is stable, extensible, and suitable as a foundational infrastructure primitive.