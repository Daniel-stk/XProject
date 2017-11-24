﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.Tools
{
    class BooleanConverter : JsonConverter
    {
        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value.ToString().ToLower().Trim())
            {
                case "true":
                case "yes":
                case "y":
                case "1":
                    return true;
                case "false":
                case "no":
                case "n":
                case "0":
                    return false;
            }

            return new JsonSerializer().Deserialize(reader, objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

        }
    }
}
