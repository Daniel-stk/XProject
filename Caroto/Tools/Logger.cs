namespace Caroto.Tools
{
    public enum LogType {Info, Warning, Error};

    public abstract class Logger
    {
        public abstract void Log(string message,LogType type);
    }
}
