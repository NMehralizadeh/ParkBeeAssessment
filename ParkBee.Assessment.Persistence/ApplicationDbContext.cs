using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Domain.Entities;
using ParkBee.Assessment.Application.Interfaces;
namespace ParkBee.Assessment.Persistence
{
    public partial class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Garage> Garages { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<DoorStatusHistory> DoorStatusHistories { get; set; }
    }
}