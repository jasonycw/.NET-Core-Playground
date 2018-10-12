using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestConsole.Domain.Core;
using TestConsole.Domain.Test.Events;

namespace TestConsole.Domain.Test.Handler
{
    public class TestCreatedHandler : DomainEventHandler<TestCreated>
    {
        public override Task Handle(TestCreated domainEvent, CancellationToken token)
        {
            SaveChangesAsync(domainEvent, token);
            return Task.CompletedTask;
        }
    }
}
