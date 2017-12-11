﻿using Caroto.RecurringTasks.Tasks;
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
        //private TestSender sender;
        private TriggerSequenceTask triggerSequence;
        private StopSequenceTask stopSequence;
        private VideoDownloadListComposerTask videoDownloadListComposer;
        private VideoDownloadTask videoDownload;
        private GenerateProgrammingTask programmingGenerator;

        public ComposeRecurringTasks()
        {
            hub = MessageHub.Instance;
            //sender = TestSender.Instance;
            videoDownloadListComposer = VideoDownloadListComposerTask.Instance;
            triggerSequence = TriggerSequenceTask.Instance;
            stopSequence = StopSequenceTask.Instance;
            videoDownload = VideoDownloadTask.Instance;
            programmingGenerator = GenerateProgrammingTask.Instance;
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
            //sender.BufferBlockMessager = MessageService;
            triggerSequence.BufferBlockMessager = MessageService;
            stopSequence.BufferBlockMessager = MessageService;
            videoDownload.BufferBlockMessager = MessageService;
            programmingGenerator.BufferBlockMessager = MessageService;
        }

        private void CreateRecurringTasks()
        {
            hub.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => hub.PublishMessage(ct), hub.CancellationTokenSource.Token,TimeSpan.FromSeconds(1));
            videoDownloadListComposer.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => videoDownloadListComposer.ComposeVideoDownloadList(ct), videoDownloadListComposer.CancellationTokenSource.Token, TimeSpan.FromMinutes(10));
            //sender.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => sender.WriteMessage(ct), sender.CancellationTokenSource.Token,TimeSpan.FromMinutes(5));
            triggerSequence.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => triggerSequence.TriggerSequence(ct), triggerSequence.CancellationTokenSource.Token,TimeSpan.FromSeconds(1));
            stopSequence.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => stopSequence.StopSequence(ct), stopSequence.CancellationTokenSource.Token, TimeSpan.FromSeconds(1));
            videoDownload.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => videoDownload.DownloadVideos(ct), videoDownload.CancellationTokenSource.Token, TimeSpan.FromMinutes(15));
            programmingGenerator.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => programmingGenerator.CreateProgrammingFile(ct), programmingGenerator.CancellationTokenSource.Token, TimeSpan.FromHours(1));
        }

        private void StartTasks()
        {
            hub.StartRecurringTask();
            //videoDownloadListComposer.StartRecurringTask();
            //sender.StartRecurringTask();
            //triggerSequence.StartRecurringTask();
            //stopSequence.StartRecurringTask();
            //videoDownload.StartRecurringTask();
            programmingGenerator.StartRecurringTask();
        }
    }
}
