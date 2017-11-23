using Caroto.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Caroto.RecurringTasks
{
    public abstract class RecurringTask
    {
        protected ActionBlock<DateTimeOffset> _neverEndingTask;
        protected CancellationTokenSource _cancellationTokenSource;
        protected BufferBlock<string> _bufferBlock;

        public BufferBlock<string> BufferBlockMessager { get { return _bufferBlock; } set { _bufferBlock = value; } }
        public ActionBlock<DateTimeOffset> NeverEndingTask { get { return _neverEndingTask; } set { _neverEndingTask = value; } }
        public CancellationTokenSource CancellationTokenSource { get { return _cancellationTokenSource; } }

        public RecurringTask()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public virtual void StartRecurringTask()
        {
            if(_neverEndingTask != null)
            {
                _neverEndingTask.Post(DateTimeOffset.Now);
            }
            else
            {
                throw new RecurringTaskNullException("La tarea recurrente esta en valor nulo");
            }
        }

        public virtual void StopRecurringTask()
        {
            using (_cancellationTokenSource)
            {
                _cancellationTokenSource.Cancel();
            }

            _cancellationTokenSource = null;
            _neverEndingTask = null;
        }
    }
}
