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
public sealed class InsertOutBoxMessagesInterceptor: SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {

        if (eventData.Context is not null)
        {
            InsterOutboxMessages(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {

        if (eventData.Context is not null)
        {
            InsterOutboxMessages(eventData.Context);
        }
        return base.SavingChanges(eventData, result);
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
            .Select(domainEvent=> new OutboxMessage
            {
                Id= Guid.NewGuid(),
                Type=domainEvent.GetType().FullName!,
                Content= JsonSerializer.Serialize(domainEvent),
                OccureredOnUtc= DateTime.UtcNow,

            }).ToList();
        
        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}


public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime OccureredOnUtc { get; set; }
    public DateTime? ProcessedOnUtc { get; set; } = null;
    public string? Error { get; set; } = null;


}