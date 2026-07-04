using Multi_Tenant_Authorization.Interfaces;

namespace Multi_Tenant_Authorization.Scenarios;

internal static class RoleAssignmentScenario
{
    public static void Run(ITrustedService trusted)
    {
        Console.WriteLine("\n=== RoleAssignmentScenario ===");
        trusted.AssignRole("alice", "Manager");
    }
}