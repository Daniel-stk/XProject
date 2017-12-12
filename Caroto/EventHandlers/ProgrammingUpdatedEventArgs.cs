using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.EventHandlers
{
    public class ProgrammingUpdatedEventArgs:EventArgs
    {
        private DateTime _lastUpdate;

        public ProgrammingUpdatedEventArgs(DateTime lastUpdate)
        {
            _lastUpdate = lastUpdate;
        }

        public DateTime LastUpdate { get { return _lastUpdate; } }
    }
}
