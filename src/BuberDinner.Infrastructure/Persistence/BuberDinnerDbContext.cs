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
    private readonly InsertOutBoxMessagesInterceptor _insertOutBoxMessagesInterceptor;
    private readonly SoftDeleteInterceptor _softDeleteInterceptor;

    public BuberDinnerDbContext(DbContextOptions<BuberDinnerDbContext> options,
                                PublishDoimainEventsIntercetor publishDoimainEventsIntercetor, 
                                InsertOutBoxMessagesInterceptor insertOutBoxMessagesInterceptor,
                                SoftDeleteInterceptor softDeleteInterceptor) : base(options)
    {
        _publishDoimainEventsIntercetor = publishDoimainEventsIntercetor;
        _insertOutBoxMessagesInterceptor = insertOutBoxMessagesInterceptor;
        _softDeleteInterceptor = softDeleteInterceptor;
    }
    public DbSet<Bill> Bills { get; set; } = null!;
    public DbSet<Dinner> Dinners { get; set; } = null!;
    public DbSet<Guest> Guests { get; set; } = null!;
    public DbSet<Host> Hosts { get; set; } = null!;
    public DbSet<Menu> Menus { get; set; } = null!;
    public DbSet<MenuReview> MenuReviews { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(BuberDinnerDbContext).Assembly);

        base.OnModelCreating(builder); 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_insertOutBoxMessagesInterceptor, _publishDoimainEventsIntercetor, _softDeleteInterceptor);

        base.OnConfiguring(optionsBuilder);
    }
}
