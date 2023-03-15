using Confluent.Kafka;
using Mietify.Consumer.Deserializer;
using Mietyfy.Protobuf.Messages;

{
    var config = new ConsumerConfig()
    {
        BootstrapServers = "localhost:29092",
        SecurityProtocol = SecurityProtocol.Plaintext,
        GroupId = "Id"
    };

    using (var c = new ConsumerBuilder<Ignore, Listing>(config).SetValueDeserializer(new ListingDeserializer()).Build())
    {
        c.Subscribe("Listings");

        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true; // prevent the process from terminating.
            cts.Cancel();
        };
        try
        {
            while (true)
            {
                try
                {
                    var cr = c.Consume(cts.Token);

                    Console.WriteLine($"Consumed message '{cr.Value.Address.PostalCode}' at: '{cr.TopicPartitionOffset}'.");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occurred: {e.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Ensure the consumer leaves the group cleanly and final offsets are committed.
            c.Close();
        }
    }
}