using System;
using TestConsole.Domain.Core;
using TestConsole.Domain.Test.Events;

namespace TestConsole.Domain.Test
{
    public class Test : Entity<Guid>
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }

        public void Rename(string name)
        {
            var oldName = Name;
            Name = name;
            RaiseEvent(new TestRenamed(Id, oldName, name));
        }

        public void ChangeEmail(Email email)
        {
            var oldEmail = Email.Create(Email.Value);
            Email = email;
            RaiseEvent(new TestEmailChanged(Id, oldEmail));
        }

        private Test(string name, Email email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            RaiseEvent(new TestCreated(Id));
        }

        public static Test Create(string name, Email email)
        {
            return new Test(name, email);
        }
    }
}
