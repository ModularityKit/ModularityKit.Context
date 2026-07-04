namespace Multi_Tenant_Authorization.Interfaces;

/// <summary>
/// Interface for an untrusted service that can only read context information.
/// </summary>
public interface IUntrustedService
{
    /// <summary>
    /// Perform an access check for the current context.
    /// </summary>
    void CheckAccess();

    /// <summary>
    /// Attempt an unauthorized role addition (should be blocked).
    /// </summary>
    void TryUnauthorizedRoleAdd();
}