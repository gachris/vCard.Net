using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// This class is used to represent the Organization Name and Unit (ORG) property of a vCard
    /// </summary>
    /// <remarks>
    /// This property class parses the <see cref="Name"/> property
    /// and allows access to the component organization name and unit parts. This property
    /// is based on the X.520 Organization Name attribute and the X.520 Organization
    /// Unit attribute.
    /// </remarks>
    public class Organization : EncodableDataType
    {
        private readonly StringCollection _units;

        /// <summary>
        /// This property is used to set or get the organization name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This property is used to set or get the string to use when sorting organizations
        /// </summary>
        /// <value>
        /// If set, this value should be given precedence over the <see cref="Name"/> property
        /// </value>
        public string SortAs 
        {
            get => Parameters.Get("SORT-AS");
            set
            {
                if (value is null)
                {
                    Parameters.Remove("SORT-AS");
                }
                else Parameters.Set("SORT-AS", value);
            }
        }

        /// <summary>
        /// This property is used to get the Organization Units string collection
        /// </summary>
        /// <value>
        /// Units can be added to or removed from the returned collection reference
        /// </value>
        public StringCollection Units => _units;

        /// <summary>
        /// This property is used to set or get the Organization Units as a string value
        /// </summary>
        /// <remarks>
        /// The string can contain one or more unit names separated by commas or semi-colons.
        /// The string will be split and loaded into the units string collection.
        /// </remarks>
        public string UnitsString
        {
            get => string.Join(", ", _units.OfType<string>());
            set
            {
                _units.Clear();
                if (value == null)
                {
                    return;
                }

                string[] array = value.Split(new char[2] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                string[] array2 = array;
                foreach (string text in array2)
                {
                    string text2 = text.Trim();
                    if (text2.Length > 0)
                    {
                        _units.Add(text2);
                    }
                }
            }
        }

        public Organization() => _units = new StringCollection();

        public Organization(string value)
        {
            _units = new StringCollection();

            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var serializer = new OrganizationSerializer();
            CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
        }
    }
}