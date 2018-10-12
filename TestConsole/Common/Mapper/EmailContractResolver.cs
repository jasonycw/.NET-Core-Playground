using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace TestConsole.Common.Mapper
{
    public class EmailContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            property.PropertyName = char.ToLowerInvariant(property.PropertyName[0]) + property.PropertyName.Substring(1);
            //if (property.DeclaringType == typeof(EmailRequest) && property.PropertyName == "body")
            //    property.PropertyName = "html";
            //if (property.DeclaringType == typeof(Attachment) && property.PropertyName == "file")
            //    property.PropertyName = "data";

            return property;
        }
    }
}
