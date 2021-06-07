using System.Linq;
using AutoMapper;
using ParkBee.Assessment.Application.Mappings;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Application.Garages.Queries.GetGarageDetails
{
    public class DoorDto : IMapFrom<Door>
    {
        public int DoorId { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }

        public void Mapping(Profile profile)
        {

            profile.CreateMap<Door, DoorDto>().MaxDepth(0)
            .ForMember(d => d.DoorId, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.IsOnline, c => c.MapFrom(s => s.DoorStatusHistories.FirstOrDefault().IsOnline));
        }
    }
}
