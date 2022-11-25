using System.Collections.Specialized;
using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// This class is used to represent the Categories (CATEGORIES) property of a vCard object.
    /// </summary>
    /// <remarks>
    /// This property class parses the <see cref="CategoriesString"/>
    /// property and allows access to the component parts. It is used to specify application
    /// category information about the object. This property is only valid for use with
    /// the vCard 3.0 specification objects.
    /// </remarks>
    public class Categories : EncodableDataType
    {
        private readonly StringCollection _categories;

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>
        /// Supports vCard 3.0 and 4.0.
        /// </value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard30 | SpecificationVersions.vCard40;

        /// <summary>
        /// This property is used to get the categories string collection
        /// </summary>
        /// <value>
        /// Categories can be added to or removed from the returned collection reference
        /// </value>
        public StringCollection Collection => _categories;

        /// <summary>
        /// This property is used to set or get the categories as a string value
        /// </summary>
        /// <value>
        /// The string can contain one or more categories separated by commas or semi-colons.
        /// The string will be split and loaded into the categories string collection.
        /// </value>
        public string CategoriesString
        {
            get => string.Join(", ", _categories);
            set
            {
                _categories.Clear();
                if (value == null)
                {
                    return;
                }

                string[] array = value.Split(',', ';');
                string[] array2 = array;
                foreach (string text in array2)
                {
                    string text2 = text.Trim();
                    if (text2.Length > 0)
                    {
                        _categories.Add(text2);
                    }
                }
            }
        }

        public Categories()
        {
            Version = SpecificationVersions.vCard30;
            _categories = new StringCollection();
        }

        public Categories(string value)
        {
            _categories = new StringCollection();

            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var serializer = new CategoriesSerializer();
            CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
        }
    }
}