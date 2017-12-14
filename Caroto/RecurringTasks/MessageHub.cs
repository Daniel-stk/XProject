using Caroto.DomainObjects;
using Caroto.EventHandlers;
using Caroto.Tools;
using Gateway;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Caroto.RecurringTasks
{
    public class MessageHub : RecurringTask
    {
        private static readonly Lazy<MessageHub> _instance = new Lazy<MessageHub>(() => new MessageHub());
        private MessageHub() : base() { }   

        public static MessageHub Instance { get { return _instance.Value; } }
        public event TriggerSequenceEventHandler TriggerSequenceEvent;
        public event StopSequenceEventHandler StopSequenceEvent;
        public event VideoDownloadEventHandler VideoDownloadEvent;
        public event ProgrammingUpdatedEventHandler ProgrammingUpdatedEvent;
        public event NextSequenceCreatedEventHandler NextSequenceCreatedEvent;

        public async Task PublishMessage(CancellationToken cancellationToken)
        {
            var message = await _bufferBlock.ReceiveAsync();
            if (!string.IsNullOrEmpty(message))
            {
                ProcessMessage(message);
            }
        }

        public void PublishMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ProcessMessage(message);
            }
        }

        private void ProcessMessage(string message)
        {
            switch (message)
            {
                case "Play next sequence":
                    OnTriggerSequence();
                    break;
                case "Stop sequence":
                    OnStopSequence();
                    break;
                case "Next Sequence Created":
                    OnNextSequenceCreated();
                    break;
                case "Download Operation Done":
                    OnVideoDownloadDone();
                    break;
                case "Programming Downloaded":
                    OnProgrammingUpdated();
                    break;
                default:
                    Console.WriteLine("Comando no conocido");
                    break;
            }
        }

        private void OnTriggerSequence()
        {
            Sequence sequence;
            try
            {
                sequence = JsonFileHandler.ReadJsonFile<Sequence>(CarotoSettings.Default.NextSequenceFolder + @"\nextPlaylist.json");
                JsonFileHandler.DeleteJsonFile(CarotoSettings.Default.NextSequenceFolder + @"\nextPlaylist.json");
                JsonFileHandler.WriteJsonFile(CarotoSettings.Default.NextSequenceFolder + @"\currentPlaylist.json", sequence);
                TriggerSequenceEventArgs args = new TriggerSequenceEventArgs(sequence.PlayList, sequence.TotalSequenceDuration, sequence.SequenceName ,sequence.OnLoop);
                TriggerSequenceEvent(this, args);
            }
            catch(Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Origen -" + GetType().ToString() + " Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + "On MessageHub OnTriggerSequence Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
            }
        }

        private void OnStopSequence()
        {
            Sequence sequence;
            try
            {
                sequence = JsonFileHandler.ReadJsonFile<Sequence>(CarotoSettings.Default.NextSequenceFolder + @"\currentPlaylist.json");
                CarotoSettings.Default.TotalTime += TimeSpan.ParseExact(sequence.TotalSequenceDuration,@"hh\:mm\:ss",CultureInfo.InvariantCulture);
                CarotoSettings.Default.Save();
                StopSequenceEventArgs args = new StopSequenceEventArgs(sequence.SequenceName,sequence.TotalSequenceDuration);
                JsonFileHandler.DeleteJsonFile(CarotoSettings.Default.NextSequenceFolder + @"\currentPlaylist.json");
                StopSequenceEvent(this, args);
            }
            catch(Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Origen -" + GetType().ToString() + " Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + "On MessageHub OnStopSequence Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
            }
        }

        private void OnVideoDownloadDone()
        {
            try
            {
                var videoCount = Directory.GetFiles(CarotoSettings.Default.VideoFolder + @"\videos","*.mp4").Length;
                VideoDownloadEventArgs args = new VideoDownloadEventArgs(videoCount);
                VideoDownloadEvent(this, args);
            }
            catch(Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Origen -" + GetType().ToString() + " Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + "On MessageHub OnVideoDownloadDone Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
            }
        }

        private void OnProgrammingUpdated()
        {
            try
            {
                var lastUpdate = DateTime.Now;
                Properties.Settings.Default.LastUpdate = lastUpdate;
                Properties.Settings.Default.Save();
                ProgrammingUpdatedEventArgs args = new ProgrammingUpdatedEventArgs(lastUpdate);
                ProgrammingUpdatedEvent(this, args);
            }
            catch(Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Origen -" + GetType().ToString() + " Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + "On MessageHub OnProgrammingUpdated Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
            }
        }

        private void OnNextSequenceCreated()
        {
            try
            {
                string text;
                if (File.Exists(CarotoSettings.Default.NextSequenceFolder + @"\nextPlaylist.json"))
                {
                    var sequence = JsonFileHandler.ReadJsonFile<Sequence>(CarotoSettings.Default.NextSequenceFolder + @"\nextPlaylist.json");
                    text = "Hoy de " + sequence.TimeToPlay.TimeOfDay.ToString() + "hrs a " + sequence.EndTime.TimeOfDay.ToString()+ "hrs ";
                }
                else
                {
                    text = "No existen más programaciónes para el dia de hoy";
                }
                Properties.Settings.Default.NextSequence = text;
                Properties.Settings.Default.Save();
                NextSequenceCreatedEventArgs args = new NextSequenceCreatedEventArgs(text);
                NextSequenceCreatedEvent(this, args);
            }
            catch (Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Origen -" + GetType().ToString() + " Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + "On MessageHub OnNextSequenceCreated Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
            }
        }
    }
}
