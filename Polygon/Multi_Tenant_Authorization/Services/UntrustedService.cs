using ModularityKit.Context.Abstractions;
using Multi_Tenant_Authorization.Context;
using Multi_Tenant_Authorization.Interfaces;

namespace Multi_Tenant_Authorization.Services;

internal class UntrustedService(IContextAccessor<ReadOnlyTenantContext> ctxAccessor) : IUntrustedService
{
    public void CheckAccess()
    {
        var ctx = ctxAccessor.RequireCurrent();
        Console.WriteLine($"[Untrusted] Access check: ContextID={ctx.Id}, Tenant={ctx.TenantId}");
    }

    public void TryUnauthorizedRoleAdd()
    {
        Console.WriteLine("[Untrusted] Attempting unauthorized role addition...");
        try
        {
            dynamic dynCtx = ctxAccessor.RequireCurrent();
            dynCtx.AddRole("Admin");
            Console.WriteLine("✗ Security breach!");
        }
        catch
        {
            Console.WriteLine("✓ Blocked: untrusted cannot modify roles.");
        }
    }
}