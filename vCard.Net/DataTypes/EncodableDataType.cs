namespace vCard.Net.DataTypes
{
    /// <summary>
    /// An abstract class from which all vCard data types inherit.
    /// </summary>
    public class EncodableDataType : CardDataType, IEncodableDataType
    {
        public virtual string Encoding
        {
            get => Parameters.Get("ENCODING");
            set => Parameters.Set("ENCODING", value);
        }

        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;
    }
}