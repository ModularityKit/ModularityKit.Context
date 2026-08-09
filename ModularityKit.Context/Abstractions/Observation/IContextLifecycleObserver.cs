using ModularityKit.Context.Models.Diagnostics;

namespace ModularityKit.Context.Abstractions.Observation;

/// <summary>
/// Observes context lifecycle, receiving an event for each phase transition.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Opt-in: the event layer is inert unless observers are registered.</item>
/// <item>Observers must not change context semantics; a faulting observer is isolated.</item>
/// <item>Consumers may ignore this interface entirely and remain unaffected.</item>
/// </list>
/// </remarks>
public interface IContextLifecycleObserver
{
    /// <summary>
    /// Invoked when a context transitions to a new lifecycle phase.
    /// </summary>
    /// <remarks>
    /// The observer is notified after the lifecycle transition has been recorded.
    /// Exceptions thrown by an observer are isolated by the context infrastructure
    /// and do not alter the outcome of the observed context operation.
    /// </remarks>
    /// <param name="e">The lifecycle event describing the context transition. </param>
    void OnContextEvent(ContextLifecycleEvent e);
}