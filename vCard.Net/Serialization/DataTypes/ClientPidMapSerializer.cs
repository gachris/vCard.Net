using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes
{
    public class ClientPidMapSerializer : StringSerializer
    {
        public ClientPidMapSerializer() : base()
        {
        }

        public ClientPidMapSerializer(SerializationContext ctx) : base(ctx)
        {
        }

        public override Type TargetType => typeof(ClientPidMap);

        public override string SerializeToString(object obj)
        {
            if (!(obj is ClientPidMap clientPidMap))
            {
                return null;
            }

            if (clientPidMap.Id == 0 || string.IsNullOrWhiteSpace(clientPidMap.Uri))
            {
                return null;
            }

            return Encode(clientPidMap, string.Join(";", clientPidMap.Id.ToString(), clientPidMap.Uri));
        }

        public ClientPidMap Deserialize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (!(CreateAndAssociate() is ClientPidMap clientPidMap))
            {
                return null;
            }

            // Decode the value, if necessary!
            value = Decode(clientPidMap, value);

            if (value == null || value.Length <= 0)
            {
                return null;
            }

            clientPidMap.Id = 0;
            clientPidMap.Uri = null;
            if (!string.IsNullOrWhiteSpace(value))
            {
                string[] array = value.Split(new char[1] { ';' });
                if (array[0].Length != 0 && int.TryParse(array[0], out var result) && result != 0)
                {
                    clientPidMap.Id = result;
                }

                if (array.Length > 1)
                {
                    clientPidMap.Uri = array[1];
                }
            }

            return clientPidMap;
        }

        public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
    }
}