using Confluent.Kafka;
using Google.Protobuf;
using Mietyfy.Protobuf.Messages;

namespace Mietify.Util.Serializers;

public class AveragePriceSerializer : ISerializer<AveragePrice>
{
    public byte[] Serialize(AveragePrice data, SerializationContext context)
    {
        return data.ToByteArray();
    }
}