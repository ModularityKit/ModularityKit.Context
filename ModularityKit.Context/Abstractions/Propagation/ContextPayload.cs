namespace ModularityKit.Context.Abstractions.Propagation;

/// <summary>
/// Represents agnostic serialized context payload.
/// </summary>
/// <param name="ContentType">The media type describing the serialized payload format, such as <c>application/json</c>. </param>
/// <param name="Data">The raw bytes containing the serialized context representation</param>
/// <remarks>
/// <para><see cref="ContextPayload"/> is a transport neutral representation used
/// when propagating context data across process, protocol, or serialization
/// boundaries.
/// </para>
/// <para>
/// The payload does not impose a specific serialization format. The
/// <see cref="ContentType"/> identifies the format so that the receiving
/// component can select an appropriate serializer or deserializer.
/// </para>
/// <para>
/// The payload does not validate, deserialize, or otherwise interpret the
/// contents of <paramref name="Data"/>.
/// </para>
/// </remarks>
public readonly record struct ContextPayload(
    string ContentType,
    ReadOnlyMemory<byte> Data);