using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using ParkBee.Assessment.Persistence;
using ParkBee.Assessment.Domain.Entities;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Garages.Commands;
using ParkBee.Assessment.Application.UnitTests.Common;
using ParkBee.Assessment.Application.Exceptions;

namespace ParkBee.Assessment.Application.UnitTests.Garages
{
    public class RefreshDoorStatusCommandTests : IDisposable
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<IDoorStatusService> _doorStatusServiceMock;
        private readonly Mock<ILoggedInUserContext> _loggedInUserContextMock;

        public RefreshDoorStatusCommandTests()
        {
            _loggedInUserContextMock = new Mock<ILoggedInUserContext>();
            _loggedInUserContextMock.Setup(m => m.GarageId).Returns(2);
            _dbContext = ApplicationDbContextFactory.Create();

            _doorStatusServiceMock = new Mock<IDoorStatusService>();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(_dbContext);
        }
         
        [Fact]
        public void ShouldCheckRequiredInjects()
        {
            Assert.Throws<ArgumentNullException>(() => new RefreshDoorStatusCommandHandler(null, _doorStatusServiceMock.Object, _loggedInUserContextMock.Object));
            Assert.Throws<ArgumentNullException>(() => new RefreshDoorStatusCommandHandler(_dbContext, null, _loggedInUserContextMock.Object));
            Assert.Throws<ArgumentNullException>(() => new RefreshDoorStatusCommandHandler(_dbContext, _doorStatusServiceMock.Object, null));
        }

        [Fact]
        public async Task ShouldReturnDoorStatus()
        {
            _doorStatusServiceMock.Setup(cs => cs.CheckDoorStatus(It.Is<Door>(d => d.Id == 2))).ReturnsAsync(false);
            _doorStatusServiceMock.Setup(cs => cs.CheckDoorStatus(It.Is<Door>(d => d.Id == 3))).ReturnsAsync(true);
            var sut = new RefreshDoorStatusCommandHandler(_dbContext, _doorStatusServiceMock.Object, _loggedInUserContextMock.Object);

            await Assert.ThrowsAsync<NotFoundException>(()=>sut.Handle(new RefreshDoorStatusCommand { DoorId = 1 }, CancellationToken.None));

            Assert.False(await sut.Handle(new RefreshDoorStatusCommand { DoorId = 2 }, CancellationToken.None));
            Assert.True(await sut.Handle(new RefreshDoorStatusCommand { DoorId = 3 }, CancellationToken.None));
        }
    }
}
