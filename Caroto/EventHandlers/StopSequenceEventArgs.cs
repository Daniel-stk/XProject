using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.EventHandlers
{
    public class StopSequenceEventArgs : EventArgs
    {
        private string _sequenceName;

        public StopSequenceEventArgs()
        {
            _sequenceName = "default_sequence";
        }

        public StopSequenceEventArgs(string sequenceName)
        {
            _sequenceName = sequenceName;
        }

        public string SequenceName { get { return _sequenceName; } }
    }
}
