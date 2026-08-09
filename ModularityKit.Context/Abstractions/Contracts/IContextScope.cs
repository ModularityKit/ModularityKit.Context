namespace ModularityKit.Context.Abstractions.Contracts;

/// <summary>
/// Represents an active context scope that restores the previous context when disposed.
/// </summary>
/// <remarks>
/// A context scope defines bounded lifetime for an active context.
/// When the scope is disposed of, the context that was active before the scope
/// was created is restored.
/// </remarks>
/// <typeparam name="TContext">The type of context managed by the scope. </typeparam>
public interface IContextScope<out TContext> : IDisposable
    where TContext : class, IContext
{
    /// <summary>
    /// Gets the context that is active inside this scope.
    /// </summary>
    TContext Context { get; }

    /// <summary>
    /// Gets the context that was active before this scope was created.
    /// </summary>
    /// <remarks>
    /// This value is <see langword="null"/> when the scope was created without
    /// previously active context.
    /// </remarks>
    TContext? PreviousContext { get; }

    /// <summary>
    /// Gets the timestamp when the scope was created.
    /// </summary>
    DateTimeOffset StartedAt { get; }

    /// <summary>
    /// Gets the timestamp when the scope was disposed, if it has been disposed.
    /// </summary>
    /// <value>
    /// The disposal timestamp, or <see langword="null"/> when the scope is still active.
    /// </value>
    DateTimeOffset? DisposedAt { get; }

    /// <summary>
    /// Gets value indicating whether the scope has been disposed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the scope has been disposed; otherwise,
    /// <see langword="false"/>.
    /// </value>
    bool IsDisposed { get; }
}