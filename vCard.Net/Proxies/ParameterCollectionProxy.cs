using System;
using System.Collections.Generic;
using System.Linq;
using vCard.Net.Collections;
using vCard.Net.Collections.Proxies;

namespace vCard.Net.Proxies
{
    public class ParameterCollectionProxy : GroupedCollectionProxy<string, CardParameter, CardParameter>, IParameterCollection
    {
        protected GroupedValueList<string, CardParameter, CardParameter, string> Parameters
            => RealObject as GroupedValueList<string, CardParameter, CardParameter, string>;

        public ParameterCollectionProxy(IGroupedList<string, CardParameter> realObject) : base(realObject) { }

        public virtual void SetParent(ICardObject parent)
        {
            foreach (var parameter in this)
            {
                parameter.Parent = parent;
            }
        }

        public virtual void Add(string name, string value) => RealObject.Add(new CardParameter(name, value));

        public virtual string Get(string name)
        {
            var parameter = RealObject.FirstOrDefault(o => string.Equals(o.Name, name, StringComparison.Ordinal));

            return parameter?.Value;
        }

        public virtual IList<string> GetMany(string name) => new GroupedValueListProxy<string, CardParameter, CardParameter, string, string>(Parameters, name);

        public virtual void Set(string name, string value)
        {
            var parameter = RealObject.FirstOrDefault(o => string.Equals(o.Name, name, StringComparison.Ordinal));

            if (parameter == null)
            {
                RealObject.Add(new CardParameter(name, value));
            }
            else
            {
                parameter.SetValue(value);
            }
        }

        public virtual void Set(string name, IEnumerable<string> values)
        {
            var parameter = RealObject.FirstOrDefault(o => string.Equals(o.Name, name, StringComparison.Ordinal));

            if (parameter == null)
            {
                RealObject.Add(new CardParameter(name, values));
            }
            else
            {
                parameter.SetValue(values);
            }
        }

        public virtual int IndexOf(CardParameter obj) => 0;

        public virtual void Insert(int index, CardParameter item) { }

        public virtual void RemoveAt(int index) { }

        public virtual CardParameter this[int index]
        {
            get => Parameters[index];
            set { }
        }
    }
}