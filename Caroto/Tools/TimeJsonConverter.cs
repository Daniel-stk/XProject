using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Caroto.Tools
{
    class TimeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var time = reader.Value.ToString();
                DateTime parsedTime = DateTime.ParseExact(time, @"H:mm", CultureInfo.InvariantCulture);
                return parsedTime;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DateTime.Now;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            { 
                DateTime dateTime;
                if(value is DateTime)
                {
                    dateTime = (DateTime)value;
                }
                else
                {
                    dateTime = DateTime.Now;
                }

                var time = dateTime.TimeOfDay;
                writer.WriteValue(time.ToString(@"h:mm"));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
