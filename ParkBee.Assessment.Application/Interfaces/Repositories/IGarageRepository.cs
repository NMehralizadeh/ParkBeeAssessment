using System.Threading.Tasks;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Application.Interfaces.Repositories
{
    public interface IGarageRepository
    {
        Task<Garage> GetGarageDetail(int garageId);
    }
}