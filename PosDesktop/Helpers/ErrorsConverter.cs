using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace PosDesktop.Helpers;

public class ErrorsConverter : JsonConverter<IDictionary<string, ICollection<string>>>
{
    public override IDictionary<string, ICollection<string>> ReadJson(JsonReader reader, Type objectType, IDictionary<string, ICollection<string>> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var errors = new Dictionary<string, ICollection<string>>();
        var jObject = JObject.Load(reader);

        foreach (var property in jObject.Properties())
        {
            if (property.Value.Type == JTokenType.Array)
            {
                errors[property.Name] = property.Value.ToObject<ICollection<string>>();
            }
            else
            {
                errors[property.Name] = new List<string> { property.Value.ToString() };
            }
        }

        return errors;
    }

    public override void WriteJson(JsonWriter writer, IDictionary<string, ICollection<string>> value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}