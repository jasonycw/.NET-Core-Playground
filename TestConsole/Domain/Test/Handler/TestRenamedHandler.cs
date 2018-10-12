using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestConsole.Domain.Core;
using TestConsole.Domain.Test.Events;

namespace TestConsole.Domain.Test.Handler
{
    public class TestRenamedHandler : DomainEventHandler<TestRenamed>
    {
        public override Task Handle(TestRenamed domainEvent, CancellationToken token)
        {
            SaveChangesAsync(domainEvent, token);
            return Task.CompletedTask;
        }
    }
}
