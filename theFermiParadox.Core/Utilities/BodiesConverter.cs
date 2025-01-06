using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core.Utilities
{
    public class BodiesConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is List<ABody> bodies)
            {
                writer.WriteStartArray();
                foreach (var body in bodies)
                {
                    serializer.Serialize(writer, body); 
                }
                writer.WriteEndArray();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<List<ABody>>(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(List<ABody>).IsAssignableFrom(objectType);
        }
    }

}
