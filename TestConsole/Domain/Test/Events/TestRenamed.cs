using System;
using TestConsole.Domain.Core;

namespace TestConsole.Domain.Test.Events
{
    public class TestRenamed : DomainEvent<Test>
    {
        public string OldName { get; }
        public string NewName { get; }

        public TestRenamed(Guid testId, string oldName, string newName)
            : base(testId.ToString())
        {
            OldName = oldName;
            NewName = newName;
        }
    }
}
