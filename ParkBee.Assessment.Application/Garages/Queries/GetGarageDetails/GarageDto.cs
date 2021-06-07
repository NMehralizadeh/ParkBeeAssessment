using System.Collections.Generic;
using AutoMapper;
using ParkBee.Assessment.Application.Mappings;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Application.Garages.Queries.GetGarageDetails
{
    public class GarageDto : IMapFrom<Garage>
    {
        public int GarageId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<DoorDto> Doors { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Garage, GarageDto>()
            .ForMember(d => d.GarageId, opt => opt.MapFrom(s => s.Id));
        }
    }
}
