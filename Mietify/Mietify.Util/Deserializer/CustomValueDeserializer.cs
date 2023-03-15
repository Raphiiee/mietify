using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Confluent.Kafka;

namespace Mietify.Util.Deserializer
{
    public class CustomValueDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            using (MemoryStream ms = new MemoryStream(data.ToArray()))
            {
                IFormatter br = new BinaryFormatter();
                return (T)br.Deserialize(ms);
            }
        }
    }
}
