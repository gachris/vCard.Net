using vCard.Net.Collections;

namespace vCard.Net;

/// <summary>
/// Represents a vCard object interface that provides functionalities for managing vCard components.
/// </summary>
public interface IVCardObject : IGroupedObject<string>, ILoadable, ICopyable, IServiceProvider
{
    /// <summary>
    /// Gets or sets the name of the vCard object.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the parent of this vCard object.
    /// </summary>
    IVCardObject Parent { get; set; }

    /// <summary>
    /// Gets a collection of children of this vCard object.
    /// </summary>
    IVCardObjectList<IVCardObject> Children { get; }

    /// <summary>
    /// Gets or sets the line number where this vCard object was found during parsing.
    /// </summary>
    int Line { get; set; }

    /// <summary>
    /// Gets or sets the column number where this vCard object was found during parsing.
    /// </summary>
    int Column { get; set; }
}
