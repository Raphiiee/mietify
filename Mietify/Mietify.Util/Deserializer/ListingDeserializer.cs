using Confluent.Kafka;
using Mietify.Protobuf.Messages;
using Mietyfy.Protobuf.Messages;

namespace Mietify.Util.Deserializer;

public class ListingDeserializer : IDeserializer<Listing>
{
    public Listing Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return Listing.Parser.ParseFrom(data.ToArray());
    }
}