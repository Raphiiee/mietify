using Confluent.Kafka;
using Mietify.Consumer;
using Mietify.Consumer.Deserializer;
using Mietyfy.Protobuf.Messages;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


var consumer = new ConsumerClass<Listing>();

var ayay =  consumer.ConsumeAsync();

//basically our stream?
await foreach(var x in ayay)
{
    Console.WriteLine($"AAAAAAA {x.Id}");
}

