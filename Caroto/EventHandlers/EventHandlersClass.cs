
namespace Caroto.EventHandlers
{
    public delegate void TriggerSequenceEventHandler(object sender, TriggerSequenceEventArgs args);
    public delegate void StopSequenceEventHandler(object sender, StopSequenceEventArgs args);
    public delegate void VideoDownloadEventHandler(object sender,VideoDownloadEventArgs args);
    public delegate void ProgrammingUpdatedEventHandler(object sender,ProgrammingUpdatedEventArgs args);
    public delegate void NextSequenceCreatedEventHandler(object sender, NextSequenceCreatedEventArgs args);

    public class EventHandlersClass { } 
}
