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

        /// <summary>
        /// A value that uniquely identifies the vCard.
        /// </summary>
        /// <remarks>
        ///     This value is optional.  The string must be any string
        ///     that can be used to uniquely identify the Person.  The
        ///     usage of the field is determined by the software.  Typical
        ///     possibilities for a unique string include a URL, a GUID,
        ///     or an LDAP directory path.  However, there is no particular
        ///     standard dictated by the Person specification.
        /// </remarks>
        public virtual string Uid
        {
            get => Properties.Get<string>("UID");
            set => Properties.Set("UID", value);
        }
    }
}