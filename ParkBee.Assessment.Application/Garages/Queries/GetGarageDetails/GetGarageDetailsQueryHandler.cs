using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Exceptions;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Application.Garages.Queries.GetGarageDetails
{
    public class GetGarageDetailsQueryHandler : IRequestHandler<GetGarageDetailsQuery, GarageDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILoggedInUserContext _loggedInUserContext;

        public GetGarageDetailsQueryHandler(IApplicationDbContext dbContext, ILoggedInUserContext loggedInUserContext)
        {
            _dbContext = dbContext;
            _loggedInUserContext = loggedInUserContext;
        }

        public async Task<GarageDto> Handle(GetGarageDetailsQuery request, CancellationToken cancellationToken)
        {
            var garage = await _dbContext.Garages.Include(g => g.Doors)
                .ThenInclude(d => d.DoorStatusHistories.OrderByDescending(p => p.ChangeDate).Take(1))
                .FirstOrDefaultAsync(g => g.Id == _loggedInUserContext.GarageId, cancellationToken: cancellationToken);
            if (garage == null)
                throw new NotFoundException($"Garage with Id {_loggedInUserContext.GarageId} not found");

            return MapGaragetoDto(garage);
        }

        private GarageDto MapGaragetoDto(Garage garage)
        {
            var doors = new List<DoorDto>();
            garage.Doors.ToList().ForEach(door =>
            {
                doors.Add(new DoorDto
                {
                    DoorId = door.Id,
                    Name = door.Name,
                    IsOnline = door.DoorStatusHistories.First().IsOnline
                });
            });

            return new GarageDto
            {
                GarageId = garage.Id,
                Name = garage.Name,
                Address = garage.Address,
                Doors = doors
            };
        }
    }
}