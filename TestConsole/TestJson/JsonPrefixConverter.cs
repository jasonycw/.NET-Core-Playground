using Newtonsoft.Json;
using System;

namespace TestConsole.TestJson
{
    public class JsonPrefixConverter : JsonConverter
    {
        private const string Prefix = "prefix=";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is int)
                writer.WriteValue($"{Prefix}{value}");
            else
                throw new Exception("Expected int object value.");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
                throw new Exception($"Unexpected token parsing date. Expected string, got {reader.TokenType}.");

            var str = (string)reader.Value;
            if (int.TryParse(str.Replace(Prefix, ""), out var result))
                return result;
            throw new Exception($"Invalid value '{reader.Value}' while deserializing value.");
        }

        public override bool CanConvert(Type objectType) => true;
    }
}
