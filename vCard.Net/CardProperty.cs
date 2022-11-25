using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using vCard.Net.CardComponents;

namespace vCard.Net
{
    /// <summary>
    /// A class that represents a property of the <see cref="Card"/>
    /// itself or one of its components.  It can also represent non-standard
    /// (X-) properties of an vCard component, as seen with many
    /// applications.
    /// </summary>
    /// <remarks>
    /// Currently, the "known" properties for an vCard are as
    /// follows:
    /// <list type="bullet">
    ///     <item>ProdID</item>
    ///     <item>Version</item>
    ///     <item>Method</item>
    /// </list>
    /// There may be other, custom X-properties applied to the vCard,
    /// and X-properties may be applied to vCard components.
    /// </remarks>
    [DebuggerDisplay("{Name}:{Value}")]
    public class CardProperty : CardObject, ICardProperty
    {
        private List<object> _values = new List<object>();

        /// <summary>
        /// Returns a list of parameters that are associated with the vCard object.
        /// </summary>
        public virtual IParameterCollection Parameters { get; protected set; } = new ParameterList();

        public CardProperty() { }

        public CardProperty(string name) : base(name) { }

        public CardProperty(string name, object value) : base(name) => _values.Add(value);

        public CardProperty(int line, int col) : base(line, col) { }

        /// <summary>
        /// Adds a parameter to the vCard object.
        /// </summary>
        public virtual void AddParameter(string name, string value)
        {
            var p = new CardParameter(name, value);
            Parameters.Add(p);
        }

        /// <summary>
        /// Adds a parameter to the vCard object.
        /// </summary>
        public virtual void AddParameter(CardParameter p) => Parameters.Add(p);

        public override void CopyFrom(ICopyable obj)
        {
            base.CopyFrom(obj);

            if (!(obj is ICardProperty p))
            {
                return;
            }

            SetValue(p.Values);
        }

        public virtual IEnumerable<object> Values => _values;

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

        public virtual bool ContainsValue(object value) => _values.Contains(value);

        public virtual int ValueCount => _values?.Count ?? 0;

        public virtual void SetValue(object value)
        {
            if (_values.Count == 0)
            {
                _values.Add(value);
            }
            else if (value != null)
            {
                // Our list contains values.  Let's set the first value!
                _values[0] = value;
            }
            else
            {
                _values.Clear();
            }
        }

        public virtual void SetValue(IEnumerable<object> values)
        {
            // Remove all previous values
            _values.Clear();
            var toAdd = values ?? Enumerable.Empty<object>();
            _values.AddRange(toAdd);
        }

        public virtual void AddValue(object value)
        {
            if (value == null)
            {
                return;
            }

            _values.Add(value);
        }

        public virtual void RemoveValue(object value)
        {
            if (value == null)
            {
                return;
            }
            _values.Remove(value);
        }
    }
}