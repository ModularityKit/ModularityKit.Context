namespace ModularityKit.Context.Models;

/// <summary>
/// Describes the terminal state of a context execution.
/// </summary>
public enum ContextExecutionState
{
    Succeeded = 0,
    Faulted = 1,
    Canceled = 2,
}
