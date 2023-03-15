using Confluent.Kafka;
using Google.Protobuf;
using Mietyfy.Protobuf.Messages;

namespace Mietify.DataProducer.Serializers;

public class AddressSerializer : ISerializer<Address>
{
    public byte[] Serialize(Address data, SerializationContext context)
    {
        return data.ToByteArray();
    }
}