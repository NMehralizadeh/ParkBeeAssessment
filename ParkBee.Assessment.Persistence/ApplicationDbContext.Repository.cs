using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Interfaces.Repositories;
using ParkBee.Assessment.Persistence.Repositories;

namespace ParkBee.Assessment.Persistence
{
    public partial class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private IDoorRepository _doorRepository;
        public IDoorRepository DoorRepository => _doorRepository ??= new DoorRepository(this);
    }
}