using AutoMapper;
using Confluent.Kafka;
using Mietify.Backend.Models.WebDto;
using Mietify.Util.Deserializer;

namespace Mietify.Backend.Handler;

public class ListingHandler : BackgroundService
{
    private readonly MietifyDbContext _dbContext;
    private IMapper _mapper;

    public ListingHandler(MietifyDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumerConfig = new ConsumerConfig()
        {
            //todo do Confguration
            BootstrapServers = "localhost:29092",
            SecurityProtocol = SecurityProtocol.Plaintext,
            GroupId = "Id"
        };

        using (IConsumer<Ignore, Mietify.Protobuf.Messages.Listing> c = new ConsumerBuilder<Ignore, Mietify.Protobuf.Messages.Listing>(consumerConfig)
                   .SetValueDeserializer(new ListingDeserializer()).Build())
        {
            c.Subscribe("Listings");

            Listing ret = null;


            while (!stoppingToken.IsCancellationRequested)
            {
                var cr = c.Consume(stoppingToken);

                Console.WriteLine($"Consumed message '{cr.Message.Value.Address.PostalCode}' at: '{cr.TopicPartitionOffset}'.");

                _dbContext.Listings.Add(_mapper.Map<Models.DbModels.DbListing>(cr.Message.Value));
            }

            c.Close();

            return Task.CompletedTask;
        }
    }
}