using Caroto.DomainObjects;
using Caroto.Tools;
using Gateway;
using Responses;
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
    public class ComposeNextSequenceTask : RecurringTask
    {
        private static readonly Lazy<ComposeNextSequenceTask> _instance = new Lazy<ComposeNextSequenceTask>(() => new ComposeNextSequenceTask());
        private ComposeNextSequenceTask() : base() { }

        public static ComposeNextSequenceTask Instance { get { return _instance.Value; } }

        public async Task CreateNextSequenceFile(CancellationToken token)
        {
            if (File.Exists(CarotoSettings.Default.ProgrammingFolder + @"\programming.json"))
            {
                var programming = JsonFileHandler.ReadJsonFile<List<ProgrammingResponse>>(CarotoSettings.Default.ProgrammingFolder + @"\programming.json");
                if (!File.Exists(CarotoSettings.Default.NextSequenceFolder + @"\nextPlaylist.json"))
                {
                    var nextProgramming = programming.Where(p => p.Start.TimeOfDay > DateTime.Now.TimeOfDay && DateTime.Now.DayOfWeek.ToString().CompareTo(Enum.GetName(typeof(DayOfWeek), p.Day - 1)) == 0).First();
                    var playList = new List<string>();
                    foreach(var video in nextProgramming.Sequence.Videos)
                    {
                        playList.Add(video.Name + ".mp4");
                    }
                    var nextSequence = new Sequence() {SequenceName = nextProgramming.Sequence.Name, TimeToPlay = nextProgramming.Start, EndTime = nextProgramming.End, OnLoop = nextProgramming.Loop , SequenceEnded = false,  TotalDurationInSeconds = "500", PlayList = playList  };
                    JsonFileHandler.WriteJsonFile(CarotoSettings.Default.NextSequenceFolder + @"\nextPlaylist.json",nextSequence);

                    await _bufferBlock.SendAsync("Next Sequence Created");
                }
            }
            else
            {
                Console.WriteLine("La programacion aun no esta generada");
            }
        }
    }
}
