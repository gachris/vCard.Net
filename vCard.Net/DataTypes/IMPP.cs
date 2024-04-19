using System.Collections.Generic;
using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents an Instant Messaging and Presence Protocol (IMPP) property in a vCard.
/// </summary>
public class IMPP : EncodableDataType
{
    /// <summary>
    /// Gets or sets the value of the IMPP property.
    /// </summary>
    public virtual string Value { get; set; }

    /// <summary>
    /// Gets or sets the list of social profile types associated with this IMPP property.
    /// </summary>
    public virtual IList<string> Types
    {
        get => Parameters.GetMany("TYPE");
        set => Parameters.Set("TYPE", value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IMPP"/> class.
    /// </summary>
    public IMPP()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IMPP"/> class with the specified value.
    /// </summary>
    /// <param name="value">The IMPP property value.</param>
    public IMPP(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new IMPPSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }
}