using System.Threading.Tasks;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Application.Interfaces
{
    public interface IDoorStatusService
    {
        Task<bool> CheckDoorStatus(Door door);
    }
}