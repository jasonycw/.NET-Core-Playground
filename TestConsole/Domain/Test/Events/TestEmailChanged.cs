using System;
using TestConsole.Domain.Core;

namespace TestConsole.Domain.Test.Events
{
    public class TestEmailChanged : DomainEvent<Test>
    {
        public Email OldEmail { get; }

        public TestEmailChanged(Guid entityId, Email oldEmail) : base(entityId.ToString()) => OldEmail = oldEmail;
    }
}
