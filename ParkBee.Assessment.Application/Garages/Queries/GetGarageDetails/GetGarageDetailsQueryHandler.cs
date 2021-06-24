using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ParkBee.Assessment.Application.Exceptions;
using ParkBee.Assessment.Application.Interfaces.Repositories;

namespace ParkBee.Assessment.Application.Garages.Queries.GetGarageDetails
{
    public class GetGarageDetailsQueryHandler : IRequestHandler<GetGarageDetailsQuery, GarageDto>
    {
        private readonly IGarageRepository _garageRepository;
        private readonly IMapper _mapper;

        public GetGarageDetailsQueryHandler(
            IGarageRepository garageRepository,
            IMapper mapper
            )
        {
            _garageRepository = garageRepository;
            _mapper = mapper;
        }

        public async Task<GarageDto> Handle(GetGarageDetailsQuery request, CancellationToken cancellationToken)
        {
            var garage = await _garageRepository.GetGarageDetail(request.GarageId);
            if (garage == null)
                throw new NotFoundException($"Garage with Id {request.GarageId} not found");

            return _mapper.Map<GarageDto>(garage);
        }
    }
}