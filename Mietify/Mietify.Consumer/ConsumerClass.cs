using Confluent.Kafka;
using Google.Protobuf;
using Mietify.Consumer.Deserializer;
using Mietyfy.Protobuf.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mietify.Consumer
{
    public class ConsumerClass<T>
    {
        ConsumerConfig config;
        public ConsumerClass()
        {
            this.config = new ConsumerConfig()
            {
                BootstrapServers = "localhost:29092",
                SecurityProtocol = SecurityProtocol.Plaintext,
                GroupId = "Id"
            };
        }

        public async IAsyncEnumerable<Listing> ConsumeAsync()
        {
            using (IConsumer<Ignore, Listing> c = new ConsumerBuilder<Ignore, Listing>(config).SetValueDeserializer(new ListingDeserializer()).Build())
            {

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                c.Subscribe("Listings");

                Listing ret = null;


                while (!cts.IsCancellationRequested)
                {

                    var cr = c.Consume(cts.Token);

                    Console.WriteLine($"Consumed message '{cr.Message.Value.Address.PostalCode}' at: '{cr.TopicPartitionOffset}'.");

                    yield return cr.Message.Value;
                }

                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                c.Close();
            }
        }
    }

}


