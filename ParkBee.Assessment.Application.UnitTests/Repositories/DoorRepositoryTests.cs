using System;
using System.Linq;
using System.Threading.Tasks;
using ParkBee.Assessment.Application.Interfaces.Repositories;
using ParkBee.Assessment.Application.UnitTests.Common;
using ParkBee.Assessment.Persistence;
using ParkBee.Assessment.Persistence.Repositories;
using Xunit;

namespace ParkBee.Assessment.Application.UnitTests.Repositories
{
    public class DoorRepositoryTests
    {
        private const int GarageId = 1;

        private readonly ApplicationDbContext _dbContext;
        private readonly IDoorRepository _repo;


        public DoorRepositoryTests()
        {

            _dbContext = ApplicationDbContextFactory.Create();
            _repo = new DoorRepository(_dbContext);
        }

        [Fact]
        public void ShouldCheckForRequiredInjects()
        {
            Assert.Throws<ArgumentNullException>(() => new DoorRepository(null));
        }

        [Fact]
        public async Task ShouldReturnValueGetAllDoors()
        {
            var result = await _repo.GetAllDoors();
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task ShouldReturnLatestStatusOfADoor()
        {
            var result = await _repo.GetDoorWithLatestStatus(1, GarageId);
            Assert.NotNull(result);
            Assert.True(result.DoorStatusHistories.First().IsOnline);
        }
    }
}
