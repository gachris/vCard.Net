using System;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// This enumerated type defines the various vCard kinds for the <see cref="Kind"/> class
    /// </summary>
    [Serializable]
    public enum CardKind
    {
        /// <summary>
        /// No type defined
        /// </summary>
        None,
        /// <summary>
        /// An individual
        /// </summary>
        Individual,
        /// <summary>
        /// A group
        /// </summary>
        Group,
        /// <summary>
        /// An organization
        /// </summary>
        Organization,
        /// <summary>
        /// A location
        /// </summary>
        Location,
        /// <summary>
        /// Indicates some other type of vCard
        /// </summary>
        Other
    }
}