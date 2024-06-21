using BuberDinner.Domain.Common;
using BuberDinner.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Interceptors;
public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {

        if (eventData.Context is null)
        {
            return base.SavingChanges(eventData, result);
        }

        SoftDelete(eventData.Context);

        return base.SavingChanges(eventData, result);
    }



    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        }
        SoftDelete(eventData.Context);


        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    private static void SoftDelete(DbContext context)
    {
        IEnumerable<EntityEntry<ISoftDeletable>> entries =
             
             context
            .ChangeTracker
            .Entries<ISoftDeletable>()
            .Where(a => a.State == EntityState.Deleted);

        foreach (EntityEntry<ISoftDeletable> entry in entries)
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsDeleted = true;
            entry.Entity.DeletedOnTime = DateTime.UtcNow;

        }
    }
}
