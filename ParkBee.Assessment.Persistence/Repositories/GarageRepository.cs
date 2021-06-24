using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Interfaces.Repositories;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Persistence.Repositories
{

}
public class GarageRepository : IGarageRepository
{
    private readonly IApplicationDbContext _dbContext;

    public GarageRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Garage> GetGarageDetail(int garageId)
    {
        return await _dbContext.Garages.Include(g => g.Doors)
            .ThenInclude(d => d.DoorStatusHistories.OrderByDescending(x => x.ChangeDate).Take(1))
            .FirstOrDefaultAsync(g => g.Id == garageId);
    }
}