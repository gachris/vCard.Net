using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// This class is used to represent the Name (N) property of a vCard
    /// </summary>
    /// <remarks>
    /// This property class parses the <see cref="SortableName"/> property
    /// and allows access to the component name parts. It is based on the semantics of
    /// the X.520 individual name attributes.
    /// </remarks>
    public class Name : EncodableDataType
    {
        /// <summary>
        /// This property is used to set or get the family name (last name)
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// This property is used to set or get the given name (first name)
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// This property is used to set or get additional names such as one or more middle
        /// </summary>
        public string AdditionalNames { get; set; }

        /// <summary>
        /// This property is used to set or get the name prefix (i.e. Mr., Mrs.)
        /// </summary>
        public string NamePrefix { get; set; }

        /// <summary>
        /// This property is used to set or get the name suffix (i.e. Jr, Sr)
        /// </summary>
        public string NameSuffix { get; set; }

        /// <summary>
        /// This property is used to set or get the string to use when sorting names
        /// </summary>
        /// <value>
        /// If set, this value should be given precedence over the <see cref="SortableName"/> property
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
        /// This read-only property can be used to get the name in a format suitable for sorting by last name
        /// </summary>
        /// <remarks>
        /// The name is returned in a comma-separated format in the order family name, given name, additional names, name suffix, name prefix.
        /// </remarks>
        public string SortableName
        {
            get
            {
                string[] array = new string[5];
                int num = 0;
                if (!string.IsNullOrWhiteSpace(FamilyName))
                {
                    array[num++] = FamilyName;
                }

                if (!string.IsNullOrWhiteSpace(GivenName))
                {
                    array[num++] = GivenName;
                }

                if (!string.IsNullOrWhiteSpace(AdditionalNames))
                {
                    array[num++] = AdditionalNames;
                }

                if (!string.IsNullOrWhiteSpace(NameSuffix))
                {
                    array[num++] = NameSuffix;
                }
                
                if (!string.IsNullOrWhiteSpace(NamePrefix))
                {
                    array[num++] = NamePrefix;
                }

                return num == 0 ? "Unknown" : string.Join(", ", array, 0, num);
            }
        }

        /// <summary>
        /// This read-only property can be used to get the full, formatted name
        /// </summary>
        public string FormattedName
        {
            get
            {
                string[] array = new string[5];
                int num = 0;
                if (!string.IsNullOrWhiteSpace(NamePrefix))
                {
                    array[num++] = NamePrefix;
                }

                if (!string.IsNullOrWhiteSpace(GivenName))
                {
                    array[num++] = GivenName;
                }

                if (!string.IsNullOrWhiteSpace(AdditionalNames))
                {
                    array[num++] = AdditionalNames;
                }

                if (!string.IsNullOrWhiteSpace(FamilyName))
                {
                    array[num++] = FamilyName;
                }

                if (!string.IsNullOrWhiteSpace(NameSuffix))
                {
                    array[num++] = NameSuffix;
                }

                return num == 0 ? "Unknown" : string.Join(" ", array, 0, num);
            }
        }

        public Name()
        {
        }

        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var serializer = new NameSerializer();
            CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
        }
    }
}