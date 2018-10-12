using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json;

namespace TestConsole.Domain.Core
{
    public class Email : ValueObject
    {
        public string Value { get; private set; }

        [JsonConstructor]
        private Email(string value) => Value = value;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value;

        #region Static methods
        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
                return null;

            try
            {
                return new Email(
                    new MailAddress(value).Address.ToLowerInvariant());
            }
            catch
            {
                // ignored
            }
            return null;
        }
        #endregion
    }
}
