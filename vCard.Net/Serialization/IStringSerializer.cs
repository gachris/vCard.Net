using System.IO;

namespace vCard.Net.Serialization
{
    public interface IStringSerializer : ISerializer
    {
        string SerializeToString(object obj);
        object Deserialize(TextReader tr);
    }
}