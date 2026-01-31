# ADR-004: Read-Only Context Model (IReadOnlyContext)

## Tag
#adr_004

## Status
Accepted

## Date
2025-12-19

## Scope
ModularityKit.Context.Abstractions

## Context

Some consumers require access to context information without the ability to modify or influence the context lifecycle. Exposing full context capabilities in such cases would blur responsibility boundaries and increase the risk of misuse.

To clearly distinguish between mutable context handling and safe, read-only access, a dedicated abstraction is required that represents an immutable view of a context.

___
## Decision

### IReadOnlyContext

**Responsibilities:**

- Represent read only view of context instance.
- Expose context identity and lifecycle information without mutation capabilities.
- Provide a semantic boundary for consumers that must not modify context state.

```csharp
public interface IReadOnlyContext : IContext
{
}
```
___
## Design Rationale

- A marker-style abstraction communicates intent without increasing complexity.
- Explicit read-only typing improves API clarity and consumer expectations.
- The model avoids defensive copying while maintaining architectural boundaries.
- It supports safe context exposure in infrastructure and integration scenarios.
