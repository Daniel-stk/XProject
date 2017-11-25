using Caroto.DomainObjects;
using Caroto.Tools;
using Gateway;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Caroto.RecurringTasks.Tasks
{
    class StopSequenceTask : RecurringTask
    {
        private static readonly Lazy<StopSequenceTask> _instance = new Lazy<StopSequenceTask>(() => new StopSequenceTask());
        private StopSequenceTask () : base() { }
        
        public static StopSequenceTask Instance
        {
            get { return _instance.Value; }
        }
        
        public async Task StopSequence(CancellationToken token)
        {
            try
            {
                if (File.Exists(CarotoSettings.Default.NextSequenceFolder + @"\currentPlaylist.json")) { 
                    var sequence = JsonFileHandler.ReadJsonFile<Sequence>(CarotoSettings.Default.NextSequenceFolder + @"\currentPlaylist.json");
                    var sequenceEndTimeSpan = sequence.EndTime.TimeOfDay;
                    var currentTimeSpan = DateTime.Now.TimeOfDay;

                    if(Math.Abs((sequenceEndTimeSpan - currentTimeSpan).TotalMilliseconds) < 500)
                    {
                        await _bufferBlock.SendAsync("Stop sequence");
                    }
                }
                else
                {
                    Console.WriteLine("No existe playlist actual");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }   
    }
}
