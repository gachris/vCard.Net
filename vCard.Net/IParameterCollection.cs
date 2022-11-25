using System.Collections.Generic;
using vCard.Net.Collections;

namespace vCard.Net
{
    public interface IParameterCollection : IGroupedList<string, CardParameter>
    {
        void SetParent(ICardObject parent);
        void Add(string name, string value);
        string Get(string name);
        IList<string> GetMany(string name);
        void Set(string name, string value);
        void Set(string name, IEnumerable<string> values);
    }
}