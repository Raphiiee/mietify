using Confluent.Kafka;
using Google.Protobuf;
using Mietyfy.Protobuf.Messages;

namespace Mietify.DataProducer.Serializers;

public class ListingSerializer : ISerializer<Listing>
{
    public byte[] Serialize(Listing data, SerializationContext context)
    {
        return data.ToByteArray();
    }
}