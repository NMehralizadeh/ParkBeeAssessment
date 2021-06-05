using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Persistence.Configurations
{
    public class DoorStatusHistoryConfig : IEntityTypeConfiguration<DoorStatusHistory>
    {
        public void Configure(EntityTypeBuilder<DoorStatusHistory> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasOne(d => d.Door)
                .WithMany(p => p.DoorStatusHistories)
                .HasForeignKey(d => d.DoorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
