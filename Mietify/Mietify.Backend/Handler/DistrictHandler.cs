using AutoMapper;
using Confluent.Kafka;
using Mietify.Backend.Handler.Deserializer;
using Mietify.Backend.Models.DbModels;
using Mietyfy.Protobuf.Messages;

namespace Mietify.Backend.Handler;

public class DistrictHandler : BackgroundService
{
    private readonly MietifyDbContext _dbContext;
    private IMapper _mapper;

    public DistrictHandler(MietifyDbContext dbContext, IMapper mapper)
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

        using (IConsumer<Ignore, AveragePrice> c = new ConsumerBuilder<Ignore, AveragePrice>(consumerConfig)
                   .SetValueDeserializer(new AverageDeserializer()).Build())
        {
            c.Subscribe("AveragePrice");
            
            while (!stoppingToken.IsCancellationRequested)
            {
                var cr = c.Consume(stoppingToken);

                var dbDistrict =_dbContext.Districts.SingleOrDefault(s => s.PostalCode == cr.Message.Value.PostalCode);
                if (dbDistrict == null)
                {
                    var newDistrict = new DbDistrict();
                    newDistrict.PostalCode = cr.Message.Value.PostalCode;
                    newDistrict.AveragePrice = cr.Message.Value.AveragePrice_;
                    _dbContext.Districts.Add(newDistrict);
                }
                else
                {
                    dbDistrict.AveragePrice = cr.Message.Value.AveragePrice_;
                }
            }

            c.Close();

            return Task.CompletedTask;
        }
    }
}