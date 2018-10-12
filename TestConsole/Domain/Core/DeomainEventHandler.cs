using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestConsole.Domain.Core
{
    public abstract class DomainEventHandler<TDomainEvent>
        where TDomainEvent : DomainEvent
    {
        public abstract Task Handle(TDomainEvent domainEvent, CancellationToken token);

        protected Task SaveChangesAsync(DomainEvent domainEvent, CancellationToken token)
        {
            Console.WriteLine($"{GetType().Name} is called");
            Console.WriteLine($"Insert event to db");
            Console.WriteLine($"\t Id: {domainEvent.Id}");
            Console.WriteLine($"\t CreationDate: {domainEvent.CreationDate}");
            Console.WriteLine($"\t Type: {domainEvent.Type}");
            Console.WriteLine($"\t EntityType: {domainEvent.EntityType}");
            Console.WriteLine($"\t EntityId: {domainEvent.EntityId}");
            Console.WriteLine($"\t Data: {JsonConvert.SerializeObject(domainEvent.Data)}");
            return Task.CompletedTask;
        }
    }
}
