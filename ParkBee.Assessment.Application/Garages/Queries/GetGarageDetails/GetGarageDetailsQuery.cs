using MediatR;

namespace ParkBee.Assessment.Application.Garages.Queries.GetGarageDetails
{
    public class GetGarageDetailsQuery : IRequest<GarageDto>
    {
        public int GarageId { get; set; }
    }
}
