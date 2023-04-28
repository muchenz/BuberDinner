using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner;
using BuberDinner.Domain.Dinner.Entities;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuberDinner.Domain.Common.Enums.Enums;

namespace BuberDinner.Infrastructure.Persistence.Configurations;
internal class DinnerConfiguration : IEntityTypeConfiguration<Dinner>
{
    public void Configure(EntityTypeBuilder<Dinner> builder)
    {
        ConfigureDinnersTable(builder);
        ConfigureReservationsTable(builder);
    }

    private void ConfigureDinnersTable(EntityTypeBuilder<Dinner> builder)
    {
        builder.ToTable("Dinners");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => DinnerId.Create(value));

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(100);

        builder.Property(x => x.StartDatetime);
        builder.Property(x => x.EndDatetime);
        builder.Property(x => x.StartedDatetime);
        builder.Property(x => x.EndedDatetime);
        builder.Property(x => x.Status);
        builder.Property(x => x.IsPublic);
        builder.Property(x => x.MaxGests);

        builder.OwnsOne(x => x.Price)
            .Property(p => p.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.HostId)
            .HasConversion(
            id => id.Value,
            value => HostId.Create(value));

        builder.Property(x => x.MenuId)
           .HasConversion(
           id => id.Value,
           value => MenuId.Create(value));

        builder.Property(x => x.ImageUrl);

        builder.OwnsOne(x => x.Location);

    }

    private void ConfigureReservationsTable(EntityTypeBuilder<Dinner> builder)
    {
        builder.OwnsMany(x => x.Reservations, sb =>
        {
            sb.ToTable("Reservations");

            sb.WithOwner()
                .HasForeignKey("DinnerId");

            sb.Property(x => x.Id)
                .HasColumnName("ReservationId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ReservationId.Create(value));

            sb.HasKey("Id", "DinnerId");



            sb.Property(x => x.GuestId)
                .HasConversion(
                    id => id.Value,
                    value => GuestId.Create(value));

            sb.Property(x => x.BillId)
              .HasConversion(
                  id => id.Value,
                  value => BillId.Create(value));

            sb.Property(x => x.GuestCount);
            sb.Property(x => x.ReservationStatus);


            sb.Property(x => x.ArrivalDateTime);
            sb.Property(x => x.CreatedDateTime);
            sb.Property(x => x.UpdatedDateTime);

        });

        builder.Metadata.FindNavigation(nameof(Dinner.Reservations))!
           .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}



