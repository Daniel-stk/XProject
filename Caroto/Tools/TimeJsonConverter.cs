using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Caroto.Tools
{
    class TimeJsonConverter : JsonConverter
    {
        public override bool CanWrite
        {
            get
            {
                return false; 
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var time = reader.Value.ToString();
            DateTime parsedTime = DateTime.ParseExact(time, "H:mm", CultureInfo.InvariantCulture);
            return parsedTime;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
