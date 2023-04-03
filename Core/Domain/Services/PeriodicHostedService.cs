using Core.Abstraction.Interfaces;
using DataAccessLayer.Abstraction.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Domain.Services;

public class PeriodicHostedService : BackgroundService
{
    private readonly TimeSpan _period= TimeSpan.FromSeconds(5);
    private readonly ILogger<PeriodicHostedService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private int _executionCount = 0;

    public PeriodicHostedService(ILogger<PeriodicHostedService> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(_period);
        while (!stoppingToken.IsCancellationRequested
            && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await using AsyncServiceScope asyncScope = _scopeFactory.CreateAsyncScope();

                var stateService = asyncScope.ServiceProvider.GetRequiredService<InnogotchiStateService>();

                await IncreaseInnogotchiState(stateService);

                _executionCount++;

                _logger.LogInformation($"count: {_executionCount}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to execute PeriodicHostedService with message: {ex.Message}");
            }
        }
    }
    
    private async Task IncreaseInnogotchiState(InnogotchiStateService stateService)
    {
        await stateService.IncreaseAgeAsync();
        await stateService.IncreaseHungerLevelAsync();
        await stateService.IncreaseWaterLevelAsync();
        await stateService.IncreaseHappinessDaysAsync();
    }
}
