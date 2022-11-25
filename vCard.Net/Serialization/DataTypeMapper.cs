using System;
using System.Collections.Generic;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization
{
    public delegate Type TypeResolverDelegate(object context);

    internal class DataTypeMapper
    {
        private class PropertyMapping
        {
            public Type ObjectType { get; set; }
            public TypeResolverDelegate Resolver { get; set; }
            public bool AllowsMultipleValuesPerProperty { get; set; }
        }

        private readonly IDictionary<string, PropertyMapping> _propertyMap = new Dictionary<string, PropertyMapping>(StringComparer.OrdinalIgnoreCase);

        public DataTypeMapper()
        {
            #region General Properties

            AddPropertyMapping("SOURCE", typeof(Source), false);
            AddPropertyMapping("KIND", typeof(Kind), false);

            #endregion

            #region Identification Properties

            AddPropertyMapping("N", typeof(Name), false);
            //AddPropertyMapping("PHOTO", typeof(Photo), false);
            AddPropertyMapping("BDAY", typeof(IDateTime), false);
            AddPropertyMapping("ANNIVERSARY", typeof(IDateTime), false);
            AddPropertyMapping("GENDER", typeof(Gender), false);

            #endregion

            #region Delivery Addressing Properties

            //AddPropertyMapping("ADR", typeof(Address), true);

            #endregion

            #region Communications Properties

            //AddPropertyMapping("TEL", typeof(Telephone), true);
            //AddPropertyMapping("EMAIL", typeof(Email), true);
            //AddPropertyMapping("IMPP", typeof(IMPP), true);

            #endregion

            #region Geographical Properties

            //AddPropertyMapping("TZ", typeof(Net.DataTypes.TimeZone), false);
            AddPropertyMapping("GEO", typeof(GeographicPosition), false);

            #endregion

            #region Organizational Properties

            //AddPropertyMapping("LOGO", typeof(Logo), false);
            AddPropertyMapping("ORG", typeof(Organization), false);
            //AddPropertyMapping("RELATED", typeof(Related), true);

            #endregion

            #region Explanatory Properties

            AddPropertyMapping("CATEGORIES", typeof(Categories), false);
            //AddPropertyMapping("SOUND", typeof(Sound), false);
            AddPropertyMapping("CLIENTPIDMAP", typeof(ClientPidMap), true);
            AddPropertyMapping("REV", typeof(IDateTime), false);

            #endregion

            #region Security Properties

            AddPropertyMapping("KEY", typeof(Key), false);

            #endregion
        }

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

        public void RemovePropertyMapping(string name)
        {
            if (name != null && _propertyMap.ContainsKey(name))
            {
                _propertyMap.Remove(name);
            }
        }

        public virtual bool GetPropertyAllowsMultipleValues(object obj)
        {
            var p = obj as ICardProperty;
            return !string.IsNullOrWhiteSpace(p?.Name)
                && _propertyMap.TryGetValue(p.Name, out var m)
                && m.AllowsMultipleValuesPerProperty;
        }

        public virtual Type GetPropertyMapping(object obj)
        {
            var p = obj as ICardProperty;
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
}