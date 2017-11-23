using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Caroto.RecurringTasks
{
    class TestSender : RecurringTask
    {
        private static readonly Lazy<TestSender> _instance = new Lazy<TestSender>(() => new TestSender());
        private TestSender() : base() { }

        public static TestSender Instance { get { return _instance.Value; } }

        public async Task WriteMessage(CancellationToken cancellationToken)
        {
            await _bufferBlock.SendAsync("Hello World");
        }
    }
}
