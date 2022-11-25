using vCard.Net.Collections;

namespace vCard.Net
{
    public interface ICardObject : IGroupedObject<string>, ILoadable, ICopyable, IServiceProvider
    {
        /// <summary>
        /// The name of the vCard object.
        /// Every vCard object can be assigned
        /// a name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Returns the parent of this object.
        /// </summary>
        ICardObject Parent { get; set; }

        /// <summary>
        /// Returns a collection of children of this object.
        /// </summary>
        ICardObjectList<ICardObject> Children { get; }

        /// <summary>
        /// Returns the line number where this vCard
        /// object was found during parsing.
        /// </summary>
        int Line { get; set; }

        /// <summary>
        /// Returns the column number where this vCard
        /// object was found during parsing.
        /// </summary>
        int Column { get; set; }
    }
}