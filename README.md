# Context System

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)

A thread safe, async safe context management system with built-in security boundaries for .NET applications.

## Features

- **Thread-Safe & Async-Safe** - Built on `AsyncLocal<T>` for automatic context isolation
- **Security Boundaries** - Defensive copy pattern prevents untrusted code from accessing sensitive data
- **Multi-Tenant Ready** - Perfect for SaaS applications requiring tenant isolation
- **Plugin-Friendly** - Safe context exposure to third-party plugins
- **Distributed Tracing** - Built-in support for correlation IDs and trace context
-  **High Performance** - Minimal overhead (~100ns per operation)

---

## Quick Start
```csharp
using ModularityKit.Context.Abstractions;
using ModularityKit.Context.AspNet;

// 1. Define your context
public class MyContext : IContext
{
    public string Id { get; }
    public DateTimeOffset CreatedAt { get; }
    public string UserId { get; }
    public string TenantId { get; }
}

// 2. Register in DI
services.AddContext();

// 3. Use in your code
public class OrderService(IContextAccessor<MyContext> context)
{
    public void ProcessOrder()
    {
        var ctx = context.RequireCurrent();
        Console.WriteLine($"Processing for user: {ctx.UserId}");
    }
}

// 4. Execute with context
var context = new MyContext("ctx-1", "user-123", "tenant-456");

await contextManager.ExecuteInContext(context, async () =>
{
    orderService.ProcessOrder();
});
```

---

## 🎯 Why Use Context System?

### Without Context System ❌
```csharp
public class OrderService
{
    // Passing context everywhere - painful!
    public async Task CreateOrder(string userId, string tenantId, string correlationId)
    {
        await ValidateOrder(userId, tenantId, correlationId);
        await ProcessPayment(userId, tenantId, correlationId);
        await SendEmail(userId, tenantId, correlationId);
    }
}
```

### With Context System ✅
```csharp
public class OrderService(IContextAccessor<MyContext> context)
{
    // Clean API - context flows automatically
    public async Task CreateOrder()
    {
        await ValidateOrder();   // Context flows automatically
        await ProcessPayment();
        await SendEmail();
    }
}
```

---

## Documentation

### Getting Started
- **[Installation & Setup](Docs/Getting-Started.md)** - Complete setup guide
- **[Basic Usage](Docs/Basic-Usage.md)** - Learn the fundamentals
- **[Core Concepts](Docs/Core-Concepts.md)** - Understanding the architecture

### Guides
- **[Multi-Tenant Applications](Docs/Multi-Tenant-Guide.md)** - Building SaaS applications

### Reference
- **[API Reference](Docs/API-Reference.md)** - Complete API documentation
- **[Best Practices](Docs/Best-Practices.md)** - Tips and recommendations

---

### Multi Tenant SaaS
```csharp
public class DocumentService(IContextAccessor<MyContext> context)
{
    public async Task GetDocument(string documentId)
    {
        var ctx = context.RequireCurrent();
        
        // Automatic tenant filtering
        return await _db.Documents
            .Where(d => d.TenantId == ctx.TenantId)
            .Where(d => d.Id == documentId)
            .FirstOrDefaultAsync();
    }
}
```
> Automatic tenant isolation using `IContextAccessor` ensures security and data separation.

[**See full example →**](Docs/Multi-Tenant-Guide.md)

---

## Architecture
```
    Application Layer
        ├─ Trusted Code (Full Access)-> IContextAccessor<MyContext>
        └─ Untrusted Code (Read-Only) -> IContextAccessor<IReadOnlyContext>
                │
                ▼
    Context Infrastructure
        ├─ ContextStore (AsyncLocal)
        ├─ ContextAccessor
        └─ ContextManager
                │
                ▼
    Security Boundary
        └─ ReadOnlyContextSnapshot (Defensive Copy)
```

[**Learn more about architecture →**](Docs/Architecture.md)

---

## Best Practices

### DO ✅
- Create context per operation (request/job)
- Use `RequireCurrent()` when context is expected
- Let `ExecuteInContext` handle cleanup
- Treat contexts as immutable

### DON'T ❌
- Share context across operations
- Mutate context properties
- Use `Current` without null check
- Store context in static fields

[**Read full best practices guide →**](Docs/Best-Practices.md)

---

## API Reference

### Core Interfaces
- **[IContext](Docs/API-Reference.md#icontext)** - Base context interface
- **[IContextAccessor\<TContext\>](Docs/API-Reference.md#icontextaccessortcontext)** - Access current context
- **[IContextManager\<TContext\>](Docs/API-Reference.md#icontextmanagertcontext)** - Manage context lifecycle

### Extension Methods
- **[AddContext\<TContext\>()](Docs/API-Reference.md#addcontexttcontext)** - Register context infrastructure
- **[AddReadOnlyContextAccessor\<TContext\>()](Docs/API-Reference.md#addreadonlycontextaccessor)** - Register read-only accessor

[**View complete API reference →**](Docs/API-Reference.md)

---

> **Built with ❤️ for .NET developers**