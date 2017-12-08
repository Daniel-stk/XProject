using Caroto.DomainObjects;
using Caroto.EventHandlers;
using Caroto.Tools;
using Gateway;
using System;
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

        public async Task PublishMessage(CancellationToken cancellationToken)
        {
            var message = await _bufferBlock.ReceiveAsync();
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
                TriggerSequenceEventArgs args = new TriggerSequenceEventArgs(sequence.PlayList, sequence.TotalDurationInSeconds, sequence.SequenceName ,sequence.OnLoop);
                TriggerSequenceEvent(this, args);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "On MessageHub OnTriggerSequence");
            }
        }

        private void OnStopSequence()
        {
            Sequence sequence;
            try
            {
                sequence = JsonFileHandler.ReadJsonFile<Sequence>(CarotoSettings.Default.NextSequenceFolder + @"\currentPlaylist.json");
                StopSequenceEventArgs args = new StopSequenceEventArgs(sequence.SequenceName);
                JsonFileHandler.DeleteJsonFile(CarotoSettings.Default.NextSequenceFolder + @"\currentPlaylist.json");
                StopSequenceEvent(this, args);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "On MessageHub OnStopSequence");
            }
        }
    }
}
