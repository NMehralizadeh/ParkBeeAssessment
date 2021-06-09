using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Interfaces.Repositories;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Persistence.Repositories
{
    public class DoorRepository : IDoorRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public DoorRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<Door>> GetAllDoors()
        {
            return await _dbContext.Doors
                            .Include(d=>d.DoorStatusHistories.OrderByDescending(dsh=>dsh.ChangeDate))
                            .ToListAsync();
        }

        public async Task<Door> GetDoorWithLatestStatus(int doorId, int garageId)
        {
            var door = await _dbContext.Doors.Include(d => d.DoorStatusHistories.OrderByDescending(p => p.ChangeDate).Take(1))
                .FirstOrDefaultAsync(d => d.Id == doorId && d.Garage.Id == garageId);
                
            return door;
        }

        public async Task ChangeDoorStatus(int doorId, int garageId, bool isOnline)
        {
            var door = await GetDoorWithLatestStatus(doorId, garageId);
            if (door != null)
                await ChangeDoorStatus(door, isOnline);
        }

        public async Task ChangeDoorStatus(Door door, bool isOnline)
        {
            // if door status changed, new DoorStatusHistory record should be added to DB
            if (!door.DoorStatusHistories.Any() || door.DoorStatusHistories.First().IsOnline != isOnline)
                door.DoorStatusHistories.Add(new DoorStatusHistory { IsOnline = isOnline, ChangeDate = DateTimeOffset.Now });

            await _dbContext.SaveChangesAsync();
        }
    }
}
