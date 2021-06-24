using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ParkBee.Assessment.Application.Garages.Queries.GetGarageDetails;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Persistence;
using Xunit;
using Moq;
using AutoMapper;
using ParkBee.Assessment.Application.Mappings;
using ParkBee.Assessment.Application.Interfaces.Repositories;
using System.Collections.Generic;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Application.UnitTests.Garages
{
    public class GetGarageDetailsQueryTests
    {
        private readonly Mock<IGarageRepository> _garageRepository;
        private readonly Mock<ILoggedInUserContext> _loggedInUserContextMock;
        private readonly IMapper _mapper;

        public GetGarageDetailsQueryTests()
        {
            _loggedInUserContextMock = new Mock<ILoggedInUserContext>();
            _loggedInUserContextMock.Setup(m => m.GarageId).Returns(2);
            _garageRepository = new Mock<IGarageRepository>();

            var ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = ConfigurationProvider.CreateMapper();
        }

        [Fact]
        public async Task ShouldReturnGarageDetails()
        {
            _garageRepository.Setup(gr => gr.GetGarageDetail(2)).ReturnsAsync(new Garage
            {
                Id = 2,
                Doors = new List<Door>{
                    new Door{
                        Id = 1,
                        Name = "First Door",
                        GarageId = 2,
                        IP = "127.0.0.1",
                        DoorStatusHistories = new List<DoorStatusHistory> {
                            new DoorStatusHistory{
                                Id = 10,
                                ChangeDate = DateTime.Now,
                                DoorId = 1,
                                IsOnline = true
                            }
                        }
                    },
                    new Door{
                        Id = 1,
                        Name = "Second Door",
                        GarageId = 2,
                        IP = "192.168.1.52",
                        DoorStatusHistories = new List<DoorStatusHistory> {
                            new DoorStatusHistory{
                                Id = 20,
                                ChangeDate = DateTime.Now.AddSeconds(-30),
                                DoorId = 2,
                                IsOnline = false
                            }
                        }
                    },
                }
            });

            var sut = new GetGarageDetailsQueryHandler(_garageRepository.Object, _mapper);

            var result = await sut.Handle(new GetGarageDetailsQuery { GarageId = 2 }, CancellationToken.None);
            Assert.Equal(2, result.GarageId);
            Assert.Equal(2, result.Doors.Count());
            Assert.IsType<GarageDto>(result);
        }
    }
}
