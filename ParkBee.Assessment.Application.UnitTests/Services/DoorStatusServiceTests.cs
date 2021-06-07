using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Services;
using ParkBee.Assessment.Domain.Entities;
using Xunit;

namespace ParkBee.Assessment.Application.UnitTests.Services
{
    public class DoorStatusServiceTests
    {
        private const string OfflineIpAddress = "192.168.255.255";
        private const string OnlineIpAddress = "8.8.8.8";
        private readonly DoorStatusService _doorStatusService;
        private readonly Mock<IConfiguration>  _configurationMock = new();
        readonly Mock<IPingService> _pingServiceMock= new();
        public DoorStatusServiceTests()
        {
            _configurationMock.SetupGet(x => x[It.Is<string>(s=>s == "PingRetry:Count")]).Returns("2");

            _pingServiceMock.Setup(x => x.Send(IPAddress.Parse(OfflineIpAddress),2,TimeSpan.FromSeconds(2))).ReturnsAsync(false);
            _pingServiceMock.Setup(x => x.Send(IPAddress.Parse(OnlineIpAddress),2,TimeSpan.FromSeconds(2))).ReturnsAsync(true);
            _doorStatusService = new DoorStatusService(_pingServiceMock.Object, _configurationMock.Object);
        }

        [Fact]
        public void ShouldCheckRequiredInjects()
        {
            Assert.Throws<ArgumentNullException>(() => new DoorStatusService(null,_configurationMock.Object));
            Assert.Throws<ArgumentNullException>(() => new DoorStatusService(_pingServiceMock.Object,null));
        }

        [Fact]
        public async Task ShouldThrowInvalidIpArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _doorStatusService.CheckDoorStatus(new Door { IP = "" }));
            Assert.Equal("IP address of door is not valid", ex.Message);
        }

        [Fact]
        public async Task ShouldCheckResultResponsePingService()
        {
            var result = await _doorStatusService.CheckDoorStatus(new Door { IP = OfflineIpAddress });

            Assert.False(result);

            result = await _doorStatusService.CheckDoorStatus(new Door { IP = OnlineIpAddress });

            Assert.True(result);
        }
    }
}
