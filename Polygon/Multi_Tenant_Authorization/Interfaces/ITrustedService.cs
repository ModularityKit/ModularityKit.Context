namespace Multi_Tenant_Authorization.Interfaces;

/// <summary>
/// Interface for trusted service that can modify tenant context and assign roles.
/// </summary>
public interface ITrustedService
{
    /// <summary>
    /// Assign a role to a user within the current tenant context.
    /// </summary>
    void AssignRole(string user, string role);

    /// <summary>
    /// Switch the current tenant.
    /// </summary>
    void SwitchTenant(string newTenant);
}