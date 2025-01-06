using Newtonsoft.Json;
using System;
using theFermiParadox.Core.Interfaces;

namespace theFermiParadox.Core.Utilities
{
    public class UniqueConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IUnique body)
            {
                var id = serializer.ReferenceResolver.GetReference(serializer.Context, body);
                writer.WriteValue(id);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var id = reader.Value.ToString();
            return serializer.ReferenceResolver.ResolveReference(serializer.Context, id);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IUnique).IsAssignableFrom(objectType);
        }
    }

}
