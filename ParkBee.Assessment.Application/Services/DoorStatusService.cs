using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Application.Services
{
    public class DoorStatusService : IDoorStatusService
    {
        private readonly IPingService _pingService;
        private readonly TimeSpan _interval;
        private readonly int _retryCount;

        public DoorStatusService(IPingService pingService, IConfiguration configuration)
        {
            _pingService = pingService ?? throw new ArgumentNullException(nameof(pingService));
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if(!TimeSpan.TryParse(configuration["PingRetry:Interval"], out _interval))
                _interval = TimeSpan.FromSeconds(2);
            if(!Int32.TryParse(configuration["PingRetry:Count"], out _retryCount))
                _retryCount = 2;
        }

        public async Task<bool> CheckDoorStatus(Door door)
        {

            if (!IPAddress.TryParse(door.IP, out var ipAddress))
                throw new ArgumentException("IP address of door is not valid");

            return await _pingService.Send(ipAddress,_retryCount,_interval);
        }

    }
}