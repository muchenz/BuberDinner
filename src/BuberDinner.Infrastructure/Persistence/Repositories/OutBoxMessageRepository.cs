using BuberDinner.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Persistence.Repositories;

internal interface IOutBoxMessageRepository
{
    Task<List<OutboxMessage>> GetAll();
}

internal class OutBoxMessageRepository : IOutBoxMessageRepository
{
    private readonly BuberDinnerDbContext _buberDinnerDbContext;

    public OutBoxMessageRepository(BuberDinnerDbContext buberDinnerDbContext)
    {
        _buberDinnerDbContext = buberDinnerDbContext;
    }


    public async Task<List<OutboxMessage>> GetAll()
    {
        List<OutboxMessage> messages= null;
        try
        {
            messages = await _buberDinnerDbContext.OutboxMessages.Where(a => a.ProcessedOnUtc == null).ToListAsync();
        }
        catch (Exception ex)
        {


        }
        return messages;
    }
}
