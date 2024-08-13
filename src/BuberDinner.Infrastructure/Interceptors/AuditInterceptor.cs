using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Interceptors;
public sealed class AuditInterceptor : SaveChangesInterceptor
{
    private readonly List<AuditEntry> _auditEntries;

    public AuditInterceptor(List<AuditEntry> auditEntries)
    {
        _auditEntries = auditEntries;
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var startTime = DateTime.UtcNow;

        var auditEntries = eventData.Context.ChangeTracker.
                            Entries()
                            .Where(a => a.Entity is not AuditEntry &&
                            a.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                            .Select(a => new AuditEntry
                            {
                                Id = Guid.NewGuid(), //CreateVerison7
                                StartTimeUtc = startTime,
                                MetaData = a.DebugView.LongView
                            }).ToList();

        if (auditEntries.Count == 0)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        }

        _auditEntries.AddRange(auditEntries);


        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {


        if (eventData.Context is null)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        var endTime = DateTime.UtcNow;

        foreach (var auditEntry in _auditEntries)
        {
            auditEntry.EndTimeUtc = endTime;
            auditEntry.Succeeded = true;
        }

        if (_auditEntries.Count > 0)
        {
            eventData.Context.Set<AuditEntry>().AddRange(_auditEntries);
            _auditEntries.Clear();
            await eventData.Context.SaveChangesAsync(cancellationToken);

        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public override async Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {

        if (eventData.Context is null)
        {
            await base.SaveChangesFailedAsync(eventData, cancellationToken);
        }

        var endTime = DateTime.UtcNow;

        foreach (var auditEntry in _auditEntries)
        {
            auditEntry.EndTimeUtc = endTime;
            auditEntry.Succeeded = true;
            auditEntry.ErrorMesage = eventData.Exception.Message;
        }

        if (_auditEntries.Count > 0)
        {
            eventData.Context.Set<AuditEntry>().AddRange(_auditEntries);
            _auditEntries.Clear();
            await eventData.Context.SaveChangesAsync(cancellationToken);

        }

        await base.SaveChangesFailedAsync(eventData, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {

        SavingChangesAsync(eventData, result).ConfigureAwait(false).GetAwaiter().GetResult();

        return base.SavingChanges(eventData, result);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        SavedChangesAsync(eventData, result).ConfigureAwait(false).GetAwaiter().GetResult();

        return base.SavedChanges(eventData, result);
    }

    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        SaveChangesFailedAsync(eventData).ConfigureAwait(false).GetAwaiter().GetResult();

        base.SaveChangesFailed(eventData);
    }

}

public class AuditEntry
{
    [Key]
    public Guid Id { get; set; }
    public string MetaData { get; set; } = string.Empty;
    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }
    public bool Succeeded { get; set; }

    //[MaxLength(255)]
    public string ErrorMesage { get; set; } = string.Empty;
}
