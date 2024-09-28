using System.Diagnostics;
using vCard.Net.CardComponents;

namespace vCard.Net;

/// <summary>
/// Represents a property of the <see cref="VCard"/> itself or one of its components. 
/// It can also represent non-standard (X-) properties of a vCard component, as seen with many applications.
/// </summary>
/// <remarks>
/// There may be other custom X-properties applied to the vCard, and X-properties may be applied to vCard components.
/// </remarks>
[DebuggerDisplay("{Name}:{Value}")]
public class VCardProperty : VCardObject, IVCardProperty
{
    private List<object> _values = [];

    /// <summary>
    /// Returns a collection of parameters that are associated with the vCard property.
    /// </summary>
    public virtual IParameterCollection Parameters { get; protected set; } = new ParameterList();

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardProperty"/> class.
    /// </summary>
    public VCardProperty() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardProperty"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    public VCardProperty(string name) : base(name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardProperty"/> class with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    public VCardProperty(string name, object value) : base(name) => _values.Add(value);

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardProperty"/> class with the specified line and column numbers.
    /// </summary>
    /// <param name="line">The line number where the property was found during parsing.</param>
    /// <param name="col">The column number where the property was found during parsing.</param>
    public VCardProperty(int line, int col) : base(line, col) { }

    /// <summary>
    /// Adds a parameter to the vCard property.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="value">The value of the parameter.</param>
    public virtual void AddParameter(string name, string value)
    {
        var p = new VCardParameter(name, value);
        Parameters.Add(p);
    }

    /// <summary>
    /// Adds a parameter to the vCard property.
    /// </summary>
    /// <param name="p">The parameter to add.</param>
    public virtual void AddParameter(VCardParameter p) => Parameters.Add(p);

    /// <inheritdoc/>
    public override void CopyFrom(ICopyable obj)
    {
        base.CopyFrom(obj);

        if (obj is not IVCardProperty p)
        {
            return;
        }

        SetValue(p.Values);
    }

    /// <summary>
    /// Gets the values of the property.
    /// </summary>
    public virtual IEnumerable<object> Values => _values;

    /// <summary>
    /// Gets or sets the value of the property. If the property has multiple values, only the first value is considered.
    /// </summary>
    public object Value
    {
        get => _values?.FirstOrDefault();
        set
        {
            if (value == null)
            {
                _values = null;
                return;
            }

            if (_values != null && _values.Count > 0)
            {
                _values[0] = value;
            }
            else
            {
                _values?.Clear();
                _values?.Add(value);
            }
        }
    }

    /// <summary>
    /// Determines whether the property contains the specified value.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if the property contains the value; otherwise, <c>false</c>.</returns>
    public virtual bool ContainsValue(object value) => _values.Contains(value);

    /// <summary>
    /// Gets the number of values stored in the property.
    /// </summary>
    public virtual int ValueCount => _values?.Count ?? 0;

    /// <summary>
    /// Sets the value of the property. If the property already has values, they will be replaced with the specified value.
    /// </summary>
    /// <param name="value">The value to set.</param>
    public virtual void SetValue(object value)
    {
        if (_values.Count == 0)
        {
            _values.Add(value);
        }
        else if (value != null)
        {
            // Our list contains values. Let's set the first value!
            _values[0] = value;
        }
        else
        {
            _values.Clear();
        }
    }

    /// <summary>
    /// Sets the values of the property, replacing any existing values.
    /// </summary>
    /// <param name="values">The values to set.</param>
    public virtual void SetValue(IEnumerable<object> values)
    {
        // Remove all previous values
        _values.Clear();
        var toAdd = values ?? [];
        _values.AddRange(toAdd);
    }

    /// <summary>
    /// Adds a value to the property.
    /// </summary>
    /// <param name="value">The value to add.</param>
    public virtual void AddValue(object value)
    {
        if (value == null)
        {
            return;
        }

        _values.Add(value);
    }

    /// <summary>
    /// Removes a value from the property.
    /// </summary>
    /// <param name="value">The value to remove.</param>
    public virtual void RemoveValue(object value)
    {
        if (value == null)
        {
            return;
        }
        _values.Remove(value);
    }
}