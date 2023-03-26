using Confluent.Kafka;
using Mietyfy.Protobuf.Messages;

namespace Mietify.Backend.Handler.Deserializer;

public class AverageDeserializer : IDeserializer<AveragePrice>
{
    public AveragePrice Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return AveragePrice.Parser.ParseFrom(data.ToArray());
    }
}