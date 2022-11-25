using System.Collections.Generic;
using vCard.Net.Collections;

namespace vCard.Net
{
    public class ParameterList : GroupedValueList<string, CardParameter, CardParameter, string>, IParameterCollection
    {
        public virtual void SetParent(ICardObject parent)
        {
            foreach (var parameter in this)
            {
                parameter.Parent = parent;
            }
        }

        public virtual void Add(string name, string value) => Add(new CardParameter(name, value));

        public virtual string Get(string name) => Get<string>(name);

        public virtual IList<string> GetMany(string name) => GetMany<string>(name);
    }
}