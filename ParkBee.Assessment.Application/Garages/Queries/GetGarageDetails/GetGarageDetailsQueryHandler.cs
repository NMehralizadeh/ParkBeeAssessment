using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Exceptions;
using ParkBee.Assessment.Application.Interfaces;

namespace ParkBee.Assessment.Application.Garages.Queries.GetGarageDetails
{
    public class GetGarageDetailsQueryHandler : IRequestHandler<GetGarageDetailsQuery, GarageDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILoggedInUserContext _loggedInUserContext;
        private readonly IMapper _mapper;

        public GetGarageDetailsQueryHandler(IApplicationDbContext dbContext, ILoggedInUserContext loggedInUserContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _loggedInUserContext = loggedInUserContext;
            _mapper = mapper;
        }

        public async Task<GarageDto> Handle(GetGarageDetailsQuery request, CancellationToken cancellationToken)
        {
            var garage = await _dbContext.Garages.Include(g => g.Doors)
                .ThenInclude(d => d.DoorStatusHistories.OrderByDescending(p => p.ChangeDate).Take(1))
                .FirstOrDefaultAsync(g => g.Id == _loggedInUserContext.GarageId, cancellationToken: cancellationToken);
            if (garage == null)
                throw new NotFoundException($"Garage with Id {_loggedInUserContext.GarageId} not found");
            
            return _mapper.Map<GarageDto>(garage);
        }
    }
}