using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ParkBee.Assessment.Application.Interfaces;

namespace ParkBee.Assessment.Application.Services
{
    public class CheckDoorsStatusesJob : BackgroundService
    {
        private readonly ILogger<CheckDoorsStatusesJob> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CheckDoorsStatusesJob(
            ILogger<CheckDoorsStatusesJob> logger,
            IServiceScopeFactory serviceScopeFactory
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogDebug($"CheckDoorsStatusesJob task doing background work.");

                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<IApplicationDbContext>();
                var doorStatusService = scope.ServiceProvider.GetService<IDoorStatusService>();
                var doors = await dbContext.DoorRepository.GetAllDoors();
                foreach (var door in doors)
                {
                    var isOnline = await doorStatusService.CheckDoorStatus(door);
                    await dbContext.DoorRepository.ChangeDoorStatus(door, isOnline);
                }

                await Task.Delay(5000, cancellationToken);
            }
        }
    }
}