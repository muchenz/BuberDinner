using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.Menu.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Persistence.Configurations;
internal class HostConfiguration : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
        ConfigureMenusTable(builder);
        ConfigureHostDinnerIdsTable(builder);
        ConfigureHostMenuIdsTable(builder);
    }

    private void ConfigureMenusTable(EntityTypeBuilder<Host> builder)
    {
        builder.ToTable("Hosts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => HostId.Create(value));

        builder.Property(x => x.FirstName)
           .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .HasMaxLength(100);

        builder.Property(x => x.ProfileImage)
           .HasMaxLength(100);

        builder.OwnsOne(x => x.AverageRating);

        builder.Property(x => x.UserId)
            .HasConversion(
            id => id.Value,
            value => UserId.Create(value));

    }

    private void ConfigureHostDinnerIdsTable(EntityTypeBuilder<Host> builder)
    {
        builder.OwnsMany(x => x.DinnerIds, dib =>
        {
            dib.ToTable("HostDinnerIds");

            dib.WithOwner().HasForeignKey("HostId");

            dib.HasKey("Id");

            dib.Property(x => x.Value)
            .HasColumnName("DinnerId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Host.DinnerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureHostMenuIdsTable(EntityTypeBuilder<Host> builder)
    {
        builder.OwnsMany(x => x.MenuIds, dib =>
        {
            dib.ToTable("HostMenuIds");

            dib.WithOwner().HasForeignKey("HostId");

            dib.HasKey("Id");

            dib.Property(x => x.Value)
            .HasColumnName("MenuId")
            .ValueGeneratedNever();
            
        });

        builder.Metadata.FindNavigation(nameof(Host.MenuIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
      
}