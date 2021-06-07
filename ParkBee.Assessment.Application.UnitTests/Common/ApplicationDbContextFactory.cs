using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Interfaces.Repositories;
using ParkBee.Assessment.Domain.Entities;
using ParkBee.Assessment.Persistence;
using ParkBee.Assessment.Persistence.Repositories;

namespace ParkBee.Assessment.Application.UnitTests.Common
{
    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();
            context.Garages.RemoveRange(context.Garages);
            context.Doors.RemoveRange(context.Doors);
            context.DoorStatusHistories.RemoveRange(context.DoorStatusHistories);
            context.SaveChanges();

            context.Garages.AddRange(
                new Garage { Id = 1 },
                new Garage { Id = 2 }
                );
            context.SaveChanges();

            context.Doors.AddRange(
                new Door
                {
                    GarageId = 1,
                    Id = 1
                },
                new Door
                {
                    GarageId = 2,
                    Id = 2
                },
                new Door
                {
                    GarageId = 2,
                    Id = 3
                }
                );
            context.SaveChanges();

            context.DoorStatusHistories.AddRange(
                new DoorStatusHistory { DoorId = 1, IsOnline = true, ChangeDate = DateTimeOffset.Now },
                new DoorStatusHistory { DoorId = 2, IsOnline = false },
                new DoorStatusHistory { DoorId = 2, IsOnline = false, ChangeDate = DateTimeOffset.Now.AddHours(-3) },
                new DoorStatusHistory { DoorId = 3, IsOnline = false, ChangeDate = DateTimeOffset.Now.AddHours(-1) },
                new DoorStatusHistory { DoorId = 3, IsOnline = false }
                );
            context.SaveChanges();

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}