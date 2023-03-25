namespace Mietify.Backend.Handler;

public class DistrictHandler : BackgroundService
{
    private readonly MietifyDbContext _dbContext;

    public DistrictHandler(MietifyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}