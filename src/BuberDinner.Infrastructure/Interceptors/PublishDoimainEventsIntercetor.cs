using BuberDinner.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Interceptors;
public class PublishDoimainEventsIntercetor : SaveChangesInterceptor
{
    private readonly IPublisher _mediator;

    public PublishDoimainEventsIntercetor(IPublisher mediator)
    {
        _mediator = mediator;
    }
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await PublishDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext? dbContext)
    {

        if (dbContext == null)
        {
            return;
        }

        // get hold of all the varius emtities

        var entitisWithDoimainEvents = dbContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(a => a.Entity.DomainEvents.Any())
            .Select(a=>a.Entity)
            .ToList();
        //get hold of all domain event

        var domainEvents = entitisWithDoimainEvents.SelectMany(a => a.DomainEvents).ToList();

        //clear doimain events
        entitisWithDoimainEvents.ForEach(a => a.ClearDomainEvents());
        //publish doimain event

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }

    }
}
