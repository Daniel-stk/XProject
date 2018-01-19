using Gateway;
using System;
using System.IO;

namespace Caroto.Tools
{
    public class FileLogger : Logger
    {
        private static readonly Lazy<FileLogger> _instance = new Lazy<FileLogger>(() => new FileLogger());
        private static object SyncRoot = new object();
        private FileLogger() { }

        public static FileLogger Instance { get { return _instance.Value; } }

        public override void Log(string message, LogType type)
        {
            switch (type)
            {
                case LogType.Info:
                    lock (SyncRoot)
                    {
                        using (var streamWriter = new StreamWriter(CarotoSettings.Default.LogFolder + @"\Information.txt"))
                        {
                            streamWriter.WriteLine(message);
                            streamWriter.Close();
                        }
                    }

                    break;
                case LogType.Warning:
                case LogType.Error:
                    lock (SyncRoot)
                    {
                        using (var streamWriter = new StreamWriter(CarotoSettings.Default.LogFolder + @"\Exceptions.txt"))
                        {
                            streamWriter.WriteLine(message);
                            streamWriter.Close();
                        }
                    }
                    break;
            }
        }
    }
}
