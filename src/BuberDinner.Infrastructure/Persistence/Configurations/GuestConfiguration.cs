using BuberDinner.Domain.Guest;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.Entities;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;

namespace BuberDinner.Infrastructure.Persistence.Configurations;
internal class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        ConfigureGuestsTable(builder);
        ConfigureGuestUserIdsTable(builder);
        ConfigureGuestUpcomingDinnerIdsTable(builder);
        ConfigureGuestPastDinnerIdsTable(builder);
        ConfigureGuestPendingDinnerIdsTable(builder);
        ConfigureGuestBillIdsTable(builder);
        ConfigureGuestMenuReviewIdsTable(builder);
        ConfigureDinnerRatingsTable(builder);




    }

    private void ConfigureGuestUserIdsTable(EntityTypeBuilder<Guest> builder)
    {
        builder.OwnsMany(x => x.UserIds, dib =>
        {
            dib.ToTable("GuestUserIds");

            dib.WithOwner().HasForeignKey("GuestId");

            dib.HasKey("Id");

            dib.Property(x => x.Value)
            .HasColumnName("UserId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Guest.UserIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureGuestUpcomingDinnerIdsTable(EntityTypeBuilder<Guest> builder)
    {
        builder.OwnsMany(x => x.UpcomingDinnerIds, dib =>
        {
            dib.ToTable("GuestUpcomingDinnerIds");

            dib.WithOwner().HasForeignKey("GuestId");

            dib.HasKey("Id");

            dib.Property(x => x.Value)
            .HasColumnName("UpcomingDinnerId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Guest.UpcomingDinnerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureGuestPastDinnerIdsTable(EntityTypeBuilder<Guest> builder)
    {
        builder.OwnsMany(x => x.PastDinnerIds, dib =>
        {
            dib.ToTable("GuestPastDinnerIds");

            dib.WithOwner().HasForeignKey("GuestId");

            dib.HasKey("Id");

            dib.Property(x => x.Value)
            .HasColumnName("PastDinnerId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Guest.PastDinnerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    private void ConfigureGuestPendingDinnerIdsTable(EntityTypeBuilder<Guest> builder)
    {
        builder.OwnsMany(x => x.PendingDinnerIds, dib =>
        {
            dib.ToTable("GuestPendingDinnerIds");

            dib.WithOwner().HasForeignKey("GuestId");

            dib.HasKey("Id");

            dib.Property(x => x.Value)
            .HasColumnName("PendingDinnerId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Guest.PendingDinnerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureGuestBillIdsTable(EntityTypeBuilder<Guest> builder)
    {
        builder.OwnsMany(x => x.BillIds, dib =>
        {
            dib.ToTable("GuestBillIds");

            dib.WithOwner().HasForeignKey("GuestId");

            dib.HasKey("Id");

            dib.Property(x => x.Value)
            .HasColumnName("BillId")
            .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Guest.BillIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    private void ConfigureGuestMenuReviewIdsTable(EntityTypeBuilder<Guest> builder)
    {
        builder.OwnsMany(x => x.MenuReviewIds, dib =>
        {
            dib.ToTable("GuestMenuReviewIds");

            dib.WithOwner().HasForeignKey("GuestId");

            dib.HasKey("Id");

            dib.Property(x => x.Value)
            .HasColumnName("MenuReviewId")
            .ValueGeneratedNever();

        });

        builder.Metadata.FindNavigation(nameof(Guest.MenuReviewIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    private void ConfigureGuestsTable(EntityTypeBuilder<Guest> builder)
    {
        builder.ToTable("Guests");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => GuestId.Create(value));

        builder.Property(x => x.FirstName)
           .HasMaxLength(100);

        builder.Property(x => x.LastName)
           .HasMaxLength(100);

        builder.Property(x => x.ProfileImage)
           .HasMaxLength(100);

        builder.OwnsOne(x => x.AverageRating);


    }

    private void ConfigureDinnerRatingsTable(EntityTypeBuilder<Guest> builder)
    {
        builder.OwnsMany(x => x.DinnerRatings, sb =>
        {

            sb.ToTable("DinnerRatings");

            sb.WithOwner()
                .HasForeignKey("GuestId");

            sb.Property(x => x.Id)
                .HasColumnName("RatingId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => RatingId.Create(value));
            
            sb.HasKey("Id", "GuestId");

            sb.OwnsOne(x => x.Rating);

            sb.Property(x => x.DinnerId)
                .HasConversion(
                    id => id.Value,
                    value => DinnerId.Create(value));

            sb.Property(x => x.HostId)
                .HasConversion(
                    id => id.Value,
                    value => HostId.Create(value));
        });

        builder.Metadata.FindNavigation(nameof(Guest.DinnerRatings))!
           .SetPropertyAccessMode(PropertyAccessMode.Field);
    }


    //public sealed class Guest2 : AggregateRoot<GuestId>
    //{
    //    private readonly List<UserId> _userIds = new();
    //    private readonly List<DinnerId> _upcomingDinnerIds = new();
    //    private readonly List<DinnerId> _pastDinnerIds = new();
    //    private readonly List<DinnerId> _pendingDinnerIds = new();
    //    private readonly List<BillId> _billIds = new();
    //    private readonly List<MenuReviewId> _menuReviewIds = new();
    //    private readonly List<DinnerRating> _dinnerRatings = new();
    //    public string FirstName { get; }
    //    public string LastName { get; }
    //    public Uri ProfileImage { get; }
    //    public AverageRating AverageRating { get; }
    //    public IReadOnlyList<UserId> UserIds => _userIds.AsReadOnly();
    //    public IReadOnlyList<DinnerId> UpcomingDinnerIds => _upcomingDinnerIds.AsReadOnly();
    //    public IReadOnlyList<DinnerId> PastDinnerIds => _pastDinnerIds.AsReadOnly();
    //    public IReadOnlyList<DinnerId> PendingDinnerIds => _pendingDinnerIds.AsReadOnly();
    //    public IReadOnlyList<BillId> BillIds => _billIds.AsReadOnly();
    //    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
    //    public IReadOnlyList<DinnerRating> DinnerRatings => _dinnerRatings.AsReadOnly();
    //    public DateTime CreatedDatetime { get; }
    //    public DateTime UpdatedDatetime { get; }
    //}
    //private void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
    //{



    //    builder.OwnsMany(x => x.Sections, sb =>
    //    {
    //        sb.ToTable("MenuSections");

    //        sb.WithOwner()
    //        .HasForeignKey("MenuId");

    //        sb.HasKey("Id", "MenuId");

    //        sb.Property(x => x.Id)
    //        .HasColumnName("MenuSectionId")
    //        .ValueGeneratedNever()
    //        .HasConversion(
    //            id => id.Value,
    //            value => MenuSectionId.Create(value));

    //        sb.Property(x => x.Name)
    //                .HasMaxLength(100);

    //        sb.Property(x => x.Description)
    //                .HasMaxLength(100);
    //    });





    //    builder.ToTable("Menus");
    //    builder.HasKey(x => x.Id);
    //    builder.Property(x => x.Id)
    //        .ValueGeneratedNever()
    //        .HasConversion(
    //        id => id.Value,
    //        value => MenuId.Create(value));

    //    builder.Property(x => x.Name)
    //        .HasMaxLength(100);

    //    builder.Property(x => x.Description)
    //        .HasMaxLength(100);

    //    builder.OwnsOne(x => x.AverageRating);

    //    builder.Property(x => x.HostId)
    //        .HasConversion(
    //        id => id.Value,
    //        value => HostId.Create(value));




    //}
}
