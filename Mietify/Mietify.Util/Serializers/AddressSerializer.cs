using Confluent.Kafka;
using Google.Protobuf;
using Mietify.Protobuf.Messages;
using Mietyfy.Protobuf.Messages;

namespace Mietify.Util.Serializers;

public class AddressSerializer : ISerializer<Address>
{
    public byte[] Serialize(Address data, SerializationContext context)
    {
        return data.ToByteArray();
    }
}