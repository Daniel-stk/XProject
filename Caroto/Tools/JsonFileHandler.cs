using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Caroto.Tools
{
    public class JsonFileHandler
    {
        private static object SyncRoot = new object();
        public static T ReadJsonFile<T>(string path) where T : class 
        {
            lock (SyncRoot)
            { 
                using (StreamReader file = File.OpenText(path))
                {
                        var serializer = new JsonSerializer();
                        var output = serializer.Deserialize(file, typeof(T)) as T;
                        return output;
                }
            }
        }

        public static bool WriteJsonFile<T>(string path, T data) where T : class
        {
            lock (SyncRoot)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (StreamWriter file = File.CreateText(path))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, data);
                    return true;
                }
            }
        }

        public static bool WriteJsonFile<T>(string path,List<T> data) where T : class
        {
            lock (SyncRoot)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (StreamWriter file = File.CreateText(path))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, data.ToArray());
                    return true;
                }
            }
        }

        public static bool DeleteJsonFile(string path)
        {
            lock (SyncRoot)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
            }
            return false;
        }
    }
}
