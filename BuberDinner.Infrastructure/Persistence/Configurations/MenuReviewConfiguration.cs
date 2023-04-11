using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview;
using BuberDinner.Domain.MenuReview.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Persistence.Configurations;
internal class MenuReviewConfiguration : IEntityTypeConfiguration<MenuReview>
{
    public void Configure(EntityTypeBuilder<MenuReview> builder)
    {
        ConfigureMenuReviewsTable(builder);
    }
    private void ConfigureMenuReviewsTable(EntityTypeBuilder<MenuReview> builder)
    {
        builder.ToTable("MenuReviews");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => MenuReviewId.Create(value));

        builder.OwnsOne(x => x.Rating);

        builder.Property(x => x.Comment)
            .HasMaxLength(200);


        builder.Property(x => x.HostId)
         .HasConversion(
         id => id.Value,
         value => HostId.Create(value));

        builder.Property(x => x.MenuId)
         .HasConversion(
         id => id.Value,
         value => MenuId.Create(value));

        builder.Property(x => x.GuestId)
         .HasConversion(
         id => id.Value,
         value => GuestId.Create(value));

        builder.Property(x => x.DinnerId)
         .HasConversion(
         id => id.Value,
         value => DinnerId.Create(value));

    }
     
}

