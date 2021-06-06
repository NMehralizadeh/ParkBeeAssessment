using ParkBee.Assessment.Application.Interfaces.Repositories;

namespace ParkBee.Assessment.Application.Interfaces
{
    public partial interface IApplicationDbContext
    {
        IDoorRepository DoorRepository { get; }
    }
}