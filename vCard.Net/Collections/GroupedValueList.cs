using System;
using System.Collections.Generic;
using System.Linq;
using vCard.Net.Collections.Interfaces;
using vCard.Net.Collections.Proxies;

namespace vCard.Net.Collections;

/// <summary>
/// Represents a grouped list of objects with associated values.
/// </summary>
/// <typeparam name="TGroup">The type of the group for grouping items.</typeparam>
/// <typeparam name="TInterface">The type of interface implemented by the grouped objects.</typeparam>
/// <typeparam name="TItem">The type of items in the grouped list.</typeparam>
/// <typeparam name="TValueType">The type of the value associated with each item.</typeparam>
public class GroupedValueList<TGroup, TInterface, TItem, TValueType> : GroupedList<TGroup, TInterface>
    where TInterface : class, IGroupedObject<TGroup>, IValueObject<TValueType>
    where TItem : new()
{
    /// <summary>
    /// Sets the value for the specified group.
    /// </summary>
    /// <param name="group">The group to set the value for.</param>
    /// <param name="value">The value to set.</param>
    public virtual void Set(TGroup group, TValueType value)
    {
        Set(group, [value]);
    }

    /// <summary>
    /// Sets the values for the specified group.
    /// </summary>
    /// <param name="group">The group to set the values for.</param>
    /// <param name="values">The values to set.</param>
    public virtual void Set(TGroup group, IEnumerable<TValueType> values)
    {
        if (ContainsKey(group))
        {
            AllOf(group)?.FirstOrDefault()?.SetValue(values);
            return;
        }

        // No matching item was found, add a new item to the list
        var obj = Activator.CreateInstance(typeof(TItem)) as TInterface;
        obj.Group = group;
        Add(obj);
        obj.SetValue(values);
    }

    /// <summary>
    /// Gets the value of the specified type for the specified group.
    /// </summary>
    /// <typeparam name="TType">The type of the value to retrieve.</typeparam>
    /// <param name="group">The group to retrieve the value for.</param>
    /// <returns>The value of the specified type for the group.</returns>
    public virtual TType Get<TType>(TGroup group)
    {
        var firstItem = AllOf(group).FirstOrDefault();
        if (firstItem?.Values != null)
        {
            return firstItem
                .Values
                .OfType<TType>()
                .FirstOrDefault();
        }
        return default;
    }

    /// <summary>
    /// Gets a list of values of the specified type for the specified group.
    /// </summary>
    /// <typeparam name="TType">The type of the values to retrieve.</typeparam>
    /// <param name="group">The group to retrieve the values for.</param>
    /// <returns>A list of values of the specified type for the group.</returns>
    public virtual IList<TType> GetMany<TType>(TGroup group)
    {
        return new GroupedValueListProxy<TGroup, TInterface, TItem, TValueType, TType>(this, group);
    }
}