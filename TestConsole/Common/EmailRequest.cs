using Newtonsoft.Json;
using TestConsole.Common.Mapper;

namespace TestConsole.Common
{
    public class EmailRequest
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        [JsonProperty("html")]
        public string Body { get; set; }
        public Attachment Attachment { get; set; }

        public string ToJson()
            => JsonConvert.SerializeObject(this,
                new JsonSerializerSettings { ContractResolver = new EmailContractResolver() });
    }

    public class Attachment
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        // TODO: Change it to match "{ type: 'Buffer', data: [Array] }"
        [JsonProperty("data")]
        public string File { get; set; }
    }
}
