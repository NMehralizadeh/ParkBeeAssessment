using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Persistence.Configurations
{
    public class DoorConfig : IEntityTypeConfiguration<Door>
    {
        public void Configure(EntityTypeBuilder<Door> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasMaxLength(64);
            builder.Property(e => e.IP).HasMaxLength(64);

            builder.HasOne(d => d.Garage)
                .WithMany(p => p.Doors)
                .HasForeignKey(d => d.GarageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
