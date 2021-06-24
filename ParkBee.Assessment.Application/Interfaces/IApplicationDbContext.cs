using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ParkBee.Assessment.Application.Interfaces
{
    public partial interface IApplicationDbContext
    {
        DbSet<Door> Doors { get; set; }
        DbSet<DoorStatusHistory> DoorStatusHistories { get; set; }
        DbSet<Garage> Garages { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken= default(CancellationToken));
    }
}