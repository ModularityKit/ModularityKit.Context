using ModularityKit.Context.Abstractions;

namespace Multi_Tenant_Authorization.Context;

public sealed class ReadOnlyTenantContext(TenantUserContext inner) : IContext
{
    public string Id => inner.Id;
    public DateTimeOffset CreatedAt { get; }
    public string TenantId => inner.TenantId;
    public IReadOnlyCollection<string> Roles => inner.Roles;
}