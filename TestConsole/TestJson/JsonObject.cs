using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TestConsole.TestJson
{
    public class JsonObject
    {
        [JsonConverter(typeof(JsonPrefixConverter))]
        public int NeedToAddPrefix { get; set; }
    }
}
