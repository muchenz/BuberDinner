using BuberDinner.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Interceptors;
internal sealed class InsertOutBoxMessagesInterceptor: SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {

        if (eventData.Context is not null)
        {

        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void InsterOutboxMessages(DbContext dbContext)
    {
        var outboxMessages = dbContext
            .ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(a => a.Entity.DomainEvents.Any())
            .Select(a => a.Entity)
            .SelectMany(enity=>
            {
                List<IDomainEvent> domainEvents = enity.DomainEvents.ToList();
                enity.ClearDomainEvents();

                return domainEvents;

            })
            .Select(domainEvent=> new OutBoxMessage
            {
                Id= Guid.NewGuid(),
                Type=domainEvent.GetType().Name,
                Content= JsonSerializer.Serialize(domainEvent),
                OccureredOnUtc= DateTime.UtcNow,

            })
    }
}


public sealed class OutBoxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime OccureredOnUtc { get; set; }
    public DateTime? ProcessedOnUtc { get; set; } = null;
    public string? Error { get; set; } = null;


}