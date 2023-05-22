using BuberDinner.Domain.Bill;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Guest;
using BuberDinner.Domain.Host;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.MenuReview;
using BuberDinner.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Persistence;
public class BuberDinnerDbContext:DbContext
{
    private readonly PublishDoimainEventsIntercetor _publishDoimainEventsIntercetor;

    public BuberDinnerDbContext(DbContextOptions<BuberDinnerDbContext> options,
                                PublishDoimainEventsIntercetor publishDoimainEventsIntercetor) : base(options)
    {
        _publishDoimainEventsIntercetor = publishDoimainEventsIntercetor;
    }
    public DbSet<Bill> Bills { get; set; } = null!;
    public DbSet<Dinner> Dinners { get; set; } = null!;
    public DbSet<Guest> Guests { get; set; } = null!;
    public DbSet<Host> Hosts { get; set; } = null!;
    public DbSet<Menu> Menus { get; set; } = null!;
    public DbSet<MenuReview> MenuReviews { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(BuberDinnerDbContext).Assembly);

        base.OnModelCreating(builder); 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDoimainEventsIntercetor);

        base.OnConfiguring(optionsBuilder);
    }
}
