using ModularityKit.Context.Abstractions;

namespace Multi_Tenant_Authorization.Context;

public sealed class TenantUserContext : IContext
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public DateTimeOffset CreatedAt { get; }
    public string TenantId { get; private set; } = "default-tenant";
    private readonly HashSet<string> _roles = [];

    public IReadOnlyCollection<string> Roles => _roles;

    public void AddRole(string role) => _roles.Add(role);
    public void ChangeTenant(string tenantId) => TenantId = tenantId;
}