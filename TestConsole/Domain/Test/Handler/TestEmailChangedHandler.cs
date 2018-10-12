using System.Threading;
using System.Threading.Tasks;
using TestConsole.Domain.Core;
using TestConsole.Domain.Test.Events;

namespace TestConsole.Domain.Test.Handler
{
    public class TestEmailChangedHandler : DomainEventHandler<TestEmailChanged>
    {
        public override Task Handle(TestEmailChanged domainEvent, CancellationToken token)
        {
            SaveChangesAsync(domainEvent, token);
            return Task.CompletedTask;
        }
    }
}
