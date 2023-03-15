// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Mietify.DataProducer.Serializers;
using Mietyfy.Protobuf.Messages;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:29092",
    SecurityProtocol = SecurityProtocol.Plaintext
};



const string topic = "Listings";

using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = config.BootstrapServers }).Build())
{
    try
    {
        await adminClient.CreateTopicsAsync(new TopicSpecification[] { 
            new TopicSpecification { Name = topic, ReplicationFactor = 3, NumPartitions = 3 } });
    }
    catch (CreateTopicsException e)
    {
        Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
    }
}

var listOfListings = new List<Listing>();

for (int i = 0; i < 5; i++)
    listOfListings.Add(new Listing
    {
        Name = $"Schöne Wohnung im {i}",
        Id = Guid.NewGuid().ToString(),
        Area = 100 * i,
        Price = 1000 * i,
        Address = new Address
        {
            Country = "AT",
            City = "Wien",
            Street = "Schönbrunner Straße",
            PostalCode = "100i"
        }
    });

using (var producer = new ProducerBuilder<string, Listing>(config.AsEnumerable()).SetValueSerializer(new ListingSerializer()).Build())
{
    var numProduced = 0;
    foreach (var listing in listOfListings)
        producer.Produce(topic, new Message<string, Listing> { Key = listing.Id, Value = listing },
            deliveryReport =>
            {
                if (deliveryReport.Error.Code != ErrorCode.NoError)
                {
                    Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                }
                else
                {
                    Console.WriteLine($"Produced event to topic {topic}: key = {listing.Id} value = {listing.Address.PostalCode}");
                    numProduced += 1;
                }
            });

    producer.Flush(TimeSpan.FromSeconds(10));
    Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
}