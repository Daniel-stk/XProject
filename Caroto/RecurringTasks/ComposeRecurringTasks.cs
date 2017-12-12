using Caroto.RecurringTasks.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Caroto.RecurringTasks
{
    public class ComposeRecurringTasks
    {
        private static BufferBlock<string> MessageService = new BufferBlock<string>();
        private MessageHub hub;
        private TriggerSequenceTask triggerSequence;
        private StopSequenceTask stopSequence;
        private VideoDownloadListComposerTask videoDownloadListComposer;
        private VideoDownloadTask videoDownload;
        private GenerateProgrammingTask programmingGenerator;
        private ComposeNextSequenceTask nextSequenceComposer;

        public ComposeRecurringTasks()
        {
            hub = MessageHub.Instance;
            videoDownloadListComposer = VideoDownloadListComposerTask.Instance;
            triggerSequence = TriggerSequenceTask.Instance;
            stopSequence = StopSequenceTask.Instance;
            videoDownload = VideoDownloadTask.Instance;
            programmingGenerator = GenerateProgrammingTask.Instance;
            nextSequenceComposer = ComposeNextSequenceTask.Instance;
        }

        public void ComposeTasks()
        {
            WireMessageService();
            CreateRecurringTasks();
            StartTasks();
        }

        private void WireMessageService()
        {
            hub.BufferBlockMessager = MessageService;
            videoDownloadListComposer.BufferBlockMessager = MessageService;
            triggerSequence.BufferBlockMessager = MessageService;
            stopSequence.BufferBlockMessager = MessageService;
            videoDownload.BufferBlockMessager = MessageService;
            programmingGenerator.BufferBlockMessager = MessageService;
            nextSequenceComposer.BufferBlockMessager = MessageService;
        }

        private void CreateRecurringTasks()
        {
            hub.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => hub.PublishMessage(ct), hub.CancellationTokenSource.Token,TimeSpan.FromSeconds(1));
            videoDownloadListComposer.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => videoDownloadListComposer.ComposeVideoDownloadList(ct), videoDownloadListComposer.CancellationTokenSource.Token, TimeSpan.FromMinutes(10));
            triggerSequence.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => triggerSequence.TriggerSequence(ct), triggerSequence.CancellationTokenSource.Token,TimeSpan.FromSeconds(1));
            stopSequence.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => stopSequence.StopSequence(ct), stopSequence.CancellationTokenSource.Token, TimeSpan.FromSeconds(1));
            videoDownload.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => videoDownload.DownloadVideos(ct), videoDownload.CancellationTokenSource.Token, TimeSpan.FromMinutes(15));
            programmingGenerator.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => programmingGenerator.CreateProgrammingFile(ct), programmingGenerator.CancellationTokenSource.Token, TimeSpan.FromDays(1));
            nextSequenceComposer.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => nextSequenceComposer.CreateNextSequenceFile(ct), programmingGenerator.CancellationTokenSource.Token, TimeSpan.FromMinutes(1));
        }

        private void StartTasks()
        {
            hub.StartRecurringTask();
            videoDownloadListComposer.StartRecurringTask();
            triggerSequence.StartRecurringTask();
            stopSequence.StartRecurringTask();
            programmingGenerator.StartRecurringTask();
            nextSequenceComposer.StartRecurringTask();
            videoDownload.StartRecurringTask();
        }
    }
}
