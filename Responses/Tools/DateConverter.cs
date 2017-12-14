using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.Tools
{
    public class DateConverter : JsonConverter
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
                DateTime parsedTime = DateTime.ParseExact(time, @"yyyy-MM-dd", CultureInfo.InvariantCulture);
                return parsedTime;
            }
            catch (Exception ex)
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
                if (value is DateTime)
                {
                    dateTime = (DateTime)value;
                }
                else
                {
                    dateTime = DateTime.Now;
                }
                writer.WriteValue(dateTime.ToString(@"yyyy-MM-dd"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "On JSON write");
            }
        }
    }
}
