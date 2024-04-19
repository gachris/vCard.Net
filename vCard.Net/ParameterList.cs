using System.Collections.Generic;
using vCard.Net.Collections;

namespace vCard.Net;

/// <summary>
/// Represents a list of vCard parameters associated with a vCard component.
/// </summary>
public class ParameterList : GroupedValueList<string, vCardParameter, vCardParameter, string>, IParameterCollection
{
    /// <inheritdoc/>
    public virtual void SetParent(IvCardObject parent)
    {
        foreach (var parameter in this)
        {
            parameter.Parent = parent;
        }
    }

    /// <inheritdoc/>
    public virtual void Add(string name, string value) => Add(new vCardParameter(name, value));

    /// <inheritdoc/>
    public virtual string Get(string name) => Get<string>(name);

    /// <inheritdoc/>
    public virtual IList<string> GetMany(string name) => GetMany<string>(name);
}