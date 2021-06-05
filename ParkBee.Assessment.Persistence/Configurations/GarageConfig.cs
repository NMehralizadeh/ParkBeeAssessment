using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkBee.Assessment.Domain.Entities;

namespace ParkBee.Assessment.Persistence.Configurations
{
    public class GarageConfig : IEntityTypeConfiguration<Garage>
    {
        public void Configure(EntityTypeBuilder<Garage> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasMaxLength(64);
            builder.Property(e => e.Address).HasMaxLength(512);
            builder.Property(e => e.PhoneNumber).HasMaxLength(20);
        }
    }
}
