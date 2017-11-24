using Caroto.DomainObjects;
using Caroto.EventHandlers;
using Caroto.Tools;
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
                sequence = JsonFileHandler.ReadJsonFile<Sequence>(@"\SiguientePlaylist\playlist.json");
                TriggerSequenceEventArgs args = new TriggerSequenceEventArgs(sequence.PlayList, sequence.TotalDurationInSeconds, sequence.OnLoop);
                TriggerSequenceEvent(this, args);
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message + "On MessageHub OnTriggerSequence");
            }
        }
    }
}
