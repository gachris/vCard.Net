using System.Runtime.Serialization;

namespace vCard.Net.CardComponents;

/// <summary>
/// Represents a component within a vCard object that has a unique identifier (UID).
/// </summary>
public class UniqueComponent : vCardComponent, IUniqueComponent, IComparable<UniqueComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueComponent"/> class.
    /// </summary>
    public UniqueComponent() => EnsureProperties();

    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueComponent"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the component.</param>
    public UniqueComponent(string name) : base(name) => EnsureProperties();

    /// <summary>
    /// Ensures that the component has a unique identifier (UID).
    /// </summary>
    private void EnsureProperties()
    {
        if (string.IsNullOrEmpty(Uid))
        {
            // Create a new UID for the component
            Uid = Guid.NewGuid().ToString();
        }
    }

    /// <inheritdoc/>
    protected override void OnDeserialized(StreamingContext context)
    {
        base.OnDeserialized(context);

        EnsureProperties();
    }

    /// <summary>
    /// Compares the current component with another component based on their UIDs.
    /// </summary>
    /// <param name="other">The component to compare with the current component.</param>
    /// <returns>
    /// A value indicating the relative order of the objects being compared. The return value has the following meanings:
    /// Less than zero: This object is less than the <paramref name="other"/> parameter.
    /// Zero: This object is equal to <paramref name="other"/>.
    /// Greater than zero: This object is greater than <paramref name="other"/>.
    /// </returns>
    public int CompareTo(UniqueComponent other) => string.Compare(Uid, other.Uid, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public override bool Equals(object obj) => base.Equals(obj);

    /// <inheritdoc/>
    public override int GetHashCode() => Uid?.GetHashCode() ?? base.GetHashCode();

    /// <summary>
    /// Gets or sets the unique identifier (UID) of the component.
    /// </summary>
    /// <remarks>
    /// This value is optional. The string must be any string
    /// that can be used to uniquely identify the component. The
    /// usage of the field is determined by the software. Typical
    /// possibilities for a unique string include a URL, a GUID,
    /// or an LDAP directory path. However, there is no particular
    /// standard dictated by the vCard specification.
    /// </remarks>
    public virtual string Uid
    {
        get => Properties.Get<string>("UID");
        set => Properties.Set("UID", value);
    }
}