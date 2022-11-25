using System;
using vCard.Net.DataTypes;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.Serialization
{
    public class DataTypeSerializerFactory : ISerializerFactory
    {
        /// <summary>
        /// Returns a serializer that can be used to serialize and object
        /// of type <paramref name="objectType"/>.
        /// <note>
        ///     TODO: Add support for caching.
        /// </note>
        /// </summary>
        /// <param name="objectType">The type of object to be serialized.</param>
        /// <param name="ctx">The serialization context.</param>
        public virtual ISerializer Build(Type objectType, SerializationContext ctx)
        {
            if (objectType != null)
            {
                ISerializer s;

                if (typeof(IDateTime).IsAssignableFrom(objectType))
                {
                    s = new DateTimeSerializer(ctx);
                }
                //else if (typeof(Address).IsAssignableFrom(objectType))
                //{
                //    s = new AddressSerializer(ctx);
                //}
                else if (typeof(Categories).IsAssignableFrom(objectType))
                {
                    s = new CategoriesSerializer(ctx);
                }
                else if (typeof(ClientPidMap).IsAssignableFrom(objectType))
                {
                    s = new ClientPidMapSerializer(ctx);
                }
                //else if (typeof(Email).IsAssignableFrom(objectType))
                //{
                //    s = new EmailSerializer(ctx);
                //}
                else if (typeof(Gender).IsAssignableFrom(objectType))
                {
                    s = new GenderSerializer(ctx);
                }
                else if (typeof(GeographicPosition).IsAssignableFrom(objectType))
                {
                    s = new GeographicPositionSerializer(ctx);
                }
                else if (typeof(GeographicPosition).IsAssignableFrom(objectType))
                {
                    s = new GeographicPositionSerializer(ctx);
                }
                //else if (typeof(IMPP).IsAssignableFrom(objectType))
                //{
                //    s = new IMPPSerializer(ctx);
                //}
                else if (typeof(Key).IsAssignableFrom(objectType))
                {
                    s = new KeySerializer(ctx);
                }
                else if (typeof(Kind).IsAssignableFrom(objectType))
                {
                    s = new KindSerializer(ctx);
                }
                //else if (typeof(Language).IsAssignableFrom(objectType))
                //{
                //    s = new LanguageSerializer(ctx);
                //}
                //else if (typeof(Logo).IsAssignableFrom(objectType))
                //{
                //    s = new PhotoSerializer(ctx);
                //}
                else if (typeof(Name).IsAssignableFrom(objectType))
                {
                    s = new NameSerializer(ctx);
                }
                else if (typeof(Organization).IsAssignableFrom(objectType))
                {
                    s = new OrganizationSerializer(ctx);
                }
                //else if (typeof(Photo).IsAssignableFrom(objectType))
                //{
                //    s = new PhotoSerializer(ctx);
                //}
                //else if (typeof(Related).IsAssignableFrom(objectType))
                //{
                //    s = new RelatedSerializer(ctx);
                //}
                //else if (typeof(Sound).IsAssignableFrom(objectType))
                //{
                //    s = new SoundSerializer(ctx);
                //}
                //else if (typeof(Sound).IsAssignableFrom(objectType))
                //{
                //    s = new SoundSerializer(ctx);
                //}
                else if (typeof(Source).IsAssignableFrom(objectType))
                {
                    s = new SourceSerializer(ctx);
                }
                //else if (typeof(Telephone).IsAssignableFrom(objectType))
                //{
                //    s = new TelephoneSerializer(ctx);
                //}
                //else if (typeof(Net.DataTypes.TimeZone).IsAssignableFrom(objectType))
                //{
                //    s = new TelephoneSerializer(ctx);
                //}
                // Default to a string serializer, which simply calls
                // ToString() on the value to serialize it.
                else
                {
                    s = new StringSerializer(ctx);
                }

                return s;
            }
            return null;
        }
    }
}