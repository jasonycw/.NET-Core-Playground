using System;
using TestConsole.Domain.Core;

namespace TestConsole.Domain.Test.Events
{
    public class TestCreated : DomainEvent<Test>
    {
        public TestCreated(Guid testId)
            : base(testId.ToString()) { }
    }
}
