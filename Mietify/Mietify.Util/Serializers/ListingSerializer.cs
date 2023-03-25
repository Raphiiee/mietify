using Confluent.Kafka;
using Google.Protobuf;
using Mietify.Protobuf.Messages;
using Mietyfy.Protobuf.Messages;

namespace Mietify.Util.Serializers;

public class ListingSerializer : ISerializer<Listing>
{
    public byte[] Serialize(Listing data, SerializationContext context)
    {
        return data.ToByteArray();
    }
}