using Caroto.DomainObjects;
using Caroto.Tools;
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
            try
            {
                if (File.Exists(CarotoSettings.Default.NextSequenceFolder + @"\nextPlaylist.json")) { 
                    var playlist = JsonFileHandler.ReadJsonFile<Sequence>(CarotoSettings.Default.NextSequenceFolder+@"\nextPlaylist.json");
                    var playListTimeSpan = playlist.TimeToPlay.TimeOfDay;
                    var currentTimeSpan = DateTime.Now.TimeOfDay;
                    Console.WriteLine("Hora de reproducir - " + playListTimeSpan.ToString() + " - Hora actual - " + currentTimeSpan.ToString());

                    if(Math.Abs((playListTimeSpan - currentTimeSpan).TotalMilliseconds) < 500)
                    {
                        await _bufferBlock.SendAsync("Play next sequence");
                    }
                }
                else
                {
                    Console.WriteLine("Siguiente playlist no definido");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Origen -" + GetType().ToString() + " Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + " Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
            }
        }
    }
}
