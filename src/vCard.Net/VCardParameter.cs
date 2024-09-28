using System.Diagnostics;
using System.Runtime.Serialization;
using vCard.Net.Collections.Interfaces;

namespace vCard.Net;

/// <summary>
/// Represents a parameter within a vCard object.
/// </summary>
[DebuggerDisplay("{Name}={string.Join(\",\", Values)}")]
public class VCardParameter : VCardObject, IValueObject<string>
{
    private HashSet<string> _values;

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardParameter"/> class.
    /// </summary>
    public VCardParameter() => Initialize();

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardParameter"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    public VCardParameter(string name) : base(name) => Initialize();

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardParameter"/> class with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="value">The value of the parameter.</param>
    public VCardParameter(string name, string value) : base(name)
    {
        Initialize();
        AddValue(value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardParameter"/> class with the specified name and values.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="values">The values of the parameter.</param>
    public VCardParameter(string name, IEnumerable<string> values) : base(name)
    {
        Initialize();
        foreach (var v in values)
        {
            AddValue(v);
        }
    }

    private void Initialize() => _values = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <inheritdoc/>
    protected override void OnDeserializing(StreamingContext context)
    {
        base.OnDeserializing(context);

        Initialize();
    }

    /// <inheritdoc/>
    public override void CopyFrom(ICopyable c)
    {
        base.CopyFrom(c);

        var p = c as VCardParameter;
        if (p?.Values == null)
        {
            return;
        }

        _values = new HashSet<string>(p.Values.Where(IsValidValue), StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Gets the values of the parameter.
    /// </summary>
    public virtual IEnumerable<string> Values => _values;

    /// <summary>
    /// Determines whether the parameter contains a specific value.
    /// </summary>
    /// <param name="value">The value to locate in the parameter.</param>
    /// <returns><c>true</c> if the parameter contains the value; otherwise, <c>false</c>.</returns>
    public virtual bool ContainsValue(string value) => _values.Contains(value);

    /// <summary>
    /// Gets the number of values in the parameter.
    /// </summary>
    public virtual int ValueCount => _values?.Count ?? 0;

    /// <summary>
    /// Sets the value of the parameter.
    /// </summary>
    /// <param name="value">The value to set.</param>
    public virtual void SetValue(string value)
    {
        _values.Clear();
        _values.Add(value);
    }

    /// <summary>
    /// Sets the values of the parameter.
    /// </summary>
    /// <param name="values">The values to set.</param>
    public virtual void SetValue(IEnumerable<string> values)
    {
        // Remove all previous values
        _values.Clear();
        _values.UnionWith(values.Where(IsValidValue));
    }

    private bool IsValidValue(string value) => !string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Adds a value to the parameter.
    /// </summary>
    /// <param name="value">The value to add.</param>
    public virtual void AddValue(string value)
    {
        if (!IsValidValue(value))
        {
            return;
        }
        _values.Add(value);
    }

    /// <summary>
    /// Removes a value from the parameter.
    /// </summary>
    /// <param name="value">The value to remove.</param>
    public virtual void RemoveValue(string value) => _values.Remove(value);

    /// <inheritdoc/>
    public virtual string Value
    {
        get => Values?.FirstOrDefault();
        set => SetValue(value);
    }
}