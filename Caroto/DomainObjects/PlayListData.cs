using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace Caroto.DomainObjects
{
    public class PlayListData
    {
        public IWMPPlaylist PlayList { get; set; }
        public bool OnLoop { get; set; }
        public string SequenceName { get; set; }
        public string TotalSequenceDuration { get; set; }
    }
}
