using System.Diagnostics;
using System.Runtime.Serialization;

namespace vCard.Net.CardComponents
{
    /// <summary>
    /// This class is used by the parsing framework for vCard components.
    /// Generally, you should not need to use this class directly.
    /// </summary>
    [DebuggerDisplay("Component: {Name}")]
    public class CardComponent : CardObject, ICardComponent
    {
        /// <summary>
        /// Returns a list of properties that are associated with the vCard object.
        /// </summary>
        public virtual CardPropertyList Properties { get; protected set; }

        public CardComponent() : base() => Initialize();

        public CardComponent(string name) : base(name) => Initialize();

        private void Initialize() => Properties = new CardPropertyList(this);

        protected override void OnDeserializing(StreamingContext context)
        {
            base.OnDeserializing(context);

            Initialize();
        }

        public override void CopyFrom(ICopyable obj)
        {
            base.CopyFrom(obj);

            var c = obj as ICardComponent;
            if (c == null)
            {
                return;
            }

            Properties.Clear();
            foreach (var p in c.Properties)
            {
                Properties.Add(p);
            }
        }

        /// <summary>
        /// Adds a property to this component.
        /// </summary>
        public virtual void AddProperty(string name, string value)
        {
            var p = new CardProperty(name, value);
            AddProperty(p);
        }

        /// <summary>
        /// Adds a property to this component.
        /// </summary>
        public virtual void AddProperty(ICardProperty p)
        {
            p.Parent = this;
            Properties.Set(p.Name, p.Value);
        }
    }
}