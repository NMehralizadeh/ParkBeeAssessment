using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Persistence
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Owner> Owners { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<DoorStatusHistory> DoorStatuses { get; set; }
    }
}