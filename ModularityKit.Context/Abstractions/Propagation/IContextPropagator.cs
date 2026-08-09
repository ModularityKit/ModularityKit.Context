namespace ModularityKit.Context.Abstractions.Propagation;

/// <summary>
/// Places serialized <see cref="ContextPayload"/> into, and extracts it from,
/// transport carrier such as HTTP headers, gRPC metadata, or message queue properties.
/// </summary>
/// <typeparam name="TCarrier">The transport carrier type used at the process or protocol boundary. </typeparam>
/// <remarks>
/// <para>
/// The propagator operates on an already serialized payload and does not know
/// the serializer, serialization format, or target context type.
/// </para>
/// <para>
/// Serialization from context instance to transport-neutral representation
/// is handled separately. This contract is responsible only for moving the
/// serialized representation into and out of transport carrier.
/// </para>
/// </remarks>
public interface IContextPropagator<in TCarrier>
{
    /// <summary>
    /// Writes serialized context payload into the specified transport carrier.
    /// </summary>
    /// <remarks>
    /// The payload is written using the carrier-specific propagation mechanism.
    /// The propagator does not serialize, deserialize, or otherwise modify the
    /// context represented by the payload.
    /// </remarks>
    /// <param name="payload">The serialized context representation to write into the carrier.</param>
    /// <param name="carrier">The transport carrier to populate.</param>
    void Inject(ContextPayload payload, TCarrier carrier);

    /// <summary>
    /// Reads serialized context payload from the specified transport carrier.
    /// </summary>
    /// <remarks>
    /// Extraction does not deserialize or validate the payload. The returned
    /// <see cref="ContextPayload"/> remains a transport-neutral serialized
    /// representation for processing by the appropriate serialization layer.
    /// </remarks>
    /// <param name="carrier">The transport carrier to read from.</param>
    /// <returns>
    /// The extracted serialized context payload, or <see langword="null"/>
    /// when the carrier does not contain a propagated context.
    /// </returns>
    ContextPayload? Extract(TCarrier carrier);
}