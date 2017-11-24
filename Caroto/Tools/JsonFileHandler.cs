using Gateway;
using Newtonsoft.Json;
using System.IO;

namespace Caroto.Tools
{
    public class JsonFileHandler
    {
        public static T ReadJsonFile<T>(string path) where T : class 
        {
            using (StreamReader file = File.OpenText(CarotoSettings.Default.BaseFolder + path))
            {
                    var serializer = new JsonSerializer();
                    var output = serializer.Deserialize(file, typeof(T)) as T;
                    return output;
            }
        }
    }
}
