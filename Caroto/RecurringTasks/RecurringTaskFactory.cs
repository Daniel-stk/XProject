
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Caroto.RecurringTasks
{
    public class RecurringTaskFactory
    {

        public static ActionBlock<DateTimeOffset> CreateRecurringTask(Func<DateTimeOffset, CancellationToken, Task> action, CancellationToken cancellationToken,TimeSpan delay)
        {
            if (action == null)
            {
                throw new ArgumentNullException("La accion enviada se encuentra con valor nulo");
            }

            ActionBlock<DateTimeOffset> block = null;

            block = new ActionBlock<DateTimeOffset>(async now => {
                await action(now, cancellationToken).ConfigureAwait(false);
                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                block.Post(DateTimeOffset.Now);
            }, new ExecutionDataflowBlockOptions
            {
                CancellationToken = cancellationToken
            });

            return block;
        }
    }
}
