using Caroto.RecurringTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task PublishMessage(CancellationToken cancellationToken)
        {
            Console.WriteLine(await _bufferBlock.ReceiveAsync());
        }
    }
}
