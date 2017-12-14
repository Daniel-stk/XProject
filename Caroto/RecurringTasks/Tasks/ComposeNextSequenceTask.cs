using Caroto.DomainObjects;
using Caroto.Tools;
using Gateway;
using MoreLinq;
using Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Caroto.RecurringTasks.Tasks
{
    public class ComposeNextSequenceTask : RecurringTask
    {
        private static readonly Lazy<ComposeNextSequenceTask> _instance = new Lazy<ComposeNextSequenceTask>(() => new ComposeNextSequenceTask());
        private ComposeNextSequenceTask() : base() { }

        public static ComposeNextSequenceTask Instance { get { return _instance.Value; } }

        public async Task CreateNextSequenceFile(CancellationToken token)
        {
            if (File.Exists(CarotoSettings.Default.ProgrammingFolder + @"\programming.json"))
            {
                try
                { 
                    var programming = JsonFileHandler.ReadJsonFile<List<ProgrammingResponse>>(CarotoSettings.Default.ProgrammingFolder + @"\programming.json");
                    if (!File.Exists(CarotoSettings.Default.NextSequenceFolder + @"\nextPlaylist.json"))
                    {
                        var todayProgramming = programming.Where(p => DateTime.Now.TimeOfDay <= p.Start.TimeOfDay && DateTime.Now.DayOfWeek.ToString().CompareTo(Enum.GetName(typeof(DayOfWeek), p.Day - 1)) == 0);
                        if (todayProgramming.Any())
                        {
                            var nextProgramming = todayProgramming.MinBy(t => Math.Abs((t.Start.TimeOfDay - DateTime.Now.TimeOfDay).Ticks));

                            var playList = new List<string>();
                            var totalDuration = new TimeSpan(0,0,0);
                            foreach(var video in nextProgramming.Sequence.Videos)
                            {
                                playList.Add(video.File + ".mp4");
                                var timeToAdd = new TimeSpan(0, 0, Convert.ToInt32(video.Duration));
                                totalDuration += timeToAdd;
                            }
                            var nextSequence = new Sequence() {SequenceName = nextProgramming.Sequence.Name, TimeToPlay = nextProgramming.Start, EndTime = nextProgramming.End, OnLoop = nextProgramming.Loop , SequenceEnded = false,  TotalSequenceDuration = totalDuration.ToString(), PlayList = playList  };
                            JsonFileHandler.WriteJsonFile(CarotoSettings.Default.NextSequenceFolder + @"\nextPlaylist.json",nextSequence);
                        }
                        else
                        {
#if DEBUG
                            FileLogger.Instance.Log("Origen -"+GetType().ToString()+"Mensaje - No hay mas programaciónes para este dia "+"Fecha - "+DateTime.Now.ToString(),LogType.Info);
#endif
                        }
                        await _bufferBlock.SendAsync("Next Sequence Created");
                    }
                }
                catch(Exception ex)
                {
#if DEBUG
                    FileLogger.Instance.Log("Origen -" + GetType().ToString() + " Tipo - " +ex.GetType().ToString()+"Mensaje - "+ex.Message+" Fecha - "+ DateTime.Now.ToString(), LogType.Error);
#endif
                }
            }
            else
            {
#if DEBUG
                FileLogger.Instance.Log("Origen -" + GetType().ToString() + "Mensaje - La programación aun no esta generada " + "Fecha - " + DateTime.Now.ToString(), LogType.Info);
#endif
            }
        }
    }
}
