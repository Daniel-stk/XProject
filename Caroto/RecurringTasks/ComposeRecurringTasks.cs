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
        private TestSender sender;
        private TriggerSequenceTask triggerSequence;

        public ComposeRecurringTasks()
        {
            hub = MessageHub.Instance;
            sender = TestSender.Instance;
            triggerSequence = TriggerSequenceTask.Instance;
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
            sender.BufferBlockMessager = MessageService;
            triggerSequence.BufferBlockMessager = MessageService;
        }

        private void CreateRecurringTasks()
        {
            hub.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => hub.PublishMessage(ct), hub.CancellationTokenSource.Token,TimeSpan.FromSeconds(1));
            sender.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => sender.WriteMessage(ct), sender.CancellationTokenSource.Token,TimeSpan.FromMinutes(5));
            triggerSequence.NeverEndingTask = RecurringTaskFactory.CreateRecurringTask((now, ct) => triggerSequence.TriggerSequence(ct), triggerSequence.CancellationTokenSource.Token,TimeSpan.FromSeconds(1));
        }

        private void StartTasks()
        {
            hub.StartRecurringTask();
            sender.StartRecurringTask();
            triggerSequence.StartRecurringTask();
        }
    }
}
