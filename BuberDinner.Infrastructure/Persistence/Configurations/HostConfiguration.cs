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
            .HasColumnName("HostMenuId")
            .ValueGeneratedNever();
            
        });

        builder.Metadata.FindNavigation(nameof(Host.MenuIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    //private void ConfigureMenuDinnerIdsTable2(EntityTypeBuilder<Menu> builder)
    //{
    //    builder.OwnsMany(x => x.DinnerIds, dib =>
    //    {
    //        dib.ToTable("MenuIds");

    //        dib.WithOwner().HasForeignKey("MenuId");

    //        dib.HasKey("Id");

    //        dib.Property(x => x.Value)
    //        .HasColumnName("DinnerId")
    //        .ValueGeneratedNever();
    //    });


    //    builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!
    //        .SetPropertyAccessMode(PropertyAccessMode.Field);
    //}
    //private void ConfigureMenusTable2(EntityTypeBuilder<Menu> builder)
    //{
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
//public sealed class Host2 : AggregateRoot<HostId>
//{
//    private readonly List<MenuId> _menuIds = new();
//    private readonly List<DinnerId> _dinnerIds = new();
//    public string FirstName { get; }
//    public string LastName { get; }
//    public Uri ProfileImage { get; }
//    public AverageRating AverageRating { get; }
//    public UserId UserId { get; }
//    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
//    public IReadOnlyList<MenuId> MenuIds => _menuIds.AsReadOnly();
//    public DateTime CreatedDatetime { get; }
//    public DateTime UpdatedDatetime { get; }
//}