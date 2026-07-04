using Multi_Tenant_Authorization.Interfaces;

namespace Multi_Tenant_Authorization.Scenarios;

internal static class UnauthorizedAccessScenario
{
    public static void Run(IUntrustedService untrusted)
    {
        Console.WriteLine("\n=== UnauthorizedAccessScenario ===");
        untrusted.TryUnauthorizedRoleAdd();
    }
}
