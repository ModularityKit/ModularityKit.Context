using Multi_Tenant_Authorization.Interfaces;

namespace Multi_Tenant_Authorization.Scenarios;

internal static class TenantSwitchScenario
{
    public static void Run(ITrustedService trusted)
    {
        Console.WriteLine("\n=== TenantSwitchScenario ===");
        trusted.SwitchTenant("tenant-999");
    }
}