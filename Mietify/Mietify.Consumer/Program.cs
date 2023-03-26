using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Mietify.Consumer;
using Mietify.Util.Serializers;
using Mietyfy.Protobuf.Messages;
using System.Collections;
using Mietify.Protobuf.Messages;

var envvars = Environment.GetEnvironmentVariables().Cast<DictionaryEntry>();

//Console.WriteLine("ENV VARIABLE:" + envvars.Select(x => x.Key.ToString() + ":" + x.Value.ToString())
//    .Aggregate((x, y) => x + "; " + y));

var kafkaconstring = Environment.GetEnvironmentVariable("KAFKA_CONNECTION");

if (string.IsNullOrEmpty(kafkaconstring))
{
    kafkaconstring = "localhost:29092,localhost:39092,localhost:49092";
    Console.WriteLine($"KAFKA_CONNECTION not specified in Environment - using fallback (non docker env): '{kafkaconstring}'");
}


const string Totopic = "AveragePrice";

Dictionary<string, List<Listing>> listings = new Dictionary<string, List<Listing>>();

async Task CreateTopicAverageListing(ConsumerConfig consumerConfig)
{
    using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = consumerConfig.BootstrapServers }).Build())
    {
        try
        {
            await adminClient.CreateTopicsAsync(new TopicSpecification[]
            {
                new TopicSpecification { Name = Totopic, ReplicationFactor = 3, NumPartitions = 3 }
            });
        }
        catch (CreateTopicsException e)
        {
            Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
        }
    }
}

async Task SendUpdatedAverage(ProducerConfig producerConfig, IList<Listing> listOfListings)
{
    var postalCode = listOfListings.GroupBy(l => l.Address.PostalCode).ToList();
    if (postalCode.Count > 1)
        throw new Exception("Listings have different postal codes");

    using (var producer = new ProducerBuilder<string, AveragePrice>(producerConfig.AsEnumerable()).SetValueSerializer(new AveragePriceSerializer()).Build())
    {
        var newAverage = new AveragePrice();
        newAverage.ID = Guid.NewGuid().ToString();
        newAverage.AveragePrice_ = listOfListings.Average(l => l.Price);
        newAverage.PostalCode = postalCode[0].Key;
        producer.Produce(Totopic, new Message<string, AveragePrice>()
            {
                Key = newAverage.ID, Value = newAverage
            },
            deliveryReport =>
            {
                if (deliveryReport.Error != ErrorCode.NoError)
                {
                    Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                }
                else
                {
                    Console.WriteLine($"Produced event to topic {Totopic}: key = {newAverage.ID} value = {newAverage.AveragePrice_}");
                }
            }
        );
        producer.Flush(TimeSpan.FromSeconds(10));
    }
}

var producerConfig = new ProducerConfig
{
    BootstrapServers = kafkaconstring,
    SecurityProtocol = SecurityProtocol.Plaintext
};


var consumerConfig = new ConsumerConfig()
{
    BootstrapServers = kafkaconstring,
    SecurityProtocol = SecurityProtocol.Plaintext,
    GroupId = "Id"
};
await CreateTopicAverageListing(consumerConfig);

var consumer = new ConsumerClass<Listing>(consumerConfig);
while (true)
{
    var ayay = consumer.ConsumeAsync();

    await foreach (var listing in ayay)
    {
        if (listings.ContainsKey(listing.Address.PostalCode))
        {
            listings[listing.Address.PostalCode].Add(listing);
        }
        else
        {
            listings.Add(listing.Address.PostalCode, new List<Listing>() { listing });
        }
    
        await SendUpdatedAverage(producerConfig, listings[listing.Address.PostalCode]);
    }
    Thread.Sleep(5000);
}