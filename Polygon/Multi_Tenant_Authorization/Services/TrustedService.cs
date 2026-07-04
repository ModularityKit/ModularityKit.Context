using ModularityKit.Context.Abstractions;
using Multi_Tenant_Authorization.Context;
using Multi_Tenant_Authorization.Interfaces;

namespace Multi_Tenant_Authorization.Services;

internal class TrustedService(IContextAccessor<TenantUserContext> ctxAccessor) : ITrustedService
{
    public void AssignRole(string user, string role)
    {
        var ctx = ctxAccessor.RequireCurrent();
        Console.WriteLine($"[Trusted] AssignRole: {role} -> {user}");
        ctx.AddRole(role);
    }

    public void SwitchTenant(string newTenant)
    {
        var ctx = ctxAccessor.RequireCurrent();
        Console.WriteLine($"[Trusted] SwitchTenant: {newTenant}");
        ctx.ChangeTenant(newTenant);
    }
}