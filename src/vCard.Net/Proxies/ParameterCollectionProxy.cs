using System;
using System.Collections.Generic;
using System.Linq;
using vCard.Net.Collections;
using vCard.Net.Collections.Proxies;

namespace vCard.Net.Proxies;

/// <summary>
/// Represents a proxy class for a collection of vCard parameters.
/// </summary>
public class ParameterCollectionProxy : GroupedCollectionProxy<string, vCardParameter, vCardParameter>, IParameterCollection
{
    /// <summary>
    /// Gets the underlying list of parameters.
    /// </summary>
    protected GroupedValueList<string, vCardParameter, vCardParameter, string> Parameters
        => RealObject as GroupedValueList<string, vCardParameter, vCardParameter, string>;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterCollectionProxy"/> class.
    /// </summary>
    /// <param name="realObject">The real collection object to proxy.</param>
    public ParameterCollectionProxy(IGroupedList<string, vCardParameter> realObject) : base(realObject) { }

    /// <inheritdoc/>
    public virtual void SetParent(IvCardObject parent)
    {
        foreach (var parameter in this)
        {
            parameter.Parent = parent;
        }
    }

    /// <inheritdoc/>
    public virtual void Add(string name, string value) => RealObject.Add(new vCardParameter(name, value));

    /// <inheritdoc/>
    public virtual string Get(string name)
    {
        var parameter = RealObject.FirstOrDefault(o => string.Equals(o.Name, name, StringComparison.Ordinal));

        return parameter?.Value;
    }

    /// <inheritdoc/>
    public virtual IList<string> GetMany(string name) => new GroupedValueListProxy<string, vCardParameter, vCardParameter, string, string>(Parameters, name);

    /// <inheritdoc/>
    public virtual void Set(string name, string value)
    {
        var parameter = RealObject.FirstOrDefault(o => string.Equals(o.Name, name, StringComparison.Ordinal));

        if (parameter == null)
        {
            RealObject.Add(new vCardParameter(name, value));
        }
        else
        {
            parameter.SetValue(value);
        }
    }

    /// <inheritdoc/>
    public virtual void Set(string name, IEnumerable<string> values)
    {
        var parameter = RealObject.FirstOrDefault(o => string.Equals(o.Name, name, StringComparison.Ordinal));

        if (parameter == null)
        {
            RealObject.Add(new vCardParameter(name, values));
        }
        else
        {
            parameter.SetValue(values);
        }
    }

    /// <inheritdoc/>
    public virtual int IndexOf(vCardParameter obj) => 0;

    /// <inheritdoc/>
    public virtual void Insert(int index, vCardParameter item) { }

    /// <inheritdoc/>
    public virtual void RemoveAt(int index) { }

    /// <inheritdoc/>
    public virtual vCardParameter this[int index]
    {
        get => Parameters[index];
        set { }
    }
}