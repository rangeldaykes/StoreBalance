using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StoreBalance.WebApi.Domain.Services;

namespace StoreBalance.WebApi.Application.BackgroundJobs
{
    public class ApplyCreditHostedService : BackgroundService
    {
        private readonly ILogger<ApplyCreditHostedService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;        

        public ApplyCreditHostedService(
            ILogger<ApplyCreditHostedService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(20000, stoppingToken);

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    _logger.LogInformation("Running Apply Future Credits");

                    var applyCreditService = scope.ServiceProvider.GetRequiredService<IApplyFutureCreditService>();
                    await applyCreditService.Apply();

                    _logger.LogInformation("Apply Future Credits Finished");
                }                
            }
        }
    }
}
