using ModularityKit.Context.ReadOnly.Snapshots;

namespace ModularityKit.Context.Abstractions.Propagation;

/// <summary>
/// Serializes context snapshot to and from format agnostic <see cref="ContextPayload"/>.
/// </summary>
/// <typeparam name="TContextSnapshot">The readonly snapshot type to serialize, must implement <see cref="IContextSnapshot"/>.</typeparam>
/// <remarks>
/// <list type="bullet">
/// <item>Operates on immutable transport snapshots so that it never exposes a live, mutable domain state.</item>
/// <item>Is format-neutral: <see cref="ContextPayload"/> carries Content Type and raw bytes, so JSON, MessagePack, and protobuf adapters are interchangeable.</item>
/// <item>Adapters live outside the core package; this is the core contract only.</item>
/// </list>
/// </remarks>
public interface IContextSerializer<TContextSnapshot>
    where TContextSnapshot : IContextSnapshot
{
    /// <summary>
    /// Serializes snapshot into <see cref="ContextPayload"/>.
    /// </summary>
    /// <param name="context">The snapshot to serialize. Must not be live, mutable context.</param>
    /// <returns>A format-agnostic payload.</returns>
    ContextPayload Serialize(TContextSnapshot context);

    /// <summary>
    /// Deserializes payload back into snapshot.
    /// </summary>
    /// <param name="payload">The payload previously produced by <see cref="Serialize"/>.</param>
    /// <returns>The reconstructed snapshot.</returns>
    TContextSnapshot Deserialize(ContextPayload payload);
}
