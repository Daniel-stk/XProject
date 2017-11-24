using System;
using System.Collections.Generic;

namespace Caroto.EventHandlers
{
    public class TriggerSequenceEventArgs : EventArgs
    {
        private List<string> _playlist;
        private string _totalDurationInSeconds;
        private bool _onLoop;

        public TriggerSequenceEventArgs(List<string> playlist)
        {
            if(playlist == null)
            {
                throw new ArgumentNullException("playlist contiene un valor nulo");
            }

            _playlist = playlist;
            _onLoop = false;
            _totalDurationInSeconds = "500";
        }

        public TriggerSequenceEventArgs(List<string> playlist, string totalDurationInSeconds)
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
            _totalDurationInSeconds = totalDurationInSeconds;
            _onLoop = false;
        }

        public TriggerSequenceEventArgs(List<string> playlist, string totalDurationInSeconds, bool onLoop)
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
            _totalDurationInSeconds = totalDurationInSeconds;
            _onLoop = onLoop;
        }

        public List<string> PlayList { get { return _playlist; } }
        public string TotalDurationInSeconds { get { return _totalDurationInSeconds; } }
        public bool OnLoop { get { return _onLoop; } }
    }
}
