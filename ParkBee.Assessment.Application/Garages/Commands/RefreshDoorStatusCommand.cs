using MediatR;

namespace ParkBee.Assessment.Application.Garages.Commands
{
    public class RefreshDoorStatusCommand : IRequest<bool>
    {
        public int DoorId { get; set; }
    }
}
