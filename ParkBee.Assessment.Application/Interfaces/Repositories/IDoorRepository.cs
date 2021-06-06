using System.Collections.Generic;
using System.Threading.Tasks;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Application.Interfaces.Repositories
{
    public interface IDoorRepository
    {
        Task<IReadOnlyList<Door>> GetAllDoors();
        Task<Door> GetDoorWithLatestStatus(int doorId, int garageId);
        Task ChangeDoorStatus(int doorId, int garageId, bool isOnline);
        Task ChangeDoorStatus(Door door, bool isOnline);
    }
}