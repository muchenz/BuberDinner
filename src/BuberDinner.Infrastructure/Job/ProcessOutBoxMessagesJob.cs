using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.Events;

using BuberDinner.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Job;




[DisallowConcurrentExecution]
internal class ProcessOutBoxMessagesJob:IJob
{
    private readonly IOutBoxMessageRepository _outBoxMessageRepository;
    private readonly IPublisher _publisher;

    public ProcessOutBoxMessagesJob(IOutBoxMessageRepository outBoxMessageRepository,
                                    IPublisher publisher)
    {
        _outBoxMessageRepository = outBoxMessageRepository;
        _publisher = publisher;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _outBoxMessageRepository.GetAll();
        //string typeName = "MyNamespace.MyClass"; // Type.FullName
        string assemblyName = "BuberDinner.Domain"; // MyAssembly.FullName or MyAssembly.GetName().Name
        

        foreach (var message in messages.Where(a => a.ProcessedOnUtc == null))
        {
            string assemblyQualifiedName = Assembly.CreateQualifiedName(assemblyName, message.Type)!;
            Type? type = Type.GetType(assemblyQualifiedName);

            if (type is null) continue;

            var domainEvent = JsonSerializer.Deserialize(message.Content, type) as IDomainEvent;

            //-----------------------------------
            //var options = new Newtonsoft.Json.JsonSerializerSettings { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All };

            //var domainEvent = Newtonsoft.Json.JsonConvert.DeserializeObject<IDomainEvent>(message.Content,options);
            //---------------------------------------
            if (domainEvent is null)
            {
                continue;
            }

            await _publisher.Publish(domainEvent);

            message.ProcessedOnUtc = DateTime.UtcNow;

        }
    }
}

