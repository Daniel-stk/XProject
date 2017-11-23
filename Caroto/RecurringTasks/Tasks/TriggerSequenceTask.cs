using Caroto.DomainObjects;
using Gateway;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Caroto.RecurringTasks.Tasks
{
    public class TriggerSequenceTask : RecurringTask
    {
        private static readonly Lazy<TriggerSequenceTask> _instance = new Lazy<TriggerSequenceTask>(() => new TriggerSequenceTask());
        private TriggerSequenceTask() : base() { }

        public static TriggerSequenceTask Instance { get { return _instance.Value; } }

        public async Task TriggerSequence(CancellationToken token)
        {
            using (StreamReader file = File.OpenText(CarotoSettings.Default.BaseFolder+ @"\SiguientePlaylist\playlist.json"))
            {
                try
                {
                    var serializer = new JsonSerializer();
                    var playlist = (Sequence)serializer.Deserialize(file, typeof(Sequence));
                    var playListTimeSpan = playlist.TimeToPlay.TimeOfDay;
                    var currentTimeSpan = DateTime.Now.TimeOfDay;
                    Console.WriteLine("Hora de reproducir - " + playListTimeSpan.ToString() + " - Hora actual - " + currentTimeSpan.ToString());

                    if(Math.Abs((playListTimeSpan - currentTimeSpan).TotalMilliseconds) < 500)
                    {
                        await _bufferBlock.SendAsync("Play next sequence");
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
             }
        }
    }
}
