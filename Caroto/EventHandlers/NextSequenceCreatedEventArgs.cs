using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.EventHandlers
{
    public class NextSequenceCreatedEventArgs
    {
        private string _nextSequenceStart;

        public NextSequenceCreatedEventArgs(string nextSequenceStart)
        {
            _nextSequenceStart = nextSequenceStart;
        }

        public string NextSequenceStart { get { return _nextSequenceStart; } }
    }
}
