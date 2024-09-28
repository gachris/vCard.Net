using vCard.Net.Serialization.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents a label in a vCard.
/// </summary>
public class Label : EncodableDataType
{
    /// <summary>
    /// Gets or sets the list of types associated with this label.
    /// </summary>
    public virtual IList<string> Types
    {
        get => Parameters.GetMany("TYPE");
        set => Parameters.Set("TYPE", value);
    }

    /// <summary>
    /// Gets or sets the value of the label.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Label"/> class.
    /// </summary>
    public Label()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Label"/> class with the specified value.
    /// </summary>
    /// <param name="value">The label value.</param>
    public Label(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new LabelSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Label"/> object is equal to another <see cref="Label"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Label"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Label other)
    {
        // Normalize line endings to \n for both Value properties before comparison
        string normalizedValue = NormalizeLineEndings(Value);
        string normalizedOtherValue = NormalizeLineEndings(other.Value);

        return string.Equals(normalizedValue, normalizedOtherValue, StringComparison.InvariantCultureIgnoreCase)
               && CollectionHelpers.Equals(Types, other.Types);
    }

    /// <summary>
    /// Normalizes line endings to Unix style (\n).
    /// </summary>
    /// <param name="input">The string to normalize.</param>
    /// <returns>A string with normalized line endings.</returns>
    private string NormalizeLineEndings(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        // Replace Windows line endings (\r\n) with Unix line endings (\n)
        return input.Replace("\r\n", "\n");
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Label)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = 17;
            hashCode = hashCode * 23 + (Value != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Value) : 0);
            hashCode = (hashCode * 23) ^ CollectionHelpers.GetHashCode(Types);
            return hashCode;
        }
    }
}