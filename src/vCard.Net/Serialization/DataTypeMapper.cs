﻿using vCard.Net.DataTypes;

namespace vCard.Net.Serialization;

/// <summary>
/// Delegate for resolving the type of an object based on context.
/// </summary>
/// <param name="context">The context used to resolve the type.</param>
/// <returns>The resolved type.</returns>
public delegate Type TypeResolverDelegate(object context);

/// <summary>
/// Class responsible for mapping data types to properties.
/// </summary>
internal class DataTypeMapper
{
    private class PropertyMapping
    {
        /// <summary>
        /// Gets or sets the object type associated with the property.
        /// </summary>
        public Type ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the resolver delegate for dynamically resolving the type.
        /// </summary>
        public TypeResolverDelegate Resolver { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether multiple values per property are allowed.
        /// </summary>
        public bool AllowsMultipleValuesPerProperty { get; set; }
    }

    private readonly IDictionary<string, PropertyMapping> _propertyMap = new Dictionary<string, PropertyMapping>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Initializes a new instance of the <see cref="DataTypeMapper"/> class.
    /// </summary>
    public DataTypeMapper()
    {
        AddPropertyMapping("ADR", typeof(Address), true);
        AddPropertyMapping("ANNIVERSARY", typeof(IDateTime), false);
        AddPropertyMapping("BIRTHPLACE", typeof(BirthPlace), false);
        AddPropertyMapping("CATEGORIES", typeof(Categories), false);
        AddPropertyMapping("CLIENTPIDMAP", typeof(ClientPidMap), true);
        AddPropertyMapping("DEATHDATE", typeof(IDateTime), false);
        AddPropertyMapping("DEATHPLACE", typeof(DeathPlace), false);
        AddPropertyMapping("EMAIL", typeof(Email), true);
        AddPropertyMapping("EXPERTISE", typeof(Expertise), false);
        AddPropertyMapping("GENDER", typeof(Gender), false);
        AddPropertyMapping("GEO", typeof(GeographicPosition), false);
        AddPropertyMapping("HOBBY", typeof(Hobby), false);
        AddPropertyMapping("IMPP", typeof(IMPP), true);
        AddPropertyMapping("INTEREST", typeof(Interest), false);
        AddPropertyMapping("KEY", typeof(Key), false);
        AddPropertyMapping("KIND", typeof(Kind), false);
        AddPropertyMapping("N", typeof(StructuredName), false);
        AddPropertyMapping("LABEL", typeof(Label), true);
        AddPropertyMapping("LANG", typeof(Language), true);
        AddPropertyMapping("LOGO", typeof(Logo), false);
        AddPropertyMapping("ORG", typeof(Organization), false);
        AddPropertyMapping("PHOTO", typeof(Photo), false);
        AddPropertyMapping("RELATED", typeof(Related), true);
        AddPropertyMapping("REV", typeof(IDateTime), false);
        AddPropertyMapping("SOUND", typeof(Sound), false);
        AddPropertyMapping("SOURCE", typeof(Source), false);
        AddPropertyMapping("TEL", typeof(Telephone), true);
        AddPropertyMapping("URL", typeof(Url), true);
        AddPropertyMapping("XML", typeof(Xml), false);
    }

    /// <summary>
    /// Adds a property mapping to the mapper.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="objectType">The type associated with the property.</param>
    /// <param name="allowsMultipleValues">Indicates whether multiple values per property are allowed.</param>
    public void AddPropertyMapping(string name, Type objectType, bool allowsMultipleValues)
    {
        if (name == null || objectType == null)
        {
            return;
        }

        var m = new PropertyMapping
        {
            ObjectType = objectType,
            AllowsMultipleValuesPerProperty = allowsMultipleValues
        };

        _propertyMap[name] = m;
    }

    /// <summary>
    /// Adds a property mapping with a resolver delegate to the mapper.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="resolver">The resolver delegate for dynamically resolving the type.</param>
    /// <param name="allowsMultipleValues">Indicates whether multiple values per property are allowed.</param>
    public void AddPropertyMapping(string name, TypeResolverDelegate resolver, bool allowsMultipleValues)
    {
        if (name == null || resolver == null)
        {
            return;
        }

        var m = new PropertyMapping
        {
            Resolver = resolver,
            AllowsMultipleValuesPerProperty = allowsMultipleValues
        };

        _propertyMap[name] = m;
    }

    /// <summary>
    /// Removes a property mapping from the mapper.
    /// </summary>
    /// <param name="name">The name of the property to remove.</param>
    public void RemovePropertyMapping(string name)
    {
        if (name != null && _propertyMap.ContainsKey(name))
        {
            _propertyMap.Remove(name);
        }
    }

    /// <summary>
    /// Determines whether the property allows multiple values.
    /// </summary>
    /// <param name="obj">The object representing the property.</param>
    /// <returns><c>true</c> if the property allows multiple values; otherwise, <c>false</c>.</returns>
    public virtual bool GetPropertyAllowsMultipleValues(object obj)
    {
        var p = obj as IVCardProperty;
        return !string.IsNullOrWhiteSpace(p?.Name)
            && _propertyMap.TryGetValue(p.Name, out var m)
            && m.AllowsMultipleValuesPerProperty;
    }

    /// <summary>
    /// Gets the mapping for the property.
    /// </summary>
    /// <param name="obj">The object representing the property.</param>
    /// <returns>The type mapping for the property.</returns>
    public virtual Type GetPropertyMapping(object obj)
    {
        var p = obj as IVCardProperty;

        if (p?.Name == null)
        {
            return null;
        }

        if (!_propertyMap.TryGetValue(p.Name, out var m))
        {
            return null;
        }

        return m.Resolver == null
        ? m.ObjectType
        : m.Resolver(p);
    }
}