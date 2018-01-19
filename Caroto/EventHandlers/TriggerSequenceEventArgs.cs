using Caroto.DomainObjects;
using System;
using System.Collections.Generic;

namespace Caroto.EventHandlers
{
    public class TriggerSequenceEventArgs : EventArgs
    {
        private List<string> _playlist;
        private string _totalSequenceDuration;
        private string _sequenceName;
        private bool _onLoop;
        private Sequence _sequence;

        public TriggerSequenceEventArgs(List<string> playlist,bool onLoop)
        {
            if(playlist == null)
            {
                throw new ArgumentNullException("playlist contiene un valor nulo");
            }

            _playlist = playlist;
            _sequenceName = "default_sequence";
            _totalSequenceDuration = "500";
            _onLoop = onLoop;
        }

        public TriggerSequenceEventArgs(List<string> playlist, string totalDurationInSeconds,bool onLoop)
        {
            if (playlist == null)
            {
                throw new ArgumentNullException("playlist contiene un valor nulo");
            }
            if (string.IsNullOrEmpty(totalDurationInSeconds))
            {
                throw new ArgumentNullException("totalDurationInseconds contiene valor nulo");
            }

            _playlist = playlist;
            _totalSequenceDuration = totalDurationInSeconds;
            _sequenceName = "default_sequence";
            _onLoop = onLoop;
        }
        public TriggerSequenceEventArgs(List<string> playlist, string totalDurationInSeconds,string sequenceName,bool onLoop)
        {
            if (playlist == null)
            {
                throw new ArgumentNullException("playlist contiene un valor nulo");
            }
            if (string.IsNullOrEmpty(totalDurationInSeconds))
            {
                throw new ArgumentNullException("totalDurationInseconds contiene valor nulo");
            }
            if (string.IsNullOrEmpty(sequenceName))
            {
                throw new ArgumentNullException("totalDurationInseconds contiene valor nulo");
            }

            _playlist = playlist;
            _totalSequenceDuration = totalDurationInSeconds;
            _sequenceName = sequenceName;
            _onLoop = onLoop;
        }

        public TriggerSequenceEventArgs(Sequence sequence)
        {
            if(sequence == null)
            {
                throw new ArgumentNullException("sequence contiene valor nulo");
            }

            _sequence = sequence;
        }

        public List<string> PlayList { get { return _playlist; } }
        public string TotalSequenceDuration { get { return _totalSequenceDuration; } }
        public string SequenceName { get { return _sequenceName; } }
        public bool OnLoop { get { return _onLoop; } }
        public Sequence Sequence { get { return _sequence; } }

    }
}
