using Confluent.Kafka;
using Mietyfy.Protobuf.Messages;

namespace Mietify.Consumer.Deserializer;

public class ListingDeserializer : IDeserializer<Listing>
{
    public Listing Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return Listing.Parser.ParseFrom(data.ToArray());
    }
}