using System;
using System.Runtime.Serialization;

namespace vCard.Net.CardComponents
{
    /// <summary>
    /// Represents a unique component, a component with a unique UID,
    /// which can be used to uniquely identify the component.    
    /// </summary>
    public class UniqueComponent : CardComponent, IUniqueComponent, IComparable<UniqueComponent>
    {
        public UniqueComponent() => EnsureProperties();

        public UniqueComponent(string name) : base(name) => EnsureProperties();

        private void EnsureProperties()
        {
            if (string.IsNullOrEmpty(Uid))
            {
                // Create a new UID for the component
                Uid = Guid.NewGuid().ToString();
            }
        }

        protected override void OnDeserialized(StreamingContext context)
        {
            base.OnDeserialized(context);

            EnsureProperties();
        }

        public int CompareTo(UniqueComponent other) => string.Compare(Uid, other.Uid, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => Uid?.GetHashCode() ?? base.GetHashCode();

        public virtual string Uid
        {
            get => Properties.Get<string>("UID");
            set => Properties.Set("UID", value);
        }
    }
}