using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ParkBee.Assessment.Application.Garages.Queries.GetGarageDetails;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.UnitTests.Common;
using ParkBee.Assessment.Persistence;
using Xunit;
using Moq;

namespace ParkBee.Assessment.Application.UnitTests.Garages
{
    public class GetGarageDetailsQueryTests : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<ILoggedInUserContext> _loggedInUserContextMock;

        public GetGarageDetailsQueryTests()
        {
            _loggedInUserContextMock = new Mock<ILoggedInUserContext>();
            _loggedInUserContextMock.Setup(m => m.GarageId).Returns(2);
            _dbContext = ApplicationDbContextFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(_dbContext);
        }

        [Fact]
        public async Task ShouldReturnGarageDetails()
        {
            var sut = new GetGarageDetailsQueryHandler(_dbContext, _loggedInUserContextMock.Object);

            var result = await sut.Handle(new GetGarageDetailsQuery(), CancellationToken.None);
            Assert.Equal(2, result.GarageId);
            Assert.Equal(2, result.Doors.Count());
            Assert.IsType<GarageDto>(result);

        }
    }
}
