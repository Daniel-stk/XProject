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
        private string _totalSequenceDuration;

        public StopSequenceEventArgs()
        {
            _sequenceName = "default_sequence";
        }

        public StopSequenceEventArgs(string sequenceName,string totalSequenceDuration)
        {
            _sequenceName = sequenceName;
            _totalSequenceDuration = totalSequenceDuration;
        }

        public string SequenceName { get { return _sequenceName; } }
        public string TotalSequenceDuration { get { return _totalSequenceDuration; } }
    }
}
