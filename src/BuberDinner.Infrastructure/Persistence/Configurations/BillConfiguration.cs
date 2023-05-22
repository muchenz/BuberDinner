using BuberDinner.Domain.Bill;
using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner;
using BuberDinner.Domain.Dinner.ValueObjects;
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

namespace BuberDinner.Infrastructure.Persistence.Configurations;
internal class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        ConfigureBillssTable(builder);
    }

    private void ConfigureBillssTable(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable("Bills");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => BillId.Create(value));

        builder.Property(x => x.DinnerId)
          .HasConversion(
          id => id.Value,
          value => DinnerId.Create(value));

        builder.Property(x => x.GuestId)
          .HasConversion(
          id => id.Value,
          value => GuestId.Create(value));

        builder.Property(x => x.HostId)
          .HasConversion(
          id => id.Value,
          value => HostId.Create(value));


        builder.OwnsOne(x => x.Price)
            .Property(p => p.Amount)
            .HasColumnType("decimal(18,2)");
    }

}

