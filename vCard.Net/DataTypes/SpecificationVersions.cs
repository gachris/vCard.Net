using System;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// This enumerated type defines the various specification versions that can be supported
    /// by the objects
    /// </summary>
    [Serializable]
    [Flags]
    public enum SpecificationVersions
    {
        /// <summary>
        /// The object has no version-specific behavior
        /// </summary>
        None = 0x0,
        /// <summary>
        /// A vCard 2.1 object
        /// </summary>
        vCard21 = 0x1,
        /// <summary>
        /// A vCard 3.0 object
        /// </summary>
        vCard30 = 0x2,
        /// <summary>
        /// A vCard 4.0 object
        /// </summary>
        vCard40 = 0x4,
        /// <summary>
        /// The object is used by all vCard specifications (2.1, 3.0, and 4.0)
        /// </summary>
        vCardAll = 0x8
    }
}